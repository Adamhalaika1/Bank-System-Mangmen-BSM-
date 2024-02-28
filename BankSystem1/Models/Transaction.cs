using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LoanPaymentId { get; set; }
        public int AccountId { get; set; }
        public int EmployeeId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual LoanPayment LoanPayment { get; set; } = null!;
    }
}
