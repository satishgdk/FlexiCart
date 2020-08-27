namespace FlexiCart.Model.Interfaces
{
    public interface IShoppingCartModel
    { 
        decimal GetTotal();

        void AddProduct(IProduct product, int quantity);
    }
}
