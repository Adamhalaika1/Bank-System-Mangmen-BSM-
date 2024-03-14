using Bankbank.DataContext;
using Bankbank.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace Bankbank.Entities
{
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
        public Loan(int id, LoanType loanType, decimal? loanAmount, decimal? interestRate, DateTime? startDate,LoanStatus loanStatus, decimal? interestAmount)
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
        public void CreateLoan(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    Console.WriteLine("Enter Loan Type:");
                    Console.WriteLine("0. Savings account");
                    Console.WriteLine("1. Personal");
                    Console.WriteLine("2. Mortgage");
                    Console.WriteLine("3. Student");
                    Console.WriteLine("4. Business");
                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid Loan Type.");
                        return;
                    }
                    var selectedLoanType = (LoanType)choice;
                    Console.WriteLine("You have selected: " + selectedLoanType);
                    DateTime startDate = DateTime.Now;
                    Console.WriteLine("Enter Loan Amount:");
                    decimal loanAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out loanAmount))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal value for Loan Amount:");
                    }

                    Console.WriteLine("Enter Interest Rate:");
                    decimal interestRate;
                    while (!decimal.TryParse(Console.ReadLine(), out interestRate))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal value for Interest Rate:");
                    }

                    Console.WriteLine("Enter Interest Amount:");
                    decimal interestAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out interestAmount))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal value for Interest Amount:");
                    }
                    if (user.Loan == null)
                    {
                        user.Loan = new List<Loan>();
                    }
                    Loan loan = new Loan(user.Id, selectedLoanType, loanAmount, interestRate, startDate,  LoanStatus.ApplicationReceived, interestAmount);
                    user.Loan.Add(loan);
                    context.SaveChanges();

                }
            }
        }
        public void DeleteLoan(string email, LoanType LoanType)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    var loanToDelete = user.Loan.FirstOrDefault(l => l.LoanType== LoanType);

                    if (loanToDelete != null)
                    {
                        user.Loan.Remove(loanToDelete);
                        context.SaveChanges();
                        Console.WriteLine("Loan deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Loan not found.");
                    }
                }
            }
        }



    }
}
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

















//public Loan()
//{
//    LoanPayment = new HashSet<LoanPayment>();

//}