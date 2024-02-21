using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem1
{
    public class Admin : User
    {
        public Admin(int id, string email, string name, int age, decimal balance, string status, UserTypes role)
            : base(id, email, name, age, balance, status, role)
        {
        }
        public override void PrintUserType()
        {
            Console.WriteLine("Login successful as Admin!");
        }
        //public bool IsAdmin()
        //{
        //    return true;
        //}
        //public bool IsUser()
        //{
        //    return false;
        //}
    }
}
