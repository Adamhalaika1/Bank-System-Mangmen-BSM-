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
    
    }

}






