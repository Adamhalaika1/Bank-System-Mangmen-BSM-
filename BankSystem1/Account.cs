using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem1
{
    public class Account
    {
        public int Id { get; }
        private static int nextId = 1;
        public string Role { get; set; }
        public string Email { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Balance { get; private set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; private set; }

        public Account(string email, string name, int age, decimal balance,string role)
        {
            Id = nextId++;
            Email = email;
            Name = name;
            Age = age;
            Balance = balance;
            IsActive = true;
            Role = role;


        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public void ToggleActiveStatus()
        {
            IsActive = !IsActive;
        }

        public void UpdateInfo(string newEmail, string newPhoneNumber)
        {
            Email = newEmail;
            PhoneNumber = newPhoneNumber;
        }
    }
}
