using Bankbank.DataContext;
using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bankbank.Entities.Account;

namespace Bankbank.Models
{
    class Employee : Users
    {
        public void ReadUser(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }
                Console.WriteLine("User Details:");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Date of Birth: {user.DateOfBirth}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                Console.WriteLine($"Address: {user.Address}");
                Console.WriteLine($"CustomerType: {user.CustomerType}");
                Console.WriteLine($"City: {user.City}");
                Console.WriteLine($"User Type: {user.UserType}");
            }
        }

        public void ReadAllUsers()
        {
            using (var context = new AppDbContext())
            {
                var users = context.Users.ToList();

                if (users.Count == 0)
                {
                    Console.WriteLine("No users found.");
                    return;
                }

                Console.WriteLine("User Details:");
                Console.WriteLine("User Details:");
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("| First Name  |        Last Name        | Date of Birth    | Email            |");
                Console.WriteLine("-------------------------------------------------------------------------------");

                foreach (var user in users)
                {
                    Console.WriteLine($"| {user.FirstName,-12} |     {user.LastName,-11} |      {user.DateOfBirth,-14} | {user.Email,-16} |");
                }

                Console.WriteLine("-----------------------------------------------------------------");
                // Wait for a key press before closing the console window
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }
        }


        public void CreatAccount(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    Console.WriteLine("Please choose an account type:");
                    Console.WriteLine("0. Current account");
                    Console.WriteLine("1. Savings account");
                    Console.WriteLine("2. Checking account");

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid account type.");
                        return;
                    }
                    var selectedAccountType = (AccountTypes)choice;
                    Console.WriteLine("You have selected: " + selectedAccountType);

                    DateTime dateTime = DateTime.Now;
                    bool isActive = true;
                    decimal currentBalance = 0;
                    if (user.Account == null)
                    {
                        user.Account = new List<Account>();
                    }
                    Account account = new Account(user.Id, selectedAccountType.ToString(), currentBalance, dateTime, isActive);
                    user.Account.Add(account);
                    context.SaveChanges();
                    Console.Clear();
                }
            }
        }
        public void DeleteAccount(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Id).FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    Console.WriteLine("Please choose an account type to delete:");
                    Console.WriteLine("0. Current account");
                    Console.WriteLine("1. Savings account");
                    Console.WriteLine("2. Checking account");

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 2)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid account type.");
                        return;
                    }

                    var selectedAccountType = (AccountTypes)choice;
                    Console.WriteLine("You have selected: " + selectedAccountType);

                    var accountToDelete = user.Account.FirstOrDefault(a => a.AccountType == selectedAccountType.ToString());
                    if (accountToDelete != null)
                    {
                        context.Account.Remove(accountToDelete);
                        context.SaveChanges();
                        Console.WriteLine("Account deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("User does not have an account of this type to delete.");
                    }
                }
                Console.Clear();
            }
        }


        public void ModifyAccountByEmail(string email, string accountType)
        {

            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Account).FirstOrDefault(u => u.Email == email);

                if (user != null && user.Account != null && user.Account.Any())
                {
                    Account accountToModify = user.Account.FirstOrDefault(a => a.AccountType == accountType);

                    if (accountToModify != null)
                    {
                        ModifyAccount(accountToModify);
                    }
                    else
                    {
                        Console.WriteLine($"Account with account type '{accountType}' not found for the Customer.");
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found or Customerer has no accounts.");
                }
            }
        }
        public void ModifyAccount(Account account)
        {

            Console.WriteLine("Enter the Balance");
            decimal currentBalance;

            if (!decimal.TryParse(Console.ReadLine(), out currentBalance))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number for the balance.");
            }
            else
            {
                account.CurrentBalance = currentBalance;
                account.DateClosed = DateTime.Now.AddDays(30);

            }

            Console.WriteLine("Do you want to Close the Account? (Y/N)");
            string userInput = Console.ReadLine();

            if (userInput.ToUpper() == "Y")
            {
                bool noActive = false;
                account.AccountStatus = noActive;
                Console.WriteLine("Account status updated to 'Not Active'.");
            }

            using (var context = new AppDbContext())
            {
                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();
            }
        }



        public void PrintUserAccounts()
        {
            if (currentUser != null && currentUser.Account != null && currentUser.Account.Any())
            {
                Console.WriteLine($"Accounts for {currentUser.FirstName} {currentUser.LastName}:");
                foreach (var account in currentUser.Account)
                {
                    Console.WriteLine($"Account ID: {account.Id}");
                    Console.WriteLine($"Account Type: {account.AccountType}");
                    Console.WriteLine($"Balance: {account.CurrentBalance}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No accounts found for the current user.");
            }
        }


    }
}
