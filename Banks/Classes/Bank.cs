using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank : IObservable
    {
        public Bank(string name, double percentDebit, double percentCredit, double limitDebit, double limitDeposit)
        {
            Id = Guid.NewGuid();
            Name = name;
            PercentDebit = percentDebit;
            PercentCredit = percentCredit;
            LimitDebit = limitDebit;
            LimitDeposit = limitDeposit;
            Messeges = new List<IObserver>();
            PercentsDepositAccounts = new Dictionary<int, int>();
            BankAccounts = new List<BankAccount>();
            Clients = new List<Client>();
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public double PercentDebit { get; set; }
        public double PercentCredit { get; set; }
        public double LimitDebit { get; }
        public double LimitDeposit { get; }
        public List<Client> Clients { get; }
        public List<BankAccount> BankAccounts { get; }
        public List<IObserver> Messeges { get; }
        public Dictionary<int, int> PercentsDepositAccounts { get; }

        public void AddPercentDepositAccount(int money, int percent)
        {
            PercentsDepositAccounts.Add(money, percent);
        }

        public void Attach(IObserver observer)
        {
            this.Messeges.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this.Messeges.Remove(observer);
        }

        public void Notify()
        {
            foreach (BankAccount bankAccounts in BankAccounts)
            {
                bankAccounts.Update(this);
            }
        }

        public void CountPercent(int amountMonth)
        {
            foreach (BankAccount bankAccount in BankAccounts)
            {
                if (bankAccount is Debit)
                {
                    bankAccount.Money += ((bankAccount.Money / 100) * bankAccount.Percent) * amountMonth;
                }

                if (bankAccount is Deposit)
                {
                    bankAccount.Money += ((bankAccount.Money / 100) * bankAccount.Percent) * amountMonth;
                }

                if (bankAccount is Credit)
                {
                    var credit = (Credit)bankAccount;
                    credit.OwesMoney += ((credit.OwesMoney / 100) * bankAccount.Percent) * amountMonth;
                }
            }
        }
    }
}