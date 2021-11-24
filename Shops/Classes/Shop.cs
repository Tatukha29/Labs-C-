using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Shops.Classes
{
    public class Shop
    {
        public Shop(string name, string address)
        {
            Id = new GenericId().Id;
            Name = name;
            Address = address;
            Products = new List<Product>();
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
        public List<Product> Products { get; }
    }
}