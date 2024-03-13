using Bankbank.DataContext;
using Bankbank.Entities;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Bankbank.Models
{
    internal class Admin : User
    {
        public static void CreateNewUser()
        {
            using (var context = new AppDbContext())
            {
                User User = new User();
                Console.Clear();
                Console.Write("Create a new User : .\n");
                while (true)
                {
                    Console.Write("First Name: ");
                    User.FirstName = Console.ReadLine();

                    if (string.IsNullOrEmpty(User.FirstName) || User.FirstName.Any(char.IsDigit))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid first name without numbers.");
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Last Name: ");
                    User.LastName = Console.ReadLine();

                    if (string.IsNullOrEmpty(User.LastName) || User.LastName.Any(char.IsDigit))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid first name without numbers.");
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Date of Birth (MM/DD/YYYY): ");
                    string input = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input) && DateTime.TryParseExact(input, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))

                        if (dob.Year >= 1900 && dob.Year <= DateTime.Now.Year)
                        {
                            User.DateOfBirth = dob;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid year. Please enter a year between 1900 and the current year.");
                        }
                }


                while (true)
                {
                    Console.Write("Enter the Email : ");
                    string input = Console.ReadLine();

                    if (IsValidEmail(input))
                    {
                        User.Email = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid email format. Please enter a valid email address.");
                    }
                }

                while (true)
                {
                    Console.Write("Phone Number: ");
                    string phoneNumber = Console.ReadLine();

                    if (IsValidPhoneNumber(phoneNumber))
                    {
                        User.PhoneNumber = phoneNumber;
                        break;

                    }
                    else
                    {
                        Console.WriteLine("Invalid phone number format. Please enter a 10-digit phone number.");
                    }
                }


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


        // Function to validate email using regular expression
        private static bool IsValidEmail(string email)
        {
            // Regex pattern for basic email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regex pattern for validating a phone number with exactly ten digits
            string pattern = @"^\d{10}$"; // This pattern matches exactly 10 digits

            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
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

                Console.Write("New Phone Number: ");
                string newPhoneNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(newPhoneNumber))
                {
                    Console.WriteLine("Phone Number cannot be empty. Previous value will be retained.");
                }
                else if (!IsValidPhoneNumber(newPhoneNumber))
                {
                    Console.WriteLine("Invalid phone number format. The Value must be 10 Numbers.");
                }
                else
                {
                    user.PhoneNumber = newPhoneNumber;
                }


                Console.Write("New Address: ");
                string newAddress = Console.ReadLine();
                if (!string.IsNullOrEmpty(newAddress))
                {
                    user.Address = newAddress;
                }

                Console.Write("New Customer Type: ");
                string newCustomerType = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCustomerType))
                {
                    user.CustomerType = newCustomerType;
                }

                Console.Write("New City: ");
                string newCity = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCity))
                {
                    user.City = newCity;
                }

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
