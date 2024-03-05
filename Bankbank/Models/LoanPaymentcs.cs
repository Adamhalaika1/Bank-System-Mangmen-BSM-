using Bankbank.Entities;
using System;
using System.Collections.Generic;



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
        public LoanPayment(int id, decimal? paymentAmount, DateTime? schedulePaymentDate, decimal? monthlyPayment, int? loanId)
        {
            Id = id;
            PaymentAmount = paymentAmount;
            SchedulePaymentDate = schedulePaymentDate;
            MonthlyPayment = monthlyPayment;
            LoanId = loanId;
            Transactions = new List<Transactions>();
        }
    }

}
