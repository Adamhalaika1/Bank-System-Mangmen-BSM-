using Bankbank.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
namespace Bankbank.Entities
{
    public enum AccountTypes
    {
        CurrentAccount,
        SavingAccount,
        checkingAccount,
    }
    public enum AccountStatus
    {
        Active,
        InActive
    }
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal? CurrentBalance { get; set; }

        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }

        public static Account currentAccount;
        public Account(int customerId, string accountType, decimal currentBalance, DateTime dateTime, AccountStatus accountStatus)
        {
            CustomerId = customerId;
            AccountType = accountType;
            CurrentBalance = currentBalance;
            AccountStatus = accountStatus;
            DateOpened = dateTime;
        }
        public Account()
        {
            // Parameterless constructor for Entity Framework
        }



         // Method to choose an account type
    public static Account ChooseAccountType(int userId)
    {
        var index = 0;
        List<Account> loggedUserAccounts;
        using (var context = new AppDbContext())
        {
            loggedUserAccounts = context.Accounts.Where(item => item.CustomerId == userId).ToList();
        }

        if (loggedUserAccounts == null || loggedUserAccounts.Count == 0)
        {
            Console.WriteLine("Sorry, you need to create an account");
            return null;
        }

        Console.WriteLine("Please choose an account type:");
        foreach (var accountItem in loggedUserAccounts)
        {
            Console.WriteLine(index + ". " + accountItem.AccountType);
            index++;
        }

            int choice1 = Convert.ToInt32(Console.ReadLine());
            var selectedAccountType = (AccountTypes)choice1;
            var selectedUserAccount = loggedUserAccounts.Where(acc => acc.AccountType == selectedAccountType.ToString()).FirstOrDefault();

            return selectedUserAccount;
    }
        public void DepositAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }

            CurrentBalance += amount;

            using (var context = new AppDbContext())
            {
                var accountToUpdate = context.Accounts.Find(Id);
                if (accountToUpdate != null)
                {
                    accountToUpdate.CurrentBalance = CurrentBalance;
                    context.SaveChanges();
                }
            }
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Wihdraw amount must be greater than zero.");
            }

            CurrentBalance -= amount;

            using (var context = new AppDbContext())
            {
                var accountToUpdate = context.Accounts.Find(Id);
                if (accountToUpdate != null)
                {
                    accountToUpdate.CurrentBalance = CurrentBalance;
                    context.SaveChanges();
                }
            }
        }


    }

}






