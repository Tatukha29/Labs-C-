using System;
using System.Collections.Generic;

namespace Banks.Classes
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _passport;
        private List<BankAccount> _listAccounts;
        public Client(string firstName, string lastName, string address, string passport)
        {
            _firstName = firstName;
            _lastName = lastName;
            _listAccounts = new List<BankAccount>();
            _address = address;
            _passport = passport;
        }

        public ClientBuilder ToBuilder(ClientBuilder clientBuilder)
        {
            clientBuilder.AddFirstName(_firstName);
            clientBuilder.AddLastName(_lastName);
            clientBuilder.AddAddress(_address);
            clientBuilder.AddPassport(_passport);
            return clientBuilder;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _lastName;
        }

        public string GetAddress()
        {
            return _passport;
        }

        public string GetPassport()
        {
            return _passport;
        }

        public string SetAddress(string address)
        {
            _address = address;
            return _address;
        }

        public string SetPassport(string passport)
        {
            _passport = passport;
            return _passport;
        }

        public List<BankAccount> GetListAccount()
        {
            return _listAccounts;
        }

        public List<BankAccount> AddInListAccount(BankAccount bankAccount)
        {
            _listAccounts.Add(bankAccount);
            return _listAccounts;
        }
    }
}