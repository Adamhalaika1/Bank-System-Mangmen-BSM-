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
namespace Bankbank
{
    internal class Program
    {
        static Users currentUser;

        static void Main(string[] args)
        {
            //Note :
            // Logged in by Email and the password is FirstName 
            Login();
            while (true)
            {
                if (currentUser.UserType == Role.Admin)
                {
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("1. Create New User");
                    Console.WriteLine("2. Show All User ");
                    Console.WriteLine("3. Update User");
                    Console.WriteLine("4. Delete User");
                    Console.WriteLine("5. Exit");

                }
                if (currentUser.UserType == Role.User)
                {
                    Console.WriteLine("Choose an operation:");
                    Console.WriteLine("2. Show me Details");
                    Console.WriteLine("5. Exit");
                }
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();

                        if (currentUser.UserType == Role.Admin)
                        {
                            CreateUsers();
                        }
                        else
                        {
                            Console.WriteLine("Permission denied. Only Admin can create users.");
                        }
                        break;

                    case 2:
                        Console.Clear();

                        if (currentUser.UserType == Role.Admin)
                        {
                            Console.WriteLine("Enter the email of User :");
                            string Remail = Console.ReadLine();
                            ReadUser(Remail);
                        }

                        if (currentUser.UserType == Role.User)
                        {
                            ReadUser(currentUser.Email);
                        }
                        break;

                    case 3:

                        if (currentUser.UserType == Role.Admin)
                        {
                            Console.Write("Enter the User Email you want to update: ");
                            string email = Console.ReadLine();
                            UpdateUser(email);
                        }
                        break;
                    case 4:
                        if (currentUser.UserType == Role.Admin)
                        {
                            Console.Write("Enter the User Email you want to update: ");
                            string demaill = Console.ReadLine();
                            DeleteUser(demaill);
                        }
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            }

        }
        static void Login()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Welcome to Banking System \n");
                Thread.Sleep(5000);
                Console.Write("Enter your email: ");
                string email = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                currentUser = context.Users.FirstOrDefault(u => u.Email == email && u.FirstName == password);
                if (currentUser == null)
                {
                    Console.WriteLine("Invalid credentials. Please try again.");
                    Login();
                }
            }
        }
        static void CreateUsers()
        {
            using (var context = new AppDbContext())
            {
                Users User = new Users();
                Console.WriteLine(Role.Admin);

                Console.Write("First Name: ");
                User.FirstName = Console.ReadLine();

                Console.Write("Last Name: ");
                User.LastName = Console.ReadLine();

                Console.Write("Date of Birth (MM/DD/YYYY): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                {
                    User.DateOfBirth = dob;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Defaulting to current date.");
                    User.DateOfBirth = DateTime.Now;
                }

                Console.Write("Email: ");
                User.Email = Console.ReadLine();

                Console.Write("PhoneNumber: ");
                User.PhoneNumber = Console.ReadLine();

                Console.Write("Address: ");
                User.Address = Console.ReadLine();

                Console.Write("CustomerType: ");
                User.CustomerType = Console.ReadLine();

                Console.Write("City: ");
                User.City = Console.ReadLine();

                Console.WriteLine("Select User Role:");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.Write("Enter choice (1 for Admin, 2 for User): ");

                int userTypeChoice = int.Parse(Console.ReadLine());
                if (userTypeChoice == 1)
                {
                    User.UserType = Role.Admin;
                }
                else if (userTypeChoice == 2)
                {
                    User.UserType = Role.User;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Defaulting to User.");
                    User.UserType = Role.User;

                }
                context.Users.Add(User);
                context.SaveChanges();
                Console.WriteLine("Users created successfully.");
                Thread.Sleep(3000);
                Console.Clear();

            }
        }
        static void UpdateUser(string email)
        {
            using (var context = new AppDbContext())
            {
                //var user = context.Users.Find(userId);
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }
                Console.Write("New Email: ");
                user.Email = Console.ReadLine();

                Console.Write("New PhoneNumber: ");
                user.PhoneNumber = Console.ReadLine();

                Console.Write("New Address: ");
                user.Address = Console.ReadLine();

                Console.Write("New CustomerType: ");
                user.CustomerType = Console.ReadLine();

                Console.Write("New City: ");
                user.City = Console.ReadLine();
                context.SaveChanges();
                Console.WriteLine("User updated successfully.");
            }
        }
        static void DeleteUser(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }
                context.Users.Remove(user);
                context.SaveChanges();
                Console.WriteLine("User deleted successfully.");
            }
        }
        static void ReadUser(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }
                Console.WriteLine("User Details:");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Date of Birth: {user.DateOfBirth}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                Console.WriteLine($"Address: {user.Address}");
                Console.WriteLine($"CustomerType: {user.CustomerType}");
                Console.WriteLine($"City: {user.City}");
                Console.WriteLine($"User Type: {user.UserType}");
            }
        }
        //____________________________________________________________________________
        //                            Add New Account                               //




    }

}





//Seeding Databases Conditionally with Faker 

//if (!context.Users.Any())
//{
//    var faker = new Faker<User>()
//        .RuleFor(u => u.LastName, f => f.Name.LastName())
//        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
//        .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
//        .RuleFor(u => u.Email, f => f.Internet.Email())
//        .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
//        .RuleFor(u => u.Address, f => f.Address.FullAddress())
//        .RuleFor(u => u.CustomerType, f => f.Random.Word())
//        .RuleFor(u => u.City, f => f.Address.City())
//        .RuleFor(u => u.UserType, f => f.PickRandom<Role>(Role.Admin, Role.User));

//    var users = faker.Generate(10); // Generate 10 fake users

//    context.Users.AddRange(users);
//    context.SaveChanges();

//    Console.WriteLine("Database seeded successfully.");
//}
//else
//{
//    Console.WriteLine("Database already seeded.");
//}
