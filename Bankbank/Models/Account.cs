using Bankbank.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
namespace Bankbank.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal? CurrentBalance { get; set; }

        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public int CustomerId { get; set; }
        public virtual Users Customer { get; set; }
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




