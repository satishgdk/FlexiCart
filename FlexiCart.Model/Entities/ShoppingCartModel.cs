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
			//Adding Product to Store  
        }

        private void Reset()
        {
             
            this.Total = 0;
        }

		public void AddPromotions(List<Promotion> lstPromotions)
		{
			Promotions = lstPromotions;
		}

		public decimal GetTotal()
		{
		 

			return Total;
		}

		 


	}
}
