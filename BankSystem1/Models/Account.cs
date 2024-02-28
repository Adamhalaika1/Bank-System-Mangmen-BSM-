using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? AccountNumber { get; set; }
        public int? BranchId { get; set; }
        public decimal? Balance { get; set; }
        public string? AccountType { get; set; }
        public DateTime? DateOpened { get; set; }
        public string? AccountStatus { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
