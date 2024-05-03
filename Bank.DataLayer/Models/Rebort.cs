//using Bankbank.DataContext;
//using Bankbank.Entities;
//using ConsoleTableExt;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
//using Microsoft.EntityFrameworkCore;
//using System.Threading;

//namespace Bankbank.Models
//{
//    internal class Rebort : User
//    {

//        public static void Reborts()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("Admin\n");
//                Console.WriteLine("Select an option:\n");
//                Console.WriteLine("1. User-Loan Payment Timeline");
//                Console.WriteLine("2. View Customer Account Summary");
//                Console.WriteLine("3. List of customers with loans in various stages (ApplicationReceived, UnderReview, Approved, Funded, InRepayment, Delinquent, Defaulted, PaidOff, Closed)");
//                Console.WriteLine("4. List of customers with detailed loan and account information");
//                Console.WriteLine("5. Show the count of active accounts for each account type");
//                Console.WriteLine("6. Pagination");
//                Console.WriteLine("7. Logout");
//                int choice = int.Parse(Console.ReadLine());
//                switch (choice)
//                {
//                    case 1:
//                        Rebort_1();
//                        break;
//                    case 2:
//                        Rebort_1();
//                        break;
//                    case 3:
//                        Rebort_3();
//                        break;
//                    case 4:
//                        Rebort_4();
//                        break;
//                    case 5:
//                        Rebort_5();
//                        break;
//                    case 6:
//                        DisplayData();
//                        break;
//                    case 7:
//                        User users = new User();
//                        users.Login();

//                        break;

//                }
//            }
//        }
//        public static void Rebort_1()
//        {
//            using (var context = new AppDbContext())
//            {

//                Console.Clear();
//                Console.WriteLine("1. For Customer Input the Email Customer-Loan Payment Timeline");
//                Console.WriteLine("2. For All Customer-Loan Payment Timeline press Enter");
//                string input = Console.ReadLine();

//                if (input != null && input != "")
//                {
//                    var users = context.Loan.Where(loan => loan.Customer.Email == input)
//                        .Join(
//                            context.Users,
//                            loan => loan.CustomerId,
//                            user => user.Id,
//                            (loan, user) => new
//                            {
//                                LoanId = loan.Id,
//                                LoanType = loan.LoanType,
//                                UserName = user.UserName,
//                                LoanAmount = loan.LoanAmount,
//                                InterestRate = loan.InterestRate,
//                                LoanPaymentId = loan.Id
//                            }
//                        )
//                        .Join(
//                            context.LoanPayment,
//                            loan => loan.LoanPaymentId,
//                            loanPayment => loanPayment.Id,
//                            (loan, loanPayment) => new
//                            {
//                                loan.LoanId,
//                                loan.LoanType,
//                                loan.UserName,
//                                loan.LoanAmount,
//                                loan.InterestRate,
//                                PaymentAmount = loanPayment.PaymentAmount,
//                                SchedulePaymentDate = loanPayment.SchedulePaymentDate
//                            }
//                        );

//                    DisplayUserDetails(users);
//                }
//                else
//                {
//                    var users = context.Loan
//                        .Join(
//                            context.Users,
//                            loan => loan.CustomerId,
//                            user => user.Id,
//                            (loan, user) => new
//                            {
//                                LoanId = loan.Id,
//                                LoanType = loan.LoanType,
//                                UserName = user.UserName,
//                                LoanAmount = loan.LoanAmount,
//                                InterestRate = loan.InterestRate,
//                                LoanPaymentId = loan.Id
//                            }
//                        )
//                        .Join(
//                            context.LoanPayment,
//                            loan => loan.LoanPaymentId,
//                            loanPayment => loanPayment.Id,
//                            (loan, loanPayment) => new
//                            {
//                                loan.LoanId,
//                                loan.LoanType,
//                                loan.UserName,
//                                loan.LoanAmount,
//                                loan.InterestRate,
//                                PaymentAmount = loanPayment.PaymentAmount,
//                                SchedulePaymentDate = loanPayment.SchedulePaymentDate
//                            }
//                        );

//                    DisplayUserDetails(users);
//                }
//            }
//        }

