using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    internal class Customer : Users
    {
        public void ReadUser()
        {
            Console.WriteLine("Customer Information:");
            Console.WriteLine($"Email: {currentUser.Email}");
            Console.WriteLine($"First Name: {currentUser.FirstName}");
            Console.WriteLine($"Last Name: {currentUser.LastName}");
            Console.WriteLine($"Phone Number: {currentUser.PhoneNumber}");

        }





    }
}
