using Bankbank.DataContext;
using Bankbank.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace Bankbank.Entities
{
    public enum LoanType
    {
        Personal,
        Mortgage,
        Auto,
        Student,
        Business
    }
    public enum LoanStatus
    {
        ApplicationReceived,
        UnderReview,
        Approved,
        Funded,
        InRepayment,
        Delinquent,
        Defaulted,
        PaidOff,
        Closed
    }
    public partial class Loan 
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LoanStatus LoanStatus { get; set; }
        public decimal? InterestAmount { get; set; }
        public int? CustomerId { get; set; }
        public virtual User Customer { get; set; }
        public virtual LoanPayment LoanPayment { get; set; }
    }


}