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
using ConsoleTableExt;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Bankbank.Repo;
namespace Bankbank
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Model
                User users = new User();
                Employee employee = new Employee();
                Customer customer = new Customer();
                Account account = new Account();
                Loan loan = new Loan();
                LoanPayment loanpayment = new LoanPayment();
                IUserReader admin = new Employee();
                //users.Login();

                //Repo
                using (var context = new AppDbContext())
                {
                    //IUserRepository userRepository = new UserRepository(context);
                    IUserRepository userRepository = new UserRepository();
                    UserService userService = new UserService(userRepository);
                    GetFunctionUser functionUser = new GetFunctionUser();
                    functionUser.ListAllUsers(userService);

                    //functionUser.AddUser(userService);
                    //functionUser.RetrieveUser(userService);
                    //functionUser.UpdateUser(userService);
                    //functionUser.DeleteUser(userService);
                    Thread.Sleep(100000);
                    context.SaveChanges();


                }


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
                        Console.WriteLine("4. Read User by Email");
                        Console.WriteLine("5. Read All User");
                        Console.WriteLine("6. Reborts ");
                        Console.WriteLine("7. Logout ");
                        int choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Admin.CreateNewUser();
                                break;
                            case 2:
                                Admin.UpdateUser();
                                break;
                            case 3:
                                Admin.DeleteUser();
                                break;
                            case 4:
                                admin.ReadUser();
                                break;
                            case 5:
                                admin.ReadAllUsers();
                                break;
                            case 6:
                                Rebort.Reborts();
                                break;
                            case 7:
                                Main(args);
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
                        Console.WriteLine("4. Update Account");
                        Console.WriteLine("5. Delete Account");
                        Console.WriteLine("6. Print all Account ");
                        Console.WriteLine("7. Create Loan ");
                        Console.WriteLine("8. Delete Loan ");
                        Console.WriteLine("9. Loan Payment Details ");
                        Console.WriteLine("10.Logout ");


                        int choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                employee.ReadUser();
                                break;
                            case 2:
                                employee.ReadAllUsers();
                                break;
                            case 3:
                                employee.CreateAccount();
                                break;
                            case 4:
                                employee.UpdateAccountByEmail();
                                break;
                            case 5:
                                employee.DeleteAccount();
                                break;
                            case 6:
                                employee.PrintUserAccounts();
                                break;
                            case 7:
                                loan.CreateLoan();
                                break;
                            case 8:
                                loan.DeleteLoan();
                                break;
                            case 9:
                                Console.Write("Enter the Customer Email you want to Add LoanPayment: ");
                                string emailLoanPayment = Console.ReadLine();
                                loanpayment.LoanPay(emailLoanPayment, LoanType.Personal);
                                break;
                            case 10:
                                Main(args);
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
                        Console.WriteLine("4 Update information");
                        Console.WriteLine("5. Logout ");

                        int choice = int.Parse(Console.ReadLine());
                        Console.Clear();

                        switch (choice)
                        {
                            case 1:
                                customer.ReadUser();
                                break;
                            case 2:
                                var selectedUserAccountToWithdraw = ChooseAccountType(currentUser.Id);
                                Console.WriteLine("Enter the amount to withdraw:");
                                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                                selectedUserAccountToWithdraw.Withdraw(withdrawAmount);
                                Console.WriteLine("Withdraw successful.");
                                break;
                            case 3:
                                var selectedUserAccountToDeposit = ChooseAccountType(currentUser.Id);
                                Console.WriteLine("Enter the amount to Deposit:");
                                decimal depositAmount = decimal.Parse(Console.ReadLine());
                                selectedUserAccountToDeposit.DepositAmount(depositAmount);
                                Console.WriteLine("Deposit successful.");
                                break;
                            case 4:
                                string email = currentUser.Email;

                                Admin.UpdateUser();

                                break;
                            case 5:
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

        public static List<User> GetData(int pageNumber, int pageSize)
        {
            using (var context = new AppDbContext())
            {
                return context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            }
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
//            .RuleFor(u => u.UserRole, f => f.PickRandom<Role>(Role.Admin, Role.Employee, Role.Customer))
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


//using (var context = new AppDbContext())
//{
//    if (!context.LoanPayment.Any())
//    {
//        var faker = new Faker<LoanPayment>()
//            .RuleFor(p => p.PaymentAmount, f => f.Finance.Amount())
//            .RuleFor(p => p.SchedulePaymentDate, f => f.Date.Future())
//            .RuleFor(p => p.MonthlyPayment, f => f.Finance.Amount())
//            .RuleFor(u => u.LoanId, f => f.PickRandom(85, 107, 119));

//        var loanPayments = faker.Generate(40); // Generate 40 fake loan payments

//        context.LoanPayment.AddRange(loanPayments);
//        context.SaveChanges();

//        Console.WriteLine("LoanPayment table seeded successfully.");
//    }
//    else
//    {
//        Console.WriteLine("LoanPayment table already seeded.");
//    }
//}


////seeding data for old data 


//using (var context = new AppDbContext())
//{
//    var users = context.Users.ToList();

//    var faker = new Faker<User>()
//        .RuleFor(u => u.CustomerType, f => f.PickRandom<CustomerType>(CustomerType.Legal, CustomerType.Individual));


//    foreach (var user in users)
//    {
//        var fakeUser = faker.Generate();
//        user.CustomerType = fakeUser.CustomerType;
//    }

//    context.SaveChanges();

//    Console.WriteLine("Existing data updated successfully.");
//}
