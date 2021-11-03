using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Product> _allproducts = new ();
        private List<Shop> _shops = new ();

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name, int count)
        {
            var product = new Product(name, count);
            product.Count = count;
            _allproducts.Add(product);
            return product;
        }

        public Product AddProduct(Product product, Shop shop, int price, int count)
        {
            if (!_allproducts.Contains(product))
            {
                throw new ShopException("No product");
            }

            if (shop.Products.Contains(product))
            {
                Product prod = shop.Products.Find(prod => prod.Equals(product));
                prod.Count += count;
                return prod;
            }

            Product newProduct = new Product(product.Name, count);
            newProduct.Price = price;
            shop.Products.Add(newProduct);
            return newProduct;
        }

        public void ChangeProductPrice(Shop shop, Product product, int price)
        {
            foreach (Product prod in shop.Products.Where(prod => prod.Id == product.Id))
            {
                prod.Price = price;
            }
        }

        public Shop FindMinProductPrice(string productName, int count)
        {
            int minCost = int.MaxValue;
            Shop shopMinPrice = null;
            foreach (Shop shop in _shops)
            {
                foreach (Product products in shop.Products.Where(products => products.Name == productName && products.Price < minCost && products.Count >= count))
                {
                    minCost = products.Price;
                    shopMinPrice = shop;
                }
            }

            if (minCost == int.MaxValue && shopMinPrice == null)
            {
                throw new ShopException("Invalid");
            }

            return shopMinPrice;
        }

        public void BuyProduct(Person person, Shop shop, string productName, int count)
        {
            foreach (Product product in shop.Products.Where(product => product.Name == productName && person.GetMoney() >= product.Price * count))
            {
                person.CountMoney(product.Price * count);
                product.Count -= count;
            }

            throw new ShopException("No money or no product");
        }

        public void BuyListProducts(List<Product> basket, Shop shop, Person person)
        {
            foreach (Product product in basket)
            {
                foreach (Product prod in shop.Products.Where(prod => prod.Name == product.Name))
                {
                    BuyProduct(person, shop, product.Name, product.Count);
                }
            }
        }

        public void DeliveryProduct(Person person, string productName, int count)
        {
            Shop shop = FindMinProductPrice(productName, count);
            BuyProduct(person, shop, productName, count);
        }
    }
}