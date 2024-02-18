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
        public string Email { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Balance { get;  set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; private set; }
        public enum UserTypes { Admin, User }
        public UserTypes UserType { get; set; }
        public Account(string email, string name, int age, decimal balance, UserTypes userType)
        {
            Id = nextId++;
            Email = email;
            Name = name;
            Age = age;
            Balance = balance;
            IsActive = true;
            UserType = userType;
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

        public void UpdateInfo(string newEmail)
        {
            try
            {
                if (string.IsNullOrEmpty(newEmail))
                {
                    throw new ArgumentException("Email cannot be empty");
                }

                Email = newEmail;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Warning: " + ex.Message);
            }
        }
    }
}
