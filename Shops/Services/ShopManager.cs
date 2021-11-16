using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
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
            Product productShop = shop.Products.Find(productShop => productShop.Equals(product));
            productShop.Price = price;
        }

        public Shop MinPriceShopProduct(Product product, int count)
        {
            int minCost = int.MaxValue;
            Shop shopMinPrice = null;
            foreach (Shop shop in _shops)
            {
                Product prod = shop.Products.FirstOrDefault(products => products.Name == product.Name);
                if (prod != null && prod.Price < minCost && prod.Count >= count)
                {
                    minCost = prod.Price;
                    shopMinPrice = shop;
                }
            }

            if (minCost == int.MaxValue && shopMinPrice == null)
            {
                throw new ShopException("Invalid");
            }

            return shopMinPrice;
        }

        public int MinListProductPrice(List<Product> products, Shop shop)
        {
            int receipt = 0;
            foreach (Product product in shop.Products)
            {
                foreach (Product wantedProduct in products)
                {
                    if (product.Id == wantedProduct.Id && product.Count >= wantedProduct.Count)
                    {
                        receipt += product.Price * wantedProduct.Count;
                    }
                }
            }

            return receipt;
        }

        public Shop MinPriceListShopProducts(List<Product> products)
        {
            int check = int.MaxValue;
            Shop shopMinPrice = null;
            foreach (Shop shop in _shops)
            {
                int receipt = MinListProductPrice(products, shop);
                if (receipt < check)
                {
                    check = receipt;
                    shopMinPrice = shop;
                }
            }

            if (shopMinPrice == null)
            {
                throw new ShopException("Shop is not found");
            }

            return shopMinPrice;
        }

        public void BuyProduct(Person person, Shop shop, string productName, int count)
        {
            bool check = false;
            foreach (Product product in shop.Products.Where(product => product.Name == productName && person.GetMoney() >= product.Price * count && product.Count >= count))
            {
                person.CountMoney(product.Price * count);
                product.Count -= count;
                check = true;
            }

            if (check == false)
            {
                throw new ShopException("No money or no product");
            }
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

        public void DeliveryProduct(Person person, Product product, int count)
        {
            Shop shop = MinPriceShopProduct(product, count);
            BuyProduct(person, shop, product.Name, count);
        }

        public void DeliveryListProducts(List<Product> basket, Person person)
        {
            foreach (Product product in basket)
            {
                DeliveryProduct(person, product, product.Count);
            }
        }
    }
}