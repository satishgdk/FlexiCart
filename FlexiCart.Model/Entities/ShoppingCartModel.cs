using FlexiCart.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexiCart.Model.Entities
{
	public class ShoppingCartModel : IShoppingCartModel
	{

		private Dictionary<string, CartItem> _cartItems = new Dictionary<string, CartItem>();
		private List<CartItem> lstCartItems = new List<CartItem>();

		public ShoppingCartModel()
		{
			Promotions = new List<Promotion>();
			Total = 0;
		}

		private decimal Total { get; set; }

		public List<CartItem> CartItems
		{
			get { return _cartItems.Values.ToList(); }

		}

		public List<Promotion> Promotions { get; set; }

		public void AddProduct(IProduct product, int quantity)
		{
			//add product to dictionary
			if (!_cartItems.ContainsKey(product.SKU))
			{
				_cartItems.Add(product.SKU, new CartItem() { Item = product, OrderedQty = quantity, ProcessedQty = 0, ToBeProcessedQty = quantity });
			}
			else
				throw new Exception("Adding duplicate Item ");
		}

		private void Reset()
		{
			for (int i = 0; i < CartItems.Count; i++)
			{
				CartItems[i].GrossAmount = 0;
				CartItems[i].ProcessedQty = 0;
				CartItems[i].ToBeProcessedQty = CartItems[i].OrderedQty;
			}
			this.Total = 0;
		}

		public void AddPromotions(List<Promotion> lstPromotions)
		{
			Promotions = lstPromotions;
		}

		public decimal GetTotal()
		{
			Reset();
			ApplyPromotions();
			CalculateGrossAmount();
			CalculateBillTotal();
			PrintCartItems();

			return Total;
		}


		/// <summary>
		/// Using Iterator pattern apply one or more promotions
		/// </summary>
		private void ApplyPromotions()
		{
			foreach (var promotion in Promotions)
			{
				promotion.ApplyRule(this);
			}
		}


		/// <summary>
		/// Subsystem to process each item for calculating Item Gross amount
		/// </summary>
		private void CalculateGrossAmount()
		{
			foreach (var item in CartItems.Where(p => p.ToBeProcessedQty > 0))
			{
				// sum up gross amount with applied rules
				item.GrossAmount += (item.ToBeProcessedQty * item.Item.UnitPrice);
				//update Processed Quantity
				item.ProcessedQty += item.ToBeProcessedQty;
				//update remaining quantity to apply if any other rule matches.
				item.ToBeProcessedQty -= item.ToBeProcessedQty;
			}
		}

		/// <summary>
		/// Total calculation of bill from multiple item and we can further add discount or any other feature at bill level
		/// </summary>
		private void CalculateBillTotal()
		{
			this.Total = this.CartItems.Sum(p => p.GrossAmount);
		}

		/// <summary>
		/// A method to track current state of objects for UI
		/// </summary>
		public void PrintCartItems()
		{

			foreach (var item in this.CartItems)
			{
				Console.WriteLine(string.Format(" Product: {0} , OrderedQty : {1} , GrossAmount :{2} ", item.Item.SKU, item.OrderedQty, item.GrossAmount));
			}

			Console.WriteLine(string.Format("Total:{0}", this.Total));
		}


	}
}
