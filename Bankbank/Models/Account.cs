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
        public bool AccountStatus { get; set; }
        public int CustomerId { get; set; }
        public virtual Users Customer { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }

        public static Account currentAccount;
        public Account(int customerId, string accountType, decimal currentBalance, DateTime dateTime, bool accountStatus)
        {
            this.CustomerId = customerId;
            this.AccountType = accountType;
            this.CurrentBalance = currentBalance;
            this.AccountStatus = accountStatus;
            this.DateOpened = dateTime;
        }
        public Account()
        {
            // Parameterless constructor for Entity Framework
        }
        public enum AccountTypes
        {
            CurrentAccount,
            SavingAccount,
            checkingAccount,
        }

    }



}