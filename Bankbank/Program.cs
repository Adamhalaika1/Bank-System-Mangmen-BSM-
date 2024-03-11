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
using static Bankbank.Entities.Users;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.Identity.Client;
using static Bankbank.Entities.Account;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Users = Bankbank.Entities.Users;
using System.Collections.ObjectModel;
namespace Bankbank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Users users = new Users();
            Employee employee = new Employee();
            Customer customer = new Customer();
            Admin admin = new Admin();
            Account account = new Account();
            users.Login();
            if (Users.currentUser.UserType == Role.Admin)
            {
                while (true)
                {
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. Create User");
                    Console.WriteLine("2. Update User");
                    Console.WriteLine("3. Delete User");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Admin.CreateUsers();
                            break;
                        case 2:
                            Console.Write("Enter the User Email you want to Update User: ");
                            string email = Console.ReadLine();
                            admin.UpdateUser(email);
                            break;
                        case 3:
                            Console.Write("Enter the User Email you want to delete User: ");
                            string emailDelete = Console.ReadLine();
                            admin.UpdateUser(emailDelete);
                            break;
                        case 4:
                            break;
                        case 5:

                            break;
                    }
                }
            }

            if (Users.currentUser.UserType == Role.Employee)
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
                    }
                }

            }
            if (Users.currentUser.UserType == Role.Customer)
            {
                while (true)
                {
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. My Account");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Deposit");

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
                            try
                            {
                                var index = 0;
                                List<Account> loggedUserAccounts;
                                var loggedInUserId = Users.currentUser.Id;
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
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
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



