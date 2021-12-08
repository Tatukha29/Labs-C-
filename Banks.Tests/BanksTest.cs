using System;
using System.Linq;
using Banks.Classes;
using Banks.Services;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private ICentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank("Moscow Central Bank");
        }
        
        [TestCase(10000, 500)]
        public void MakeTransaction(double money, double money2)
        {
            Bank sberbank = _centralBank.AddBank("Sberbank", 0, 2.9, 25000, 100000);
            Bank tinkoff = _centralBank.AddBank("Tinkoff", 2, 3.2, 50000, 100000);
            Client client1 = _centralBank.CreateClient("Tanya", "Rebrova");
            Client client2 = _centralBank.CreateClient("Ivan", "Ivanov");
            _centralBank.AddClientBank(client1, sberbank);
            _centralBank.AddClientBank(client2, tinkoff);
            BankAccount account1 = _centralBank.CreateDebitAccountClient(sberbank, client1, money);
            BankAccount account2 = _centralBank.CreateCreditAccountClient(tinkoff, client2, money2);
            Transaction transaction = account1.MakeTransaction(client1, account1, account2, 1000);
            Assert.AreEqual(9000, account1.Money);
            Assert.AreEqual(1500, account2.Money);
        }

        [TestCase(10000, 500)]
        public void CancelTransaction(double money, double money2)
        {
            Bank sberbank = _centralBank.AddBank("Sberbank", 0, 2.9, 25000, 100000);
            Bank tinkoff = _centralBank.AddBank("Tinkoff", 2, 3.2, 50000, 100000);
            Client client1 = _centralBank.CreateClient("Tanya", "Rebrova");
            Client client2 = _centralBank.CreateClient("Ivan", "Ivanov");
            _centralBank.AddClientBank(client1, sberbank);
            _centralBank.AddClientBank(client2, tinkoff);
            BankAccount account1 = _centralBank.CreateDebitAccountClient(sberbank, client1, money);
            BankAccount account2 = _centralBank.CreateCreditAccountClient(tinkoff, client2, money2);
            Transaction transaction = account1.MakeTransaction(client1, account1, account2, 1000);
            _centralBank.AddTrans(transaction);
            _centralBank.CancelTransaction(transaction.Id);
            Assert.AreEqual(10000, account1.Money);
        }
        
        [TestCase(10000, 500)]
        public void ToSeeHowMuchMoneyInMonth(double money, double money2)
        {
            Bank sberbank = _centralBank.AddBank("Sberbank", 1, 2.9, 25000, 100000);
            Bank tinkoff = _centralBank.AddBank("Tinkoff", 2, 3.2, 50000, 100000);
            Client client1 = _centralBank.CreateClient("Tanya", "Rebrova");
            Client client2 = _centralBank.CreateClient("Ivan", "Ivanov");
            _centralBank.AddClientBank(client1, sberbank);
            _centralBank.AddClientBank(client2, tinkoff);
            BankAccount account1 = _centralBank.CreateDebitAccountClient(sberbank, client1, money);
            BankAccount account2 = _centralBank.CreateCreditAccountClient(tinkoff, client2, money2);
            _centralBank.AllCountPercent(new DateTime(2021, 11, 1), new DateTime(2021, 12, 1));
            Assert.AreEqual(516, ((Credit)account2).OwesMoney);
            Assert.AreEqual(10100, account1.Money);
        }
    }
}