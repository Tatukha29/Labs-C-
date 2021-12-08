using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public abstract class BankAccount : IObserver
    {
        public BankAccount(double money, double limit, double percent)
        {
            Id = Guid.NewGuid();
            Money = money;
            Limit = limit;
            Percent = percent;
        }

        public Guid Id { get; }
        public double Money { get; set; }
        public double Limit { get; set; }
        public double Percent { get; set; }

        public void Update(IObservable observable)
        {
        }

        public virtual Transaction MakeTransaction(Client client, BankAccount bankAccount1, BankAccount bankAccount2, double money)
        {
            if (client.GetPassport() == string.Empty && bankAccount1.Limit < money)
            {
                throw new BanksException("Sorry, you are not fully registered client");
            }

            if (bankAccount1 is Debit && bankAccount1.Money < 0)
            {
                throw new BanksException("Sorry, insufficient funds");
            }

            Transaction transaction = new Transaction(bankAccount1, bankAccount2, money);
            bankAccount1.Money -= money;
            bankAccount2.Money += money;
            return transaction;
        }

        public virtual Transaction WithdrawCash(Bank bank, Client client, BankAccount bankAccount, double money)
        {
            Transaction transaction = new Transaction(bankAccount, null, money);
            if (bank.Clients.FirstOrDefault(clients => clients == client) == null)
            {
                throw new BanksException("Sorry, client not found this bank");
            }

            if (client.GetListAccount().FirstOrDefault(bankAccounts => bankAccounts == bankAccount) == null)
            {
                throw new BanksException("Sorry, client don't have this bank account");
            }

            bankAccount.Money -= money;
            return transaction;
        }

        public virtual void TopUpCash(Bank bank, Client client, BankAccount bankAccount, double money)
        {
            if (bank.Clients.FirstOrDefault(clients => clients == client) == null)
            {
                throw new BanksException("Sorry, client not found this bank");
            }

            if (client.GetListAccount().FirstOrDefault(bankAccounts => bankAccounts == bankAccount) == null)
            {
                throw new BanksException("Sorry, client don't have this bank account");
            }

            bankAccount.Money += money;
        }
    }
}