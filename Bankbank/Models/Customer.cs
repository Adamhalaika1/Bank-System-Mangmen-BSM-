using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    internal class Customer : User
    {
        public override void ReadUser()
        {
            Console.WriteLine("┌──────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                            My Account                            │");
            Console.WriteLine("├───────────────────┬──────────────────────────────────────────────┤");
            Console.WriteLine("│ Email:            │ " + currentUser.Email.PadRight(25) + "│");
            Console.WriteLine("├───────────────────┼──────────────────────────────────────────────┤");
            Console.WriteLine("│ First Name:       │ " + currentUser.FirstName.PadRight(25) + "│");
            Console.WriteLine("├───────────────────┼──────────────────────────────────────────────┤");
            Console.WriteLine("│ Last Name:        │ " + currentUser.LastName.PadRight(25) + "│");
            Console.WriteLine("├───────────────────┼──────────────────────────────────────────────┤");
            Console.WriteLine("│ Phone Number:     │ " + currentUser.PhoneNumber.PadRight(25) + "│");
            Console.WriteLine("└───────────────────┴──────────────────────────────────────────────┘");
            Console.WriteLine();
        }

    }

}