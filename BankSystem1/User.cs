using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem1
{
    public class User
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public int Age { get; }
        public decimal Balance { get; }
        public string Status { get; }
        public enum UserTypes { Admin, User }
        public UserTypes UserType { get; set; }
        public User(int id, string email, string name, int age, decimal balance, string status, UserTypes userType)
        {
            Id = id;
            Email = email;
            Name = name;
            Age = age;
            Balance = balance;
            Status = status;
            UserType = userType;
        }



        public virtual void PrintUserType()
        {
            if (this.UserType == User.UserTypes.Admin)
            {
                Console.WriteLine("Welcom Admin " + this.Name);
            }
            else if (this.UserType == User.UserTypes.User)
            {
                Console.WriteLine("Welcom User "+this.Name);
            }

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
