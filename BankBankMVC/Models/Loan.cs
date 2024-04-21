using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //Note :interestAmount =Loan value
        public Loan(int id, LoanType loanType, decimal? loanAmount, decimal? interestRate, DateTime? startDate, LoanStatus loanStatus, decimal? interestAmount)
        {
            CustomerId = id;
            LoanType = loanType;
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            StartDate = startDate;
            LoanStatus = loanStatus;
            InterestAmount = interestAmount;
        }
        public Loan()
        {

        }










    }


}
















//public Loan()
//{
//    LoanPayment = new HashSet<LoanPayment>();

//}