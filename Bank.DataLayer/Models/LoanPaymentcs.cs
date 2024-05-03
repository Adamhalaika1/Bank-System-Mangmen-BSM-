using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Bankbank.Entities
{
    public partial class LoanPayment
    {
        public int Id { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? SchedulePaymentDate { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public int? LoanId { get; set; }
        public virtual Loan Loan { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
