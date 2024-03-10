using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    internal class Admin : Users
    {
        public static void CreateUsers()
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
                Console.WriteLine("1. Employee");
                Console.WriteLine("2. Customer");
                Console.Write("Enter choice (1 for Employee, 2 for Customer): ");

                int userTypeChoice = int.Parse(Console.ReadLine());
                if (userTypeChoice == 1)
                {
                    User.UserType = Role.Employee;
                }
                else if (userTypeChoice == 2)
                {
                    User.UserType = Role.Customer;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Defaulting to User.");
                    User.UserType = Role.Customer;

                }
                context.Users.Add(User);
                context.SaveChanges();
                Console.WriteLine("Users created successfully.");
                Thread.Sleep(3000);
                Console.Clear();
            }

        }
        public void UpdateUser(string email)
        {
            using (var context = new AppDbContext())
            {
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

        public void DeleteUser(string email)
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






    }
}
