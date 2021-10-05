using System;
using System.Collections.Generic;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Product> _allproducts = new List<Product>();
        private List<Shop> _shops = new List<Shop>();
        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            var product = new Product(name);
            product.Count = int.MaxValue;
            _allproducts.Add(product);
            return product;
        }

        public Product AddProduct(Product product, Shop shop, int price, int count)
        {
            bool test = false;
            bool test2 = false;
            foreach (Product prod in _allproducts)
            {
                if (prod.Id == product.Id)
                {
                    test = true;
                }
            }

            if (test == false)
            {
                throw new ShopException("Invalid product name");
            }

            if (test == true)
            {
                foreach (Product newProd in shop.Products)
                {
                    if (newProd.Id == product.Id)
                    {
                        test2 = true;
                        newProd.Count += count;
                        return newProd;
                    }
                }
            }

            if (test2 == false)
            {
                Product newProduct = new Product(product.Name);
                newProduct.Count += count;
                newProduct.Price = price;
                shop.Products.Add(newProduct);
                return newProduct;
            }

            return null;
        }

        public void ChangeProductPrice(Shop shop, Product product, int price)
        {
            foreach (Product prod in shop.Products)
            {
                if (prod.Id == product.Id)
                {
                    prod.Price = price;
                }
            }
        }

        public int FindMinProduct(string productName, int count)
        {
            int minPrice = int.MaxValue;
            int shopId = -100;
            foreach (Shop shop in _shops)
            {
                foreach (Product prod in shop.Products)
                {
                    if (productName == prod.Name && prod.Price <= minPrice && prod.Count >= count)
                    {
                        minPrice = prod.Price;
                        shopId = shop.Id;
                    }
                }
            }

            if (minPrice != int.MaxValue && shopId != -100)
            {
                return shopId;
            }

            throw new ShopException("Invalid min product");
        }

        public Shop BuyProduct(Person person, Shop shop, string productName, int count)
        {
            foreach (Product product in shop.Products)
            {
                if (product.Name == productName && person.Money >= product.Price * count)
                {
                    person.Money -= product.Price * count;
                    product.Count -= count;
                    return shop;
                }
            }

            throw new ShopException("No money or no product");
        }

        public Shop DeliveryProduct(Person person, string productName, int count)
        {
            int shopId = FindMinProduct(productName, count);
            foreach (Shop newShop in _shops)
            {
                if (newShop.Id == shopId)
                {
                    foreach (Product newProd in newShop.Products)
                    {
                        if (newProd.Name == productName && person.Money >= newProd.Price * count)
                        {
                            newProd.Count -= count;
                            person.Money -= newProd.Price * count;
                            return newShop;
                        }
                    }
                }
            }

            throw new ShopException("Invalid delivery");
        }
    }
}