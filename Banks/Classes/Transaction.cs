using System;
using Banks.Tools;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(BankAccount bankAccount1, BankAccount bankAccount2, double money)
        {
            Id = Guid.NewGuid();
            BankAccount1 = bankAccount1;
            BankAccount2 = bankAccount2;
            Money = money;
        }

        public Guid Id { get; }
        public BankAccount BankAccount1 { get; }
        public BankAccount BankAccount2 { get; }
        public double Money { get; }
    }
}