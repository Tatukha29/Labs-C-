using System.Data.Common;

namespace Shops.Classes
{
    public class GenericId
    {
        private static int _id;

        public GenericId()
        {
        Id = (++_id) + 1000;
        }

        public int Id { get; }
    }
}