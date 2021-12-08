using System;
using System.IO;
using Banks.Tools;

namespace Banks.Classes
{
    public class Deposit : BankAccount
    {
        public Deposit(double money, double limit, double percent)
            : base(money, limit, percent)
        {
        }

        public override Transaction MakeTransaction(Client client, BankAccount bankAccount1, BankAccount bankAccount2, double money)
        {
            throw new BanksException("Sorry, transaction are not available for deposit accounts");
        }

        public override Transaction WithdrawCash(Bank bank, Client client, BankAccount bankAccount, double money)
        {
            throw new BanksException("Sorry, you cannot withdraw money from the deposit account");
        }
    }
}