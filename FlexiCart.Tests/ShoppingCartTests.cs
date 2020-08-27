using FlexiCart.Model.Entities;
using FlexiCart.Service;
using FlexiCart.Service.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCart.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {

        [Test]
        public void SampleTest()
        {

            Assert.IsTrue(true);

        }

        /// <summary>
        /// Add a Test method using AAA model in TDD and try to simply perform adding products to Cart and get total
        /// </summary>
        [Test]
        public void BuySimpleProducts()
        {
            //1 Arrange
            // setup inputs 

            Console.WriteLine("Perform Scenario1");
            IRepository<Product> productRepository = new ProductRepository( new FakeDbContext());
            var products = productRepository.GetAll();

            /* 
            Scenario

            1 *A 50
            1 * B 30
            1 * C 20 
            Total =100
             */


            decimal expected = 100;
            decimal actual = 0;// 

            //2 Act

            //  initialize cart

            ShoppingCartModel cart = new ShoppingCartModel();
            // addproducts

            cart.AddProduct(products.First(p => p.SKU == "A"), 1);
            cart.AddProduct(products.First(p => p.SKU == "B"), 1);
            cart.AddProduct(products.First(p => p.SKU == "C"), 1);
             

            // setup promotions to cart 
            // need to introduce a way to handle promotions to Cart and robust Product handling mechanism
             

            actual = cart.GetTotal();


            //3 Assert

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Add a Test method get Active Promotions and apply to Cart with products
        /// </summary>
        [Test]
        public void BuyProductsWithActivePromotions()
        {
            //1 Arrange
            // setup inputs 

            Console.WriteLine("Perform Scenario2");
            IRepository<Product> productRepository = new ProductRepository(new FakeDbContext());
            var products = productRepository.GetAll();

            IPromotionRepository<Promotion> promotionRepository = new PromotionRepository(new FakeDbContext());
            var lstActivePromotions = promotionRepository.GetActiveRules().ToList();

            /* 
            Scenario

            Setup unitPrices
              Set Active Promotions
              3 of A's for 130
              2 of B's for  45
              C & D for 30

            5 *A 130 + 2*50
            5 * B 45 +45 + 30
            1 * C 20 
            Total = 370

             */


            decimal expected = 370;
            decimal actual = 0;// 

            //2 Act

            //  initialize cart

            ShoppingCartModel cart = new ShoppingCartModel();
            // addproducts

            cart.AddProduct(products.First(p => p.SKU == "A"), 5);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 1);
            cart.AddPromotions(lstActivePromotions); 

            // setup promotions to cart 
            // need to introduce a way to handle promotions to Cart and robust Product handling mechanism


            actual = cart.GetTotal();


            //3 Assert

            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Add a Test method get Active Promotions and apply to Cart with products
        /// </summary>
        [Test]
        public void BuyProductsWithGroupPromotions()
        {
            //1 Arrange
            // setup inputs 

            Console.WriteLine("Perform Scenario3");
            IRepository<Product> productRepository = new ProductRepository(new FakeDbContext());
            var products = productRepository.GetAll();

            IPromotionRepository<Promotion> promotionRepository = new PromotionRepository(new FakeDbContext());
            var lstActivePromotions = promotionRepository.GetActiveRules().ToList();

            /* 
            Scenario

            Setup unitPrices
              Set Active Promotions
              3 of A's for 130
              2 of B's for  45
              C & D for 30

            5 *A 130 + 2*50
            5 * B 45 +45 + 30
            1 * C 20 
            Total = 370

             */


            decimal expected = 280;
            decimal actual = 0;// 

            //2 Act

            //  initialize cart

            ShoppingCartModel cart = new ShoppingCartModel();
            // addproducts

            cart.AddProduct(products.First(p => p.SKU == "A"), 3);
            cart.AddProduct(products.First(p => p.SKU == "B"), 5);
            cart.AddProduct(products.First(p => p.SKU == "C"), 1);
            cart.AddProduct(products.First(p => p.SKU == "D"), 1);
            cart.AddPromotions(lstActivePromotions);
            // setup promotions to cart 
            // need to introduce a way to handle promotions to Cart and robust Product handling mechanism


            actual = cart.GetTotal();


            //3 Assert

            Assert.AreEqual(actual, expected);
        }


        


    }
}
