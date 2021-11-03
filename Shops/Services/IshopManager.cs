using Shops.Classes;

namespace Shops.Services
{
    public interface IshopManager
    {
        Shop AddShop(string name, string address);
        Product RegisterProduct(string name);
        Product AddProduct(Product product, Shop shop, int price, int count);
        void ChangeProductPrice(Shop shop, Product product, int price);
        Shop FindMinProductPrice(string productName);
        Shop BuyProduct(Person person, Shop shop, string productName, int count);
        void DeliveryProduct(Person person, string productName, int count);
    }
}