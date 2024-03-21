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
    internal class Admin : User, IUserReader 
    {
        public override void ReadUser()
        {

        }
        public override void ReadAllUsers()
        {

        }

        public static void CreateNewUser()
        {
            using (var dbContext = new AppDbContext())
            {
                User newUser = new User();
                Console.Clear();
                Console.Write("Create a new User: \n");
                while (true)
                {
                    Console.Write("First Name: ");
                    newUser.FirstName = Console.ReadLine();

                    if (string.IsNullOrEmpty(newUser.FirstName) || newUser.FirstName.Any(char.IsDigit))
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
                    Console.Write("Enter Password: ");
                    newUser.Password = Console.ReadLine();

                    if (string.IsNullOrEmpty(newUser.Password))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid password.");
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Last Name: ");
                    newUser.LastName = Console.ReadLine();

                    if (string.IsNullOrEmpty(newUser.LastName) || newUser.LastName.Any(char.IsDigit))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid last name without numbers.");
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Date of Birth (MM/DD/YYYY): ");
                    string dobInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(dobInput) && DateTime.TryParseExact(dobInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                    {
                        if (dob.Year >= 1900 && dob.Year <= DateTime.Now.Year)
                        {
                            newUser.DateOfBirth = dob;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid year. Please enter a year between 1900 and the current year.");
                        }
                    }
                }
                while (true)
                {
                    Console.Write("Enter the Email: ");
                    string emailInput = Console.ReadLine();

                    if (IsValidEmail(emailInput))
                    {
                        newUser.Email = emailInput;
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
                        newUser.PhoneNumber = phoneNumber;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid phone number format. Please enter a 10-digit phone number.");
                    }
                }
                Console.Write("Address: ");
                newUser.Address = Console.ReadLine();
                Console.Write("Select Customer Type:\n ");
                Console.WriteLine("1. Individual");
                Console.WriteLine("2. Legal");
                Console.Write("Enter choice (1 for Individual, 2 for Legal): ");
                int updateUserCustomnerChoice = int.Parse(Console.ReadLine());
                newUser.CustomerType = updateUserCustomnerChoice == 1 ? CustomerType.Individual : updateUserCustomnerChoice == 2 ? CustomerType.Legal : CustomerType.Legal;
                Console.Write("City: ");
                newUser.City = Console.ReadLine();
                Console.WriteLine("Select User Role:");
                Console.WriteLine("1. Employee");
                Console.WriteLine("2. Customer");
                Console.Write("Enter choice (1 for Employee, 2 for Customer): ");
                int updateUserTypeChoice = int.Parse(Console.ReadLine());
                newUser.UserType = updateUserTypeChoice == 1 ? Role.Employee : updateUserTypeChoice == 2 ? Role.Customer : Role.Customer;
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                Console.WriteLine("User created successfully.");
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


        public static void UpdateUser()
        {
            using (var context = new AppDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter the User Email you want to Update User (press 'b' to go back): ");
                    var key = Console.ReadKey(intercept: true);

                    if (key.KeyChar == 'b' || key.KeyChar == 'B')
                    {
                        Console.WriteLine("\nGoing back...");
                        Thread.Sleep(2000);
                        return;
                    }
                    string emailUpdate = key.KeyChar + Console.ReadLine();
                    var updateUser = context.Users.FirstOrDefault(u => u.Email == emailUpdate);
                    if (updateUser == null || currentUser.UserType == Role.Customer)
                    {
                        Console.WriteLine("Customer not found.");
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Update User");
                            Console.WriteLine();
                            Console.WriteLine("Current Phone Number: " + (updateUser.PhoneNumber) + "\nNew Phone Number or press Enter to skip: ");
                            string newPhoneNumber = Console.ReadLine();

                            if (string.IsNullOrEmpty(newPhoneNumber))
                            {
                                break; // Exit the loop if Enter is pressed
                            }
                            if (newPhoneNumber != updateUser.PhoneNumber)
                            {
                                if (!IsValidPhoneNumber(newPhoneNumber))
                                {
                                    Console.WriteLine("Phone Number must be valid. Previous value will be retained.");
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    updateUser.PhoneNumber = newPhoneNumber;
                                    break;
                                }
                            }
                        }
                        Console.WriteLine();
                        Console.Write("Current Address: " + updateUser.Address + "\nNew Address or press Enter to skip: ");
                        string newAddress = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newAddress))
                        {
                            updateUser.Address = newAddress;
                        }
                        if (currentUser.UserType == Role.Employee)
                        {
                            Console.Write("Current Customer Type: " + updateUser.CustomerType + "\nNew Customer Type or press Enter to skip: ");
                            string newCustomerType = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newCustomerType))
                            {
                                Console.Write("Select Customer Type: ");
                                Console.WriteLine("1. Individual");
                                Console.WriteLine("2. Legal");
                                Console.Write("Enter choice (1 for Individual, 2 for Legal): ");
                                int updateUserCustomnerChoice = int.Parse(Console.ReadLine());
                                updateUser.CustomerType = updateUserCustomnerChoice == 1 ? CustomerType.Individual : updateUserCustomnerChoice == 2 ? CustomerType.Legal : CustomerType.Legal;
                            }
                        }
                        Console.WriteLine();
                        Console.Write("Current City: " + updateUser.City + "\nNew City or press Enter to skip: ");
                        string newCity = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCity))
                        {
                            updateUser.City = newCity;
                        }


                        Console.Write("Current Email: " + updateUser.Email + "\nNew Email or press Enter to skip: ");
                        string newEmail = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newEmail))
                        {
                            if (newEmail != updateUser.Email)
                            {
                                if (!IsValidEmail(newEmail)) 
                                {
                                    Console.WriteLine("Email must be valid. Previous value will be retained.");
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    updateUser.Email = newEmail;
                                }
                            }
                        }
                        context.SaveChanges();
                        Console.WriteLine();
                        Console.WriteLine("User updated successfully.");
                        Thread.Sleep(4000);

                    }
                }

            }
        }

        public static void DeleteUser()
        {
            using (var context = new AppDbContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write("Enter the User Email you want to delete User (press 'b' to go back): ");
                    var key = Console.ReadKey(intercept: true);

                    if (key.KeyChar == 'b' || key.KeyChar == 'B')
                    {
                        Console.WriteLine("\nGoing back...");
                        Thread.Sleep(2000);
                        return;
                    }
                    string emailDelete = key.KeyChar + Console.ReadLine();
                    var deleteUser = context.Users.FirstOrDefault(u => u.Email == emailDelete);
                    if (deleteUser == null)
                    {
                        Console.WriteLine("User not found.");
                        Thread.Sleep(4000);
                    }
                    else
                    {
                        context.Users.Remove(deleteUser);
                        context.SaveChanges();
                        Console.WriteLine("User deleted successfully.");
                        Thread.Sleep(4000);
                        break;
                    }
                }


            }
        }






    }
}
