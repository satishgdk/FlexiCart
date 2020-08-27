using FlexiCart.Model.Entities;
using FlexiCart.Model.Interfaces;
using FlexiCart.Service;
using FlexiCart.Service.Interfaces;
using System;
using System.Linq;
using Unity;
using Unity.Injection;

namespace FlexiCart
{
    class Program
    {
        static IUnityContainer container = new UnityContainer();

        static void Main(string[] args)
        {

            #region Setup Container with RegisteredTypes 


            container.RegisterType<IProduct, Product>();
            container.RegisterType<IPromotion, SimplePromotion>();

            container.RegisterType<IPromotion, GroupedPromotion>("GroupedPromotion");

            container.RegisterType<IPromotion, DiscountedPromotion>("DiscountedPromotion");

            container.RegisterType<Criteria, Criteria>("DiscountedPromotion");

            container.RegisterType<Criteria, DiscountCriteria>("DiscountCriteria");

            container.RegisterType<IShoppingCartModel, ShoppingCartModel>("ShoppingCartModel");

            container.RegisterType<IRepository<Product>, ProductRepository>(new InjectionConstructor(new FakeDbContext()));

            container.RegisterType<IPromotionRepository<Promotion>, PromotionRepository>(new InjectionConstructor(new FakeDbContext()));


            #endregion


            ClientApp user1 = container.Resolve<ClientApp>();

            user1.BuySimpleProducts();

            ClientApp user2 = container.Resolve<ClientApp>();

            user2.BuyProductsWithActivePromotions();


            ClientApp user3 = container.Resolve<ClientApp>();

            user3.BuyProductsWithActiveGroupedPromotions();

            ClientApp user4 = container.Resolve<ClientApp>();

            user4.BuyProductsWithFestivalPromotions();

            ClientApp user5 = container.Resolve<ClientApp>();

            user5.BuyProducts();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// An app /class considering as User performing action
    /// </summary>
    public class ClientApp

    {
        private readonly IRepository<Product> _productRepository;
        private readonly IPromotionRepository<Promotion> _promotionRepository;

        public ClientApp(IRepository<Product> productRepository, IPromotionRepository<Promotion> promotionRepository)
        {
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;

        }


        /// <summary>
        ///  User chosing simple products with different quantities and checking total of the Shopping cart 
        /// </summary>
        public void BuySimpleProducts()
        {
            Console.WriteLine("Buy Simple products \n");

            var products = _productRepository.GetAll();

            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 2);

            cart.GetTotal();

        }

        /// <summary>
        /// This method is to buy products considering Active Promotions
        /// </summary>
        public void BuyProductsWithActivePromotions()
        {


            Console.WriteLine("Buy  products with active promotions \n");
            var products = _productRepository.GetAll();
            var lstActivePromotions = _promotionRepository.GetActiveRules().ToList();

            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 5);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 1);
            cart.AddPromotions(lstActivePromotions);

            cart.GetTotal();


        }

        /// <summary>
        /// This method is to buy products considering Active Promotions with Group offer [C+D]
        /// </summary>
        public void BuyProductsWithActiveGroupedPromotions()
        {

            Console.WriteLine("Buy  products with active promotions with Group Promotion [C+D] \n");
            var products = _productRepository.GetAll();
            var lstActivePromotions = _promotionRepository.GetActiveRules().ToList();


            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 1);
            cart.AddProduct(products.First(p => p.SKU == "D"), 1);
            cart.AddPromotions(lstActivePromotions);

            cart.GetTotal();


        }

        /// <summary>
        /// This method is to buy products considering system is having Festival Promotions. 
        /// </summary>
        public void BuyProductsWithFestivalPromotions()
        {

            Console.WriteLine("Buy  products with Festival promotions \n");
            var products = _productRepository.GetAll();
            var lstFestivalOffers = _promotionRepository.GetFestivalPromotions().ToList();


            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 2);

            cart.AddPromotions(lstFestivalOffers);

            cart.GetTotal();

        }


        /// <summary>
        /// This method is to buy products considering zero impact
        /// </summary>
        public void BuyProducts()
        {

            var products = _productRepository.GetAll();


            ShoppingCartModel cart = new ShoppingCartModel();

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 2);

            cart.GetTotal();

        }

    }
}
