using Bankbank.Entities;
using System;
using System.Collections.Generic;

namespace Bank.Entities
{
    public enum AccountTypes
    {
        CurrentAccount,
        SavingAccount,
        CheckingAccount,
    }
    public enum AccountStatus
    {
        Active,
        InActive
    }
    public class Account
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
    }
}
