using Bankbank.DataContext;
using Bankbank.Entities;
using Bankbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Bankbank.Entities.Account;
namespace Bankbank.Entities
{

    public partial class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CustomerType { get; set; }
        public string City { get; set; }
        public enum Role { Admin, Employee,Customer }
        public Role UserType { get; set; }
        //public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Loan> Loan { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }


        public static Users currentUser;
        public void Login()
        {

            using (var context = new AppDbContext())
            {

                Console.Write("Welcome to Banking System \n");
                Console.Write("Enter your email: ");
                string email = Console.ReadLine();
                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                currentUser = context.Users.FirstOrDefault(u => u.Email == email && u.Password== password);
                if (currentUser == null)
                {
                    Console.WriteLine("Invalid credentials. Please try again.");
                    Login();
                }
            }
        }
       
    }

}
