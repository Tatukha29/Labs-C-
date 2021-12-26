using System;
using System.ComponentModel.DataAnnotations;
using Banks.Classes;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            CentralBank centralBank = new CentralBank("Russian Central Bank");
            Client oldClient = centralBank.CreateClient("Ivan", "Ivanov");
            Bank sberbank = centralBank.AddBank("Sberbank", 1, 2.9, 25000, 100000);
            Bank tinkoff = centralBank.AddBank("Tinkoff", 2, 3.2, 50000, 100000);
            centralBank.AddClientBank(oldClient, tinkoff);
            BankAccount bankAccount = centralBank.CreateCreditAccountClient(tinkoff, oldClient, 10000);
            Console.WriteLine("Write your first name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Write your last name");
            string lastName = Console.ReadLine();
            Console.WriteLine("Write your address(or press enter)");
            string address = Console.ReadLine();
            Console.WriteLine("Write your passport(or press enter)");
            string passport = Console.ReadLine();
            Client client = centralBank.CreateClient(firstName, lastName, address, passport);
            Console.WriteLine("Select the bank where you want to register: " + sberbank.Name + ", " + tinkoff.Name);
            Bank bank = centralBank.Banks.Find(banks => banks.Name == Console.ReadLine());
            centralBank.AddClientBank(client, bank);
            Console.WriteLine("Select the account where you want to register: " +
                              "1) Debit" +
                              "2) Deposit" +
                              "3) Credit" +
                              "Write one number");
            BankAccount card = null;
            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("Enter how much do you want to put in your debit account?");
                int money = int.Parse(Console.ReadLine());
                card = centralBank.CreateDebitAccountClient(bank, client, money);
            }
            else if (Console.ReadLine() == "2")
            {
                Console.WriteLine("Enter how much do you want to put in your deposit account?");
                int money = int.Parse(Console.ReadLine());
                card = centralBank.CreateDepositAccountClient(bank, client, money);
            }
            else if (Console.ReadLine() == "3")
            {
                Console.WriteLine("Enter how much do you want to recieve for your credit account?");
                int money = int.Parse(Console.ReadLine());
                card = centralBank.CreateCreditAccountClient(bank, client, money);
            }

            Console.WriteLine("Do you want to transaction to someone?(y/n)");
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine("How much money do you want to transaction?");
                double moneyTrans = double.Parse(Console.ReadLine());
                Transaction transaction = card.MakeTransaction(client, card, bankAccount, moneyTrans);
            }
            else if (Console.ReadLine() == "n")
            {
                Console.WriteLine("Thanks for you choice. Bye Bye!!!");
            }

            Console.WriteLine("Money in your card");
            Console.WriteLine(card.Money);
        }
    }
}