//        private static void DisplayUserDetails(IEnumerable<dynamic> users)
//        {
//            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
//            Console.WriteLine("| UserName              | LoanType      | LoanAmount  | InterestRate | PaymentAmount | SchedulePaymentDate |");
//            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");

//            foreach (var user in users)
//            {
//                Console.WriteLine($"| {user.UserName,-20} | {user.LoanType,-13} | {user.LoanAmount,-11:C} | {user.InterestRate,-13:P} | {user.PaymentAmount,-13:C} | {user.SchedulePaymentDate,-20:d} |");
//            }

//            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
//            Console.Write("press 'b' ro 'B' to go back  ");
//            Back();
//            Console.Clear();
//        }
//        public static void Rebort_2()
//        {
//            ////Customer Account Summary Report (2)
//            using (var context = new AppDbContext())
//            {
//                while (true)
//                {
//                    Console.Write("Enter the Id of Customer you want to show the Balance foreach Accounts and TotalBalance : ");
//                    string input = Console.ReadLine();
//                    if (int.TryParse(input, out int customerID))
//                    {
//                        var users = context.Accounts
//                            .Join(
//                          context.Users,
//                          account => account.CustomerId,
//                          user => user.Id,
//                          (account, user) => new
//                          {
//                              AccountId = account.Id,
//                              accountType = account.AccountType,
//                              user = user.UserName,
//                          }
//                          );
//                        var accountBalances = context.Accounts
//                            .Where(account => account.CustomerId == customerID)
//                            .GroupBy(account => account.AccountType)
//                            .Select(g => new { AccountType = g.Key, TotalBalance = g.Sum(account => account.CurrentBalance) })
//                            .ToList();
//                        ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From(accountBalances);
//                        tableBuilder.ExportAndWriteLine();
//                        var totalBalance = context.Accounts
//                            .Where(account => account.CustomerId == customerID)
//                            .Sum(account => account.CurrentBalance);
//                        Console.WriteLine($"Total balance of all accounts for customer {customerID}: {totalBalance}");
//                        Console.Write("press 'b' ro 'B' to go back  ");
//                        Back();
//                        Console.Clear();
//                    }
//                    else
//                    {
//                        Console.WriteLine("Invalid customer ID. Please enter a valid integer.");
//                        Back();
//                        Console.Clear();
//                    }
//                    Back();
//                }
//            }

//        }
//        public static void Rebort_3()

//        {
//            ////Report: List of customers with loans that are(ApplicationReceived, UnderReview, Approved, Funded, InRepayment, Delinquent, Defaulted, PaidOff, Closed) (3)

//            while (true)
//            {
//                Console.WriteLine("Choose a Loan Status:");
//                foreach (LoanStatus status in Enum.GetValues(typeof(LoanStatus)))
//                {
//                    Console.WriteLine($"{(int)status}. {status}");
//                }
//                Console.Write("Enter the number corresponding to the Loan Status: ");
//                int statusChoice = int.Parse(Console.ReadLine());
//                LoanStatus selectedStatus = (LoanStatus)statusChoice;
//                using (var context = new AppDbContext())
//                {
//                    var loansByStatus = context.Loan
//                        .Where(loan => loan.LoanStatus == selectedStatus)
//                        .Join(context.Users,
//                            loan => loan.CustomerId,
//                            customer => customer.Id,
//                            (loan, customer) => new { CustomerName = customer.FirstName + " " + customer.LastName, loan.LoanType, loan.LoanAmount, loan.InterestRate, loan.StartDate, loan.EndDate })
//                        .ToList();
//                    ConsoleTableBuilder.From(loansByStatus).ExportAndWriteLine();
//                    Console.Write("press 'b' ro 'B' to go back  ");
//                    Back();
//                    Console.Clear();
//                }
//            }
//        }
//        public static void Rebort_4()

