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
        public  void CreateLoan()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the User Email you want to Create Loan: ");
                string emailCreatLoan = Console.ReadLine();
                var user = context.Users.FirstOrDefault(u => u.Email == emailCreatLoan);

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
                    Loan loan = new Loan(user.Id, selectedLoanType, loanAmount, interestRate, startDate, LoanStatus.ApplicationReceived, interestAmount);
                    user.Loan.Add(loan);
                    context.SaveChanges();

                }
            }
        }
        public void DeleteLoan()
        {
            using (var context = new AppDbContext())
            {
                Console.WriteLine("Enter the Email to Delete Loan:");
                string emailDeleteLoan = Console.ReadLine();
                var selectedLoan = Loan.ChooseLoanType(emailDeleteLoan);
                var user = context.Users.FirstOrDefault(u => u.Email == emailDeleteLoan);

                if (user != null)
                {
                    var loanToDelete = user.Loan.FirstOrDefault(l => l.LoanType == selectedLoan.LoanType);

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




        // Method to choose a Loan type
        public static Loan ChooseLoanType(string email)
        {
            var index = 0;
            List<Loan> loggedUserLoans;
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return null;
                }

                loggedUserLoans = context.Loan.Where(item => item.CustomerId == user.Id).ToList();
            }

            if (loggedUserLoans == null || loggedUserLoans.Count == 0)
            {
                Console.WriteLine("Sorry, you need to create a Loan");
                return null;
            }

            Console.WriteLine("Please choose a Loan type:");
            foreach (var accountItem in loggedUserLoans)
            {
                Console.WriteLine(index + ". " + accountItem.LoanType);
                index++;
            }

            int choice1 = Convert.ToInt32(Console.ReadLine());
            var selectedLoanType = (LoanType)choice1;
            var selectedUserLoan = loggedUserLoans.Where(acc => acc.LoanType == selectedLoanType).FirstOrDefault();
            return selectedUserLoan;
        }




    }


}
















//public Loan()
//{
//    LoanPayment = new HashSet<LoanPayment>();

//}