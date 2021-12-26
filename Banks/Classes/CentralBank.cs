using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class CentralBank : ICentralBank
    {
        private const int Month = 30;
        public CentralBank(string name)
        {
            Name = name;
            Banks = new List<Bank>();
            Transactions = new List<Transaction>();
        }

        public string Name { get; }
        public List<Bank> Banks { get; }
        public List<Transaction> Transactions { get; }

        public Bank AddBank(string name, double percentDebit, double percentCredit, double limitDebit, double limitDeposit)
        {
            Bank bank = new Bank(name, percentDebit, percentCredit, limitDebit, limitDeposit);
            Banks.Add(bank);
            return bank;
        }

        public void AddTrans(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void CancelTransaction(Guid id)
        {
            Transaction transaction = Transactions.FirstOrDefault(transaction => transaction.Id == id);
            if (transaction == null)
            {
                throw new BanksException("Sorry, no such transaction id or transaction has already been completed");
            }

            transaction.BankAccount1.Money += transaction.Money;
            if (transaction.BankAccount2 != null)
            {
                transaction.BankAccount2.Money -= transaction.Money;
                Transactions.Remove(transaction);
            }

            Transactions.Remove(transaction);
        }

        public Client CreateClient(string firstName, string lastName, string address = "", string passport = "")
        {
            Client client = new Client(firstName, lastName, address, passport);
            return client;
        }

        public Client CreateClientAddress(Client client, string address)
        {
            var builder = new ClientBuilder();
            ClientBuilder clientBuilder = new ClientBuilder();
            clientBuilder.AddFirstName(client.GetFirstName());
            clientBuilder.AddLastName(client.GetLastName());
            clientBuilder.AddAddress(address);
            clientBuilder.AddPassport(client.GetPassport());
            Client newClient = clientBuilder.Build();
            return newClient;
        }

        public Client CreateClientPassport(Client client, string passport)
        {
            var builder = new ClientBuilder();
            ClientBuilder clientBuilder = new ClientBuilder();
            clientBuilder.AddFirstName(client.GetFirstName());
            clientBuilder.AddLastName(client.GetLastName());
            clientBuilder.AddAddress(client.GetAddress());
            clientBuilder.AddPassport(passport);
            Client newClient = clientBuilder.Build();
            return newClient;
        }

        public void AddClientBank(Client client, Bank bank)
        {
            bank.Clients.Add(client);
        }

        public Debit CreateDebitAccountClient(Bank bank, Client client, double money)
        {
            double limit;
            if (client.GetPassport() != null)
            {
                limit = int.MaxValue;
            }
            else
            {
                limit = bank.LimitDebit;
            }

            Debit debit = new Debit(money, limit, bank.PercentDebit);
            bank.BankAccounts.Add(debit);
            client.AddInListAccount(debit);
            return debit;
        }

        public Deposit CreateDepositAccountClient(Bank bank, Client client, double money)
        {
            double limit;
            if (client.GetPassport() != null)
            {
                limit = int.MaxValue;
            }
            else
            {
                limit = bank.LimitDeposit;
            }

            int percent = 0;
            foreach (var dictionary in bank.PercentsDepositAccounts)
            {
                if (money < dictionary.Key)
                {
                    percent = dictionary.Value;
                }
            }

            Deposit deposit = new Deposit(money, limit, percent);
            bank.BankAccounts.Add(deposit);
            client.AddInListAccount(deposit);
            return deposit;
        }

        public Credit CreateCreditAccountClient(Bank bank, Client client, double money)
        {
            double limit;
            if (client.GetPassport() != null)
            {
                limit = int.MaxValue;
            }
            else
            {
                limit = bank.LimitDebit;
            }

            Credit credit = new Credit(money, limit, bank.PercentCredit, money);
            bank.BankAccounts.Add(credit);
            client.AddInListAccount(credit);
            return credit;
        }

        public List<Bank> AllCountPercent(DateTime startTime, DateTime finalTime)
        {
            int days = (int)(finalTime.Date - startTime.Date).TotalDays;
            int amountMonth = days / Month;
            foreach (Bank bank in Banks)
            {
                bank.CountPercent(amountMonth);
            }

            return Banks;
        }

        public void ChangePercentDebitAccount(Bank bank, double newPercent)
        {
            bank.PercentDebit = newPercent;
            foreach (var bankAccount in bank.BankAccounts)
            {
                if (bankAccount is Debit)
                {
                    bankAccount.Percent = newPercent;
                }
            }
        }

        public void ChangePercentCreditAccount(Bank bank, double newPercent)
        {
            bank.PercentCredit = newPercent;
            foreach (var bankAccount in bank.BankAccounts)
            {
                if (bankAccount is Credit)
                {
                    bankAccount.Percent = newPercent;
                }
            }
        }

        public void ChangePercentDepositAccount(Bank bank, int count, int newPercent, int newMoney)
        {
            for (int i = 0; i < bank.PercentsDepositAccounts.Count; i++)
            {
                if (bank.PercentsDepositAccounts[i] == count)
                {
                    bank.PercentsDepositAccounts.Remove(bank.PercentsDepositAccounts[i]);
                    bank.AddPercentDepositAccount(newMoney, newPercent);
                }
            }
        }
    }
}