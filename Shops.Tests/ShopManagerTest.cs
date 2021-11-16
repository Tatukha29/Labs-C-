using System.Collections.Generic;
using NUnit.Framework;
using Shops.Classes;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        private IShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [TestCase(2)]
        public void AddProductShop(int count)
        {
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product product = _shopManager.RegisterProduct("banana", count);
            Product product2 = _shopManager.AddProduct(product, shop, 30, 5);
            Assert.Contains(product2, shop.Products);
        }

        [TestCase(50, 2)]
        public void ChangeProductPrice(int newPrice, int count)
        {
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product product = _shopManager.RegisterProduct("banana", count);
            Product product2 = _shopManager.AddProduct(product, shop, 30, 5);
            _shopManager.ChangeProductPrice(shop, product2, newPrice);
            Assert.AreEqual(newPrice, product2.Price);
        }

        [Test]
        public void FindMinPriceShopProduct()
        {
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Shop shop2 = _shopManager.AddShop("Perekrestok", "Lenina 13");
            Product prod = _shopManager.RegisterProduct("banana", 100);
            Product product1 = _shopManager.AddProduct(prod, shop, 100, 5);
            Product product2 = _shopManager.AddProduct(prod, shop2, 30, 6);
            Assert.AreEqual(_shopManager.MinPriceShopProduct(prod, 2), shop2);
        }

        [TestCase(2)]
        public void BuyProductShop_ThrowException(int count)
        {
            var person = new Person("Alan", 1000);
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product prod = _shopManager.RegisterProduct("banana", count);
            _shopManager.AddProduct(prod, shop, 20, 5);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.DeliveryProduct(person, prod, 6);
            });
        }

        [TestCase(3,4,5,5)]
        public void BuyListProducts(int countShop, int countShop2, int countPerson, int countPerson2)
        {
            var person = new Person("Alan", 1000);
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product prod = _shopManager.RegisterProduct("banana", countShop);
            _shopManager.AddProduct(prod, shop, 20, 1);
            Product prod2 = _shopManager.RegisterProduct("milk", countShop2);
            _shopManager.AddProduct(prod2, shop, 100, 1);
            Product basketProduct = new Product(prod.Name, countPerson);
            Product basketProduct2 = new Product(prod2.Name, countPerson2);
            List<Product> basket = new List<Product>() {basketProduct, basketProduct2};
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyListProducts(basket, shop, person);
            });
            
        }
    }
}