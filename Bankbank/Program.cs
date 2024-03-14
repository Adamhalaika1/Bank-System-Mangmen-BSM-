using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Faker;
using Bogus;
using static Bankbank.Entities.User;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.Identity.Client;
using static Bankbank.Entities.Account;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using User = Bankbank.Entities.User;
using System.Collections.ObjectModel;
namespace Bankbank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Reborts();

            try
            {
                User users = new User();
                Employee employee = new Employee();
                Customer customer = new Customer();
                Admin admin = new Admin();
                Account account = new Account();
                Loan loan = new Loan();
                users.Login();
                if (currentUser.UserType == Role.Admin)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Admin\n");
                        Console.WriteLine("Choose an operation:");
                        Console.WriteLine("1. Create User");
                        Console.WriteLine("2. Update User");
                        Console.WriteLine("3. Delete User");
                        Console.WriteLine("4. Logout ");
                        int choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Admin.CreateNewUser();
                                break;
                            case 2:
                                Console.Write("Enter the User Email you want to Update User: ");
                                string email = Console.ReadLine();
                                admin.UpdateUser(email);
                                break;
                            case 3:
                                Console.Write("Enter the User Email you want to delete User: ");
                                string emailDelete = Console.ReadLine();
                                admin.DeleteUser(emailDelete);
                                break;
                            case 4:
                                Main(args);

                                break;
                            case 5:
                                break;
                        }
                    }
                }

                if (User.currentUser.UserType == Role.Employee)
                {
                    while (true)
                    {
                        Console.WriteLine("Choose an operation:");
                        Console.WriteLine("1. Read Customer ");
                        Console.WriteLine("2. Read All Customer ");
                        Console.WriteLine("3. Create Account");
                        Console.WriteLine("4. Modify Account");
                        Console.WriteLine("5. Delete Account");
                        Console.WriteLine("6. Print all Account ");
                        Console.WriteLine("7.Create Loan ");
                        Console.WriteLine("8.Delete Loan ");
                        Console.WriteLine("9.Logout ");


                        int choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Enter the User Email you want to read customer: ");
                                string email = Console.ReadLine();
                                employee.ReadUser(email);
                                break;
                            case 2:
                                employee.ReadAllUsers();
                                break;
                            case 3:
                                Console.Write("Enter the Customer Email you want to Add Account: ");
                                string emailCreate = Console.ReadLine();
                                employee.CreatAccount(emailCreate);
                                break;
                            case 4:
                                Console.Write("Enter the User Email you want to modify Account: ");
                                string email_account = Console.ReadLine();
                                Console.WriteLine("Please choose an account type:");
                                Console.WriteLine("0. Current account");
                                Console.WriteLine("1. Savings account");
                                Console.WriteLine("2. checking  account");
                                int choice1 = Convert.ToInt32(Console.ReadLine());
                                var selectedAccountType = (AccountTypes)choice1;
                                Console.WriteLine("You have selected: " + selectedAccountType);
                                employee.ModifyAccountByEmail(email_account, selectedAccountType.ToString());
                                break;
                            case 5:
                                Console.Write("Enter the Customer Email you want to delete Account: ");
                                string emailDelete = Console.ReadLine();
                                employee.DeleteAccount(emailDelete);
                                break;
                            case 6:
                                employee.PrintUserAccounts();
                                break;
                            case 9:
                                Main(args);
                                break;
                            case 7:
                                Console.Write("Enter the User Email you want to Create Loan: ");
                                string emailCreatLoan = Console.ReadLine();
                                loan.CreateLoan(emailCreatLoan);


                                break;
                            case 8:
                                Console.Write("Enter the User Email you want to Dekete Loan: ");
                                string emailDeleteLoan = Console.ReadLine();
                                //loan.DeleteLoan(emailDeleteLoan,);
                                break;
                        }
                    }

                }
                if (User.currentUser.UserType == Role.Customer)
                {
                    while (true)
                    {
                        Console.WriteLine("Choose an operation:");
                        Console.WriteLine("1. My Account");
                        Console.WriteLine("2. Withdraw");
                        Console.WriteLine("3. Deposit");
                        Console.WriteLine("4. Logout ");

                        int choice = int.Parse(Console.ReadLine());
                        Console.Clear();

                        switch (choice)
                        {

                            case 1:

                                customer.ReadUser();
                                break;
                            case 2:
                                Console.WriteLine("Enter the amount to withdraw:");
                                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                                // Perform withdrawal operation
                                break;
                            case 3:

                                var index = 0;
                                List<Account> loggedUserAccounts;
                                var loggedInUserId = User.currentUser.Id;
                                using (var context = new AppDbContext())
                                {
                                    loggedUserAccounts = context.Accounts.Where(Item => Item.CustomerId == loggedInUserId).ToList();

                                }
                                if (loggedUserAccounts == null || loggedUserAccounts.Count == 0)
                                {
                                    Console.WriteLine("sorry you need to crate an account");
                                    break;
                                }
                                Console.WriteLine("Please choose an account type:");

                                foreach (var accountItem in loggedUserAccounts)
                                {
                                    Console.WriteLine(index + ". " + accountItem.AccountType);
                                    index++;

                                }
                                int choice1 = Convert.ToInt32(Console.ReadLine());
                                var selectedAccountType = (AccountTypes)choice1;
                                var selectedUserAccount = loggedUserAccounts.Where(acc => acc.AccountType == selectedAccountType.ToString()).FirstOrDefault();
                                var selectedAccountBallance = selectedUserAccount.CurrentBalance;
                                Console.WriteLine("Enter the amount to deposit:");
                                decimal depositAmount = decimal.Parse(Console.ReadLine());
                                selectedUserAccount.DepositAmount(depositAmount);
                                Console.WriteLine("Deposit successful.");

                                break;
                            case 4:
                                Main(args);
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                }
            }



            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);

            }
        }

        public static void Reborts()
        {
            // 1 
            //using (var context = new AppDbContext())
            //{

            //    var Allusers = context.Users.ToList();
            //    foreach (var user in Allusers)
            //    {
            //        Console.WriteLine($"{user.LastName}");
            //    }
            //    decimal test = decimal.Parse(Console.ReadLine());


            //}


            //2
            //using (var context = new AppDbContext())
            //{

            //    var users = context.Users
            //        .Join(
            //              context.Accounts,
            //              user => user.Id,
            //              account => account.Id,
            //              (user, account) => new
            //              {
            //                  AccountId = account.Id,
            //                  accountType = account.AccountType,
            //                  user = user.FirstName,
            //              }
            //        );

            //    foreach (var account in users)
            //        Console.WriteLine($"{account.AccountId} -  {account.accountType}  - {account.user} ");
            //    decimal test = decimal.Parse(Console.ReadLine());

            //}

























        }

    }







}


//seeding data


//using (var context = new AppDbContext())
//{

//    if (context.Users.Any())
//    {
//        var faker = new Faker<Users>()
//            .RuleFor(u => u.LastName, f => f.Name.LastName())
//            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
//            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
//            .RuleFor(u => u.Email, f => f.Internet.Email())
//            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
//            .RuleFor(u => u.Address, f => f.Address.FullAddress())
//            .RuleFor(u => u.CustomerType, f => f.Random.Word())
//            .RuleFor(u => u.City, f => f.Address.City())
//            .RuleFor(u => u.UserType, f => f.PickRandom<Role>(Role.Admin, Role.Employee, Role.Customer))
//            .RuleFor(u => u.Password, f => f.Internet.Password()); // Generate random password

//        var userss = faker.Generate(10); // Generate 10 fake users

//        context.Users.AddRange(userss);
//        context.SaveChanges();

//        Console.WriteLine("Database seeded successfully.");
//    }
//    else
//    {
//        Console.WriteLine("Database already seeded.");
//    }
//}




//seding data for Loan
//using (var context = new AppDbContext())
//{

//    if (context.Loan.Any())
//    {
//        var faker = new Faker<Loan>()
//            .RuleFor(u => u.LoanType, f => f.PickRandom<LoanType>())
//            .RuleFor(u => u.LoanAmount, f => f.Random.Decimal(1000, 10000))
//            .RuleFor(u => u.InterestRate, f => f.Random.Decimal(1, 20))
//            .RuleFor(u => u.StartDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddYears(1)))
//            .RuleFor(u => u.EndDate, f => f.Date.Between(new DateTime(2026, 1, 1), new DateTime(2026, 12, 31)))
//            .RuleFor(u => u.LoanStatus, f => f.PickRandom<LoanStatus>())
//            .RuleFor(u => u.InterestAmount, f => f.Random.Decimal(1000, 50000))
//            .RuleFor(u => u.CustomerId, f => f.PickRandom(19, 20, 22, 23, 29, 35, 38, 39, 45, 46));

//        var userss = faker.Generate(40); // Generate 40 fake users

//        context.Loan.AddRange(userss);
//        context.SaveChanges();

//        Console.WriteLine("Database seeded successfully.");
//    }
//    else
//    {
//        Console.WriteLine("Database already seeded.");
//    }
//}



