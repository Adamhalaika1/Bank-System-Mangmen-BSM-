using Bankbank.DataContext;
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
        public virtual Users Employee { get; set; }
        public virtual LoanPayment LoanPayment { get; set; }

        public Transactions(int id, DateTime? transactionDate, decimal? deposit, decimal? withdraw, int? employeeId, int? loanPaymentId, int? accountId)
        {
            Id = id;
            TransactionDate = transactionDate;
            Deposit = deposit;
            Withdraw = withdraw;
            EmployeeId = employeeId;
            LoanPaymentId = loanPaymentId;
            AccountId = accountId;
        }


        //public void DepositAmount(decimal amount)
        //{
        //    if (amount <= 0)
        //    {
        //        throw new ArgumentException("Deposit amount must be greater than zero.");
        //    }

        //    CurrentBalance += amount;

        //}


        //public void WithdrawAmount(decimal amount)
        //{
        //    if (amount <= 0)
        //    {
        //        throw new ArgumentException("Withdrawal amount must be greater than zero.");
        //    }

        //    if (amount > CurrentBalance)
        //    {
        //        throw new InvalidOperationException("Insufficient funds for withdrawal.");
        //    }

        //    CurrentBalance -= amount;
        //}





    }
}






