namespace Shops.Classes
{
    public class Person
    {
        private static int _money;

        public Person(string name, int money)
        {
            Name = name;
            _money = money;
        }

        public string Name { get; }

        public int GetMoney()
        {
            return _money;
        }

        public int CountMoney(int check)
        {
            return _money = GetMoney() - check;
        }
    }
}