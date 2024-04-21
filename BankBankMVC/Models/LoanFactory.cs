using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Linq;

namespace Bankbank.Entities
{
    public class LoanFactory
    {
        public static Loan CreateLoan(int id, LoanType loanType, decimal loanAmount, decimal interestRate, decimal interestAmount)
        {
            switch (loanType)
            {
                case LoanType.Personal:
                    return new PersonalLoan(id, loanAmount, interestRate, interestAmount);
                case LoanType.Mortgage:
                    return new MortgageLoan(id, loanAmount, interestRate, interestAmount);
                case LoanType.Auto:
                    return new AutoLoan(id, loanAmount, interestRate, interestAmount);
                case LoanType.Student:
                    return new StudentLoan(id, loanAmount, interestRate, interestAmount);
                case LoanType.Business:
                    return new BusinessLoan(id, loanAmount, interestRate, interestAmount);
                default:
                    throw new ArgumentException("Invalid loan type");
            }
        }
    }

    public class PersonalLoan : Loan
    {
        public PersonalLoan(int id, decimal loanAmount, decimal interestRate, decimal interestAmount)
            : base(id, LoanType.Personal, loanAmount, interestRate, DateTime.Now, LoanStatus.ApplicationReceived, interestAmount)
        {
        }
    }

    public class MortgageLoan : Loan
    {
        public MortgageLoan(int id, decimal loanAmount, decimal interestRate, decimal interestAmount)
            : base(id, LoanType.Mortgage, loanAmount, interestRate, DateTime.Now, LoanStatus.ApplicationReceived, interestAmount)
        {
        }
    }

    public class AutoLoan : Loan
    {
        public AutoLoan(int id, decimal loanAmount, decimal interestRate, decimal interestAmount)
            : base(id, LoanType.Auto, loanAmount, interestRate, DateTime.Now, LoanStatus.ApplicationReceived, interestAmount)
        {
        }
    }

    public class StudentLoan : Loan
    {
        public StudentLoan(int id, decimal loanAmount, decimal interestRate, decimal interestAmount)
            : base(id, LoanType.Student, loanAmount, interestRate, DateTime.Now, LoanStatus.ApplicationReceived, interestAmount)
        {
        }
    }

    public class BusinessLoan : Loan
    {
        public BusinessLoan(int id, decimal loanAmount, decimal interestRate, decimal interestAmount)
            : base(id, LoanType.Business, loanAmount, interestRate, DateTime.Now, LoanStatus.ApplicationReceived, interestAmount)
        {
        }
    }
}
