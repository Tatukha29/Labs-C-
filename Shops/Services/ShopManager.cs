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

        public Product RegisterProduct(string name)
        {
            var product = new Product(name);
            product.Count = int.MaxValue;
            _allproducts.Add(product);
            return product;
        }

        public Product AddProduct(Product product, Shop shop, int price, int count)
        {
            foreach (Product shopprod in _allproducts.Where(products => products.Id == product.Id)
                .SelectMany(products => shop.Products.Where(shopprod => product == shopprod)))
            {
                shopprod.Count += count;
                return shopprod;
            }

            Product newProduct = new Product(product.Name);
            newProduct.Count += count;
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

        public Shop FindMinProduct(string productName)
        {
            int minCost = int.MaxValue;
            Shop shop = null;
            foreach (Shop shops in _shops)
            {
                foreach (Product products in shops.Products)
                {
                    if (products.Name == productName && products.Price < minCost)
                    {
                        minCost = products.Price;
                        shop = shops;
                    }
                }
            }

            return shop;
        }

        public Shop BuyProduct(Person person, Shop shop, string productName, int count)
        {
            foreach (Product product in shop.Products.Where(product =>
                product.Name == productName && person.Money >= product.Price * count))
            {
                person.Money -= product.Price * count;
                product.Count -= count;
                return shop;
            }

            throw new ShopException("No money or no product");
        }

        public void DeliveryProduct(Person person, string productName, int count)
        {
            foreach (var shops in _shops)
            {
                foreach (var products in shops.Products)
                {
                    if (products.Count >= count && person.Money >= products.Count * products.Price)
                    {
                        products.Count -= count;
                        person.Money -= count * products.Price;
                    }
                    else
                    {
                        throw new ShopException("Something went wrong");
                    }
                }
            }
        }
    }
}