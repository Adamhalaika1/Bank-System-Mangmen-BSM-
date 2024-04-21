using Bankbank.DataContext;
using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Bankbank.Entities.Account;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Bankbank.Models
{
    class Employee : User, IUserReader 
    {
        private readonly AppDbContext _context;

        public Employee(AppDbContext context)
        {
            _context = context;
        }

        public override void ReadUser()
        {

                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter the User Email you want to read customer or press Enter to B to Back: ");
                    string emailRead = Back();
                    if (emailRead == null)
                        return; // User pressed 'b' to go back
                    var user = _context.Users.FirstOrDefault(u => u.Email == emailRead);
                    if (user == null)
                    {
                        Console.WriteLine("Customer not found.");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Console.WriteLine("Customer Details:");
                        Console.WriteLine($"First Name: {user.FirstName}");
                        Console.WriteLine($"Last Name: {user.LastName}");
                        Console.WriteLine($"Date of Birth: {user.DateOfBirth}");
                        Console.WriteLine($"Email: {user.Email}");
                        Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                        Console.WriteLine($"Address: {user.Address}");
                        Console.WriteLine($"CustomerType: {user.CustomerType}");
                        Console.WriteLine($"City: {user.City}");
                        Console.WriteLine($"User Role: {user.UserType}");
                        Console.WriteLine();
                        Console.Write("Press Any key to skip ..... ");
                        string Skip = Console.ReadLine();
                    }
                }
            }
        }

        public override void ReadAllUsers()
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
                Console.Write("press 'b' to go back  ");
                Back();

            }
        }

        public void CreateAccount()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the Customer Email you want to Add Account: ");
                string emailCreate = Console.ReadLine();
                var user = context.Users.FirstOrDefault(u => u.Email == emailCreate);

                if (user == null)
                {
                    Console.WriteLine("User not found. Please check the email address.");
                    return;
                }

                Console.WriteLine("Please choose an account type:");
                Console.WriteLine("0. Current account");
                Console.WriteLine("1. Savings account");
                Console.WriteLine("2. Checking account");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Invalid choice. Please select a valid account type.");
                    return;
                }

                var selectedAccountType = (AccountTypes)choice;

                if (user.Account == null)
                {
                    user.Account = new List<Account>();
                }
                var accountAvailable = user.Account.FirstOrDefault(a => a.AccountType == selectedAccountType.ToString());

                if (accountAvailable!=null)
                {
                    Console.WriteLine("You already have an account of this type. Account not added.");
                    return;
                }

                Console.WriteLine("You have selected: " + selectedAccountType);
                DateTime dateTime = DateTime.Now;
                decimal currentBalance = 0;

                Account account = new Account(user.Id, selectedAccountType.ToString(), currentBalance, dateTime, AccountStatus.Active);
                user.Account.Add(account);
                context.SaveChanges();
                Console.WriteLine("Account created successfully.");
            }
        }



        public void DeleteAccount()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the Customer Email you want to delete Account: ");
                string emailDelete = Console.ReadLine();
                var user = context.Users.Include(u => u.Id).FirstOrDefault(u => u.Email == emailDelete);
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
                        context.Accounts.Remove(accountToDelete);
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


        public void UpdateAccountByEmail()
        {
            Console.Write("Enter the User Email you want to modify Account: ");
            string email_account = Console.ReadLine();
            Console.WriteLine("Please choose an account type:");
            Console.WriteLine("0. Current account");
            Console.WriteLine("1. Savings account");
            Console.WriteLine("2. checking  account");
            int choice1 = Convert.ToInt32(Console.ReadLine());
            var selectedAccountType = (AccountTypes)choice1;
            Console.WriteLine("You have selected: " + selectedAccountType);

            using (var context = new AppDbContext())
            {
                var user = context.Users.Include(u => u.Account).FirstOrDefault(u => u.Email == email_account);

                if (user != null && user.Account != null && user.Account.Any())
                {
                    Account accountToModify = user.Account.FirstOrDefault(a => a.AccountType == selectedAccountType.ToString());

                    if (accountToModify != null)
                    {
                        UpdateAccount(accountToModify);
                    }
                    else
                    {
                        Console.WriteLine($"Account with account type '{selectedAccountType.ToString()}' not found for the Customer.");
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found or Customerer has no accounts.");
                }
            }
        }
        public void UpdateAccount(Account account)
        {
            Console.WriteLine("Do you want to Close the Account? (Y/N)");
            string userInput = Console.ReadLine();

            if (userInput.ToUpper() == "Y")
            {
                account.AccountStatus=AccountStatus.InActive;
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
