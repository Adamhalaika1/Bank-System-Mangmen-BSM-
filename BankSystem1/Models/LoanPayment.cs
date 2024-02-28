using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class LoanPayment
    {
        public LoanPayment()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int? LoanId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? SchedulePaymentDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }

        public virtual Loan? Loan { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