//        {
//            //////List of customers with their loan and account information > 4
//            using (var context = new AppDbContext())
//            {
//                var customerLoanAccountInfo = context.Users
//                    .Join(context.Loan,
//                        customer => customer.Id,
//                        loan => loan.CustomerId,
//                        (customer, loan) => new { CustomerName = customer.FirstName + " " + customer.LastName, loan.LoanType, loan.LoanAmount, loan.InterestRate, loan.StartDate, loan.EndDate })
//                    .Join(context.Accounts,
//                        loanInfo => loanInfo.CustomerName,
//                        account => account.Customer.FirstName + " " + account.Customer.LastName,
//                        (loanInfo, account) => new { CustomerName = loanInfo.CustomerName, LoanType = loanInfo.LoanType, LoanAmount = loanInfo.LoanAmount, InterestRate = loanInfo.InterestRate, StartDate = loanInfo.StartDate, EndDate = loanInfo.EndDate, AccountType = account.AccountType, CurrentBalance = account.CurrentBalance })
//                    .ToList();
//                ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From(customerLoanAccountInfo);
//                tableBuilder.ExportAndWriteLine();
//                Console.Write("press 'b' ro 'B' to go back  ");
//                Back();
//                Console.Clear();
//            }
//        }


//        public static void Rebort_5()
//        {
//            //Report: Active Accounts Report . (5)
//            //This report shows the number of active accounts for each account type.
//            using (var context = new AppDbContext())
//            {
//                var activeAccounts = context.Accounts
//                    .Where(account => account.AccountStatus == AccountStatus.Active)
//                    .GroupBy(account => account.AccountType)
//                    .Select(g => new { AccountType = g.Key, ActiveAccountCount = g.Count() });

//                foreach (var activeAccount in activeAccounts)
//                    Console.WriteLine($"{activeAccount.AccountType} -  {activeAccount.ActiveAccountCount} ");
//                decimal test = decimal.Parse(Console.ReadLine());
//            }
//        }


//        static void DisplayData()
//        {
//            Console.Clear();
//            Console.WriteLine("Input column Number To skip ");
//            int columnNumber = int.Parse(Console.ReadLine());
//            Console.WriteLine("Input the column Size ");
//            int columnSize = int.Parse(Console.ReadLine());
//            var Data = GetData(columnNumber, columnSize);
//            foreach (var user in Data)
//            {
//                Console.WriteLine($"| {user.FirstName,-12} |     {user.LastName,-11} |      {user.DateOfBirth,-14} | {user.Email,-16} |");
//            }
//            Console.Write("press 'b' ro 'B' to go back  ");
//            Back();
//            Console.Clear();
//        }

//        //Extra
//        //I refer to it as the data display method, not as a report, which I use to show data according to the columnNumber and columnSize <<Pagination>>
//        public static List<User> GetData(int columnNumber, int columnSize)
//        {
//            using (var context = new AppDbContext())
//            {
//                return context.Users.Skip((columnNumber - 1) * columnSize).Take(columnSize).ToList();
//            }
//        }


//        //Note : This Report Can't work  
//        //using (var context = new AppDbContext())
//        //{
//        //    Console.Write("Enter the month (1-12): ");
//        //    int month;
//        //    if (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
//        //    {
//        //        Console.WriteLine("Invalid month input.");
//        //        return;
//        //    }

//        //    Console.Write("Enter the year (e.g. 2023): ");
//        //    int year;
//        //    if (!int.TryParse(Console.ReadLine(), out year))
//        //    {
//        //        Console.WriteLine("Invalid year input.");
//        //        return;
//        //    }

//        //    DateTime startDate = new DateTime(year, month, 1);
//        //    DateTime endDate = startDate.AddMonths(1).AddDays(-1);

//        //    var loans = context.Loan
//        //        .Include("Account")
//        //        .Include("LoanPayments.Transaction")
//        //        .Where(l => l.StartDate >= startDate && l.StartDate <= endDate)
//        //        .ToList();

//        //    if (loans.Count > 0)
//        //    {
//        //        Console.WriteLine("Loans for the specified month:");
//        //        foreach (var loan in loans)
//        //        {
//        //            Console.WriteLine($"- Start Date: {loan.StartDate}");
//        //            Console.WriteLine($"- End Date: {loan.EndDate}");
//        //            Console.WriteLine($"- Status: {loan.LoanStatus}");
//        //            Console.WriteLine($"- Interest Amount: {loan.InterestAmount}");
//        //        }
//        //    }
//        //}
//    }


//}

