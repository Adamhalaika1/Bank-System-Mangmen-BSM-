using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public LoanPayment()
        {
            
        }

        public LoanPayment(int? loanId, decimal? paymentAmount, DateTime? schedulePaymentDate, decimal? monthlyPayment)
        {
            LoanId = loanId;
            PaymentAmount = paymentAmount;
            SchedulePaymentDate = schedulePaymentDate;
            MonthlyPayment = monthlyPayment;
            LoanId = loanId;
            Transactions = new List<Transactions>();
        }

        public  void LoanPay(string email,LoanType loanType)
        {
            using (var context = new AppDbContext())
            {
                var loan = context.Loan.FirstOrDefault(l => l.LoanType == loanType);

                if (loan == null)
                {
                    Console.WriteLine("Loan not found.");
                    return;
                }

                decimal paymentAmount;
                do
                {
                    Console.Write("Enter Payment Amount: ");
                } while (!decimal.TryParse(Console.ReadLine(), out paymentAmount) || paymentAmount <= 0);

                DateTime schedulePaymentDate;
                do
                {
                    Console.Write("Enter Scheduled Payment Date (yyyy-MM-dd): ");
                } while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out schedulePaymentDate));

                decimal monthlyPayment;
                do
                {
                    Console.Write("Enter Monthly Payment: ");
                } while (!decimal.TryParse(Console.ReadLine(), out monthlyPayment) || monthlyPayment <= 0);

                Console.WriteLine("\nLoan Payment Details:");
                Console.WriteLine($"Payment Amount: {paymentAmount}");
                Console.WriteLine($"Scheduled Payment Date: {schedulePaymentDate.ToShortDateString()}");
                Console.WriteLine($"Monthly Payment: {monthlyPayment}");

                LoanPayment newLoanPayment = new LoanPayment(loan.Id, paymentAmount, schedulePaymentDate, monthlyPayment);
                loan.LoanPayment =newLoanPayment;
                context.SaveChanges();
            }
        }


    }
}
