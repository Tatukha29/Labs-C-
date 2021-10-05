using NUnit.Framework;
using Shops.Classes;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        private ShopManager _shopManager;
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void AddProduct_ThrowExeption()
        {
            Assert.Catch<ShopException>(() => { 
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product product = _shopManager.RegisterProduct("banana");
            Product product2 = new Product("pencil");
            _shopManager.AddProduct(product2, shop, 30, 5);
            });
        }
        
        [Test]
        public void ChangeProductPrice()
        {
                int newPrice = 50;
                Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
                Product product = _shopManager.RegisterProduct("banana");
                Product product2 = _shopManager.AddProduct(product, shop, 30, 5);
                _shopManager.ChangeProductPrice(shop, product2, newPrice);
                Assert.AreEqual(newPrice, product2.Price);
        }

        [Test]
        public void FindMinProduct_ThrowExeption()
        {
            Assert.Catch<ShopException>(() =>
            {
                Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
                Shop shop2 = _shopManager.AddShop("Perekrestok", "Lenina 13");
                Shop shop3 = _shopManager.AddShop("Pyatyerochka", "Lenina 13");
                Product prod = _shopManager.RegisterProduct("banana");
                Product product = _shopManager.AddProduct(prod, shop, 50, 5);
                Product product2 = _shopManager.AddProduct(prod, shop2, 50, 5);
                Product product3 = _shopManager.AddProduct(prod, shop3, 50, 5);
                _shopManager.FindMinProduct("candy", 1);
            });
        }

        [Test]
        public void BuyMinPriceProduct_ThrowExeption()
        {
            Assert.Catch<ShopException>(() =>
            {
                Person person = new Person("Alan", 1000);
                Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
                Shop shop2 = _shopManager.AddShop("Perekrestok", "Lenina 13");
                Shop shop3 = _shopManager.AddShop("Pyatyerochka", "Lenina 13");
                Product prod = _shopManager.RegisterProduct("banana");
                Product product = _shopManager.AddProduct(prod, shop, 20, 5);
                Product product2 = _shopManager.AddProduct(prod, shop2, 10, 5);
                Product product3 = _shopManager.AddProduct(prod, shop3, 15, 5);
                _shopManager.DeliveryProduct(person, "banana", 6);
            });
        }
    }
}