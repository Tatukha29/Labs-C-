using System;
using System.Collections.Generic;
using Banks.Classes;

namespace Banks.Services
{
    public interface ICentralBank
    {
        Bank AddBank(string name, double percentDebit, double percentCredit, double limitDebit, double limitDeposit);
        void AddTrans(Transaction transaction);
        void CancelTransaction(Guid id);
        Client CreateClient(string firstName, string lastName, string address = "", string passport = "");
        Client CreateClientAddress(Client client, string address);
        Client CreateClientPassport(Client client, string passport);
        void AddClientBank(Client client, Bank bank);
        Debit CreateDebitAccountClient(Bank bank, Client client, double money);
        Deposit CreateDepositAccountClient(Bank bank, Client client, double money);
        Credit CreateCreditAccountClient(Bank bank, Client client, double money);
        List<Bank> AllCountPercent(DateTime startTime, DateTime finalTime);
        void ChangePercentDebitAccount(Bank bank, double newPercent);
        void ChangePercentCreditAccount(Bank bank, double newPercent);
        void ChangePercentDepositAccount(Bank bank, int count, int newPercent, int newMoney);
    }
}