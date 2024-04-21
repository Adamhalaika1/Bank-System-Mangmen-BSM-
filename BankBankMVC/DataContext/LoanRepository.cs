using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankbank.Repositories
{
    public class LoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateLoan(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

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
                _context.SaveChanges();

            }
        }

        public void DeleteLoan(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                var selectedLoan = ChooseLoanType(email);
                var loanToDelete = user.Loan.FirstOrDefault(l => l.LoanType == selectedLoan.LoanType);

                if (loanToDelete != null)
                {
                    user.Loan.Remove(loanToDelete);
                    _context.SaveChanges();
                    Console.WriteLine("Loan deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Loan not found.");
                }
            }
        }

        public Loan ChooseLoanType(string email)
        {
            var index = 0;
            List<Loan> loggedUserLoans;

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            loggedUserLoans = _context.Loan.Where(item => item.CustomerId == user.Id).ToList();

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
