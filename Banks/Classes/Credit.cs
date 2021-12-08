using System;
using Banks.Tools;

namespace Banks.Classes
{
    public class Credit : BankAccount
    {
        public Credit(double money, double limit, double percent, double owesMoney)
            : base(money, limit, percent)
        {
            OwesMoney = owesMoney;
        }

        public double OwesMoney { get; set; }
    }
}