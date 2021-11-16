using System.Collections.Generic;
using Shops.Classes;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop AddShop(string name, string address);
        Product RegisterProduct(string name, int count);
        Product AddProduct(Product product, Shop shop, int price, int count);
        void ChangeProductPrice(Shop shop, Product product, int price);
        Shop MinPriceShopProduct(Product product, int count);
        Shop MinPriceListShopProducts(List<Product> products);
        void BuyProduct(Person person, Shop shop, string productName, int count);
        void BuyListProducts(List<Product> basket, Shop shop, Person person);
        void DeliveryProduct(Person person, Product product, int count);
        void DeliveryListProducts(List<Product> basket, Person person);
    }
}