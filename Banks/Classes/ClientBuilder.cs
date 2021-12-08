using System;
using System.Collections.Generic;

namespace Banks.Classes
{
    public class ClientBuilder
    {
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _passport;
        private List<BankAccount> _listAccounts;

        // public ClientBuilder(Client client, string firstName, string lastName, string address, string passport)
        // {
        //     _firstName = firstName;
        //     _lastName = lastName;
        //     _listAccounts = new List<BankAccount>();
        //     _address = address;
        //     _passport = passport;
        // }
        public ClientBuilder AddFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public ClientBuilder AddLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder AddPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public ClientBuilder AddListAccounts(List<BankAccount> bankAccounts)
        {
            _listAccounts = bankAccounts;
            return this;
        }

        public Client Build()
        {
            Client finalClient = new Client(_firstName, _lastName, _address, _passport);
            return finalClient;
        }
    }
}