﻿using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
namespace Bankbank.Entities
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Withdraw { get; set; }
        public int? EmployeeId { get; set; }
        public int? LoanPaymentId { get; set; }
        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public virtual User Employee { get; set; }
        public virtual LoanPayment LoanPayment { get; set; }
    }
}





