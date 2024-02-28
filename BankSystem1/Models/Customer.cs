using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
            Loans = new HashSet<Loan>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int? UsersId { get; set; }
        public string? CustomerType { get; set; }

        public virtual User? Users { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
