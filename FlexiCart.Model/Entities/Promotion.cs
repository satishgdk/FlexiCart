using FlexiCart.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FlexiCart.Model.Entities
{
    public abstract class Promotion :IPromotion
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
             //Apply own logic here to how this rule will impact the cart
        }
    }
     
}

