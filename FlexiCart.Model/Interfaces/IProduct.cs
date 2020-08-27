namespace FlexiCart.Model.Interfaces
{
    public interface IProduct
    {
        int ProductID { get; set; }

        string SKU { get; set; }

        decimal UnitPrice { get; set; }

    }
}
