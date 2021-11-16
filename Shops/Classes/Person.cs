namespace Shops.Classes
{
    public class Person
    {
        private int _money;

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

        public void CountMoney(int check)
        {
            _money = GetMoney() - check;
        }
    }
}