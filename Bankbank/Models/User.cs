using Bankbank.DataContext;
using Bankbank.Entities;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using static Bankbank.Entities.Account;
namespace Bankbank.Entities
{
    public enum CustomerType
    {
        Legal=1,
        Individual=2,
    }

    public partial class User
    {
        public virtual void ReadUser()
        {

        }
        public virtual void ReadAllUsers()
        {

        }
        public int Id { get; set; }
        public string UserName { get { return $"{FirstName}.{LastName}{Id}"; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public string City { get; set; }
        public enum Role { Admin, Employee, Customer }
        public Role UserType { get; set; }
        //public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Loan> Loan { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }


        public static User currentUser;
        public void Login()
        {
            using (var context = new AppDbContext())
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════╗");
                Console.WriteLine("║        Welcome to the Banking      ║");
                Console.WriteLine("║             System                 ║");
                Console.WriteLine("╚════════════════════════════════════╝");
                Console.WriteLine();

                bool isValidLogin = false;
                int attempts = 3; // Number of login attempts allowed

                while (!isValidLogin && attempts > 0)
                {
                    Console.Write(" - Enter your email: ");
                    string email = Console.ReadLine();
                    Console.Write(" - Enter your password: ");
                    string password = Console.ReadLine();

                    // Check if the credentials are valid
                    currentUser = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                    Console.Clear();
                    if (currentUser != null)
                    {
                        isValidLogin = true;
                        Console.WriteLine("Login successful....");
                        Console.WriteLine();
                        Console.WriteLine("Welcome >> " + currentUser.UserName);
                    }
                    else
                    {
                        attempts--;
                        if (attempts > 0)
                        {
                            Console.WriteLine("Invalid credentials. Please try again. Attempts left: " + attempts);
                        }
                        else
                        {
                            Console.WriteLine("Maximum login attempts reached. Please try again after 5 minutes.");
                            int remainingTime = 300; // 5 minutes in seconds
                            while (remainingTime > 0)
                            {
                                Console.Write("Try again in: " + TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss") + "\r"); // \r moves cursor to beginning of line
                                System.Threading.Thread.Sleep(1000); // 1 second delay
                                remainingTime--;
                            }
                            Console.Clear();
                            Console.WriteLine("Try again in: 00:00");
                            attempts = 3;
                        }
                    }
                }
            }
        }
        public static string Back()
        {
            var key = Console.ReadKey(intercept: true);
            if (key.KeyChar == 'b' || key.KeyChar == 'B')
            {
                Console.WriteLine("\nGoing back...");
                Thread.Sleep(2000);
                return null; // Return null to indicate 'b' was pressed
            }
            string Back = key.KeyChar + Console.ReadLine();
            return Back;
        }



    }

}
