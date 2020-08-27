namespace FlexiCart.Model.Entities
{
    /// <summary>
    /// This class acts as Wrapper to  attach rules to perform logic on calculations
    /// </summary>
    public class Criteria
    {
        public int Qty { get; set; }

        public string ProductName { get; set; }

        public int OfferedPrice { get; set; }
    }

    /// <summary>
    /// class to extend the Criteria with out breaking anything -new feature 
    /// </summary>
    public class DiscountCriteria : Criteria
    { 
        public decimal Discount { get; set; }
    }

}
