using FlexiCart.Model.Entities;
using FlexiCart.Model.Interfaces;
using FlexiCart.Service;
using FlexiCart.Service.Interfaces;
using System;
using System.Linq;
using Unity;

namespace FlexiCart
{
    class Program
    {
        static IUnityContainer container = new UnityContainer();

        static void Main(string[] args)
        {

            #region Setup Container with RegisteredTypes 


            container.RegisterType<IProduct, Product>();

            #endregion

            ClientApp user1 = container.Resolve<ClientApp>();

            user1.BuySimpleProducts();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// An app /class considering as User performing action
    /// </summary>
    public class ClientApp

    {
        /// <summary>
        ///  User chosing simple products with different quantities and checking total of the Shopping cart 
        /// </summary>
        public void BuySimpleProducts()
        {
            Console.WriteLine("Buy Simple products \n");

            IRepository<Product> _productRepository = new ProductRepository(new FakeDbContext());
            var products = _productRepository.GetAll();

            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 2);

            cart.GetTotal();

        }


    }
}
