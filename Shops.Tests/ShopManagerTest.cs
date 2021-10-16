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
        public void AddProductShop()
        {
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Product product = _shopManager.RegisterProduct("banana");
            Product product2 = _shopManager.AddProduct(product, shop, 30, 5);
            Assert.Contains(product2, shop.Products);
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
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Shop shop2 = _shopManager.AddShop("Perekrestok", "Lenina 13");
            Product prod = _shopManager.RegisterProduct("banana");
            Product product = _shopManager.AddProduct(prod, shop, 50, 5);
            Product product2 = _shopManager.AddProduct(prod, shop2, 50, 5);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.FindMinProduct("banana", 6);
            });
        }

        [Test]
        public void BuyMinPriceProduct_ThrowExeption()
        {
            Person person = new Person("Alan", 1000);
            Shop shop = _shopManager.AddShop("Diksi", "Lenina 13");
            Shop shop2 = _shopManager.AddShop("Perekrestok", "Lenina 13");
            Product prod = _shopManager.RegisterProduct("banana");
            Product product = _shopManager.AddProduct(prod, shop, 20, 5);
            Product product2 = _shopManager.AddProduct(prod, shop2, 10, 5);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.DeliveryProduct(person, "banana", 6);
            });
        }
    }
}