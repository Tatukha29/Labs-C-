using System.Data.Common;

namespace Shops.Classes
{
    public class Product
    {
        public Product(string name, int count)
        {
            Id = new GenericId().Id;
            Name = name.ToLower();
            Count = count;
        }

        public int Id { get; }
        public string Name { get; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}