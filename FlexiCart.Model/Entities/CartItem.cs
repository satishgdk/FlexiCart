using FlexiCart.Model.Interfaces;

namespace FlexiCart.Model.Entities
{
    public class CartItem
    {
        public CartItem()
        { 
        }

        /// <summary>
        /// Represent the actual Product from db and exposed as Item
        /// </summary>
        public IProduct Item { get; set; }

        /// <summary>
        /// Ordered quantity from UI 
        /// </summary>
        public int OrderedQty { get; set; }



    }
}
