using Bankbank.Entities;
using System;
using System.Collections.Generic;


namespace Bankbank.Entities
{
    public partial class Loan
    {
        public int Id { get; set; }
        public string LoanType { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LoanStatus { get; set; }
        public decimal? InterestAmount { get; set; }
        public int? CustomerId { get; set; }

        public virtual Users Customer { get; set; }
        public virtual LoanPayment LoanPayment { get; set; }



    }
}


//public Loan()
//{
//    LoanPayment = new HashSet<LoanPayment>();

//}