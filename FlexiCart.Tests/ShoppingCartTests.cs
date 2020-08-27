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

    }
}
