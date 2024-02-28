using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Loan
    {
        public Loan()
        {
            LoanPayments = new HashSet<LoanPayment>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? LoanType { get; set; }
        public decimal? Amount { get; set; }
        public string? LoanStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
    }
}
