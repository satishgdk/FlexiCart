using FlexiCart.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FlexiCart.Model.Entities
{
    public abstract class Promotion : IPromotion
    {
        public int RuleID { get; set; }

        public string RuleName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public abstract void ApplyRule(ShoppingCartModel shopppingCartModel);

    }


    /// <summary>
    /// This class representing Simple promotion for entities of same Product
    /// </summary>
    public class SimplePromotion : Promotion
    {
        /// <summary>
        /// This Property will hold criteria to apply and parameters involved in processing => rate calculation
        /// </summary>
        public Criteria Group { get; set; }

        public Criteria GetCriteria()
        {
            return Group;
        }

        public void SetCriteria(Criteria value)
        {
            Group = value;
        }

        /// <summary>
        /// using Abstract Factory methodology define own logic to apply rule for same family of objects with concrete defination of each type
        /// </summary>
        /// <param name="cart"></param>
        public override void ApplyRule(ShoppingCartModel shoppingCartModel)
        {
            bool hasMatch = false;
            do
            {
                var r = shoppingCartModel.CartItems.Where(c => c.ToBeProcessedQty > 0)
                     .FirstOrDefault(c => c.Item.SKU == Group.ProductName && c.ToBeProcessedQty >= Group.Qty);
                if (r != null)
                {
                    r.GrossAmount += Group.OfferedPrice;
                    r.ProcessedQty += Group.Qty;
                    r.ToBeProcessedQty -= Group.Qty;

                    hasMatch = true;
                }
                else
                {
                    hasMatch = false;
                }
            }
            while (hasMatch);
        }
    }

    /// <summary>
    /// This class representing Group promotion for entities of different Product
    /// </summary>
    public class GroupedPromotion : Promotion
    {
        /// <summary>
        /// offeredPrice for Group of Object with each having defined own price
        /// </summary>
        public decimal OfferedPrice { get; set; }

        /// <summary>
        /// When we want to apply criteria on different type of products/objects
        /// </summary>
        public List<Criteria> Groups { get; set; }

        /// <summary>
        /// using Abstract Factory methodology define own logic to apply rule for same family of objects with concrete defination of each type
        /// </summary>
        /// <param name="cart"></param>
        public override void ApplyRule(ShoppingCartModel shoppingCartModel)
        {
            bool hasMatch = false;
            bool isGroupMatch = false;
            do
            {
                // check if each filter has match  like we need C+D with a price but both should exist in Cart
                foreach (var criteria in Groups)
                {
                    var r = shoppingCartModel.CartItems.Where(c => c.ToBeProcessedQty > 0)
                 .FirstOrDefault(c => c.Item.SKU == criteria.ProductName && c.ToBeProcessedQty >= criteria.Qty);
                    if (r != null)
                        isGroupMatch = true;
                    else
                        isGroupMatch = false;

                }
                if (isGroupMatch == false)
                {
                    hasMatch = false;
                }
                else
                {
                    foreach (var criteria in Groups)
                    {
                        var r = shoppingCartModel.CartItems.Where(c => c.ToBeProcessedQty > 0)
                     .FirstOrDefault(c => c.Item.SKU == criteria.ProductName && c.ToBeProcessedQty >= criteria.Qty);
                        if (r != null)
                        {
                            r.GrossAmount += criteria.OfferedPrice;
                            r.ProcessedQty += criteria.Qty;
                            r.ToBeProcessedQty -= criteria.Qty;

                        }

                    }

                }
            }
            while (hasMatch);
        }

    }

     
}

