using System;
using System.Data.Common;
using Banks.Tools;

namespace Banks.Classes
{
    public class Debit : BankAccount
    {
        public Debit(double money, double limit, double percent)
            : base(money, limit, percent)
        {
        }

        public override Transaction WithdrawCash(Bank bank, Client client, BankAccount bankAccount, double money)
        {
            throw new BanksException("Sorry, you cannot withdraw money from the deposit account");
        }
    }
}