using FlexiCart.Model.Interfaces;

namespace FlexiCart.Model.Entities
{
    public class Product : IProduct
    {
        public int ProductID { get; set; }
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
