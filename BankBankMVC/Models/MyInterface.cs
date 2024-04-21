using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    interface IUserReader
    {
        void ReadUser();
        void ReadAllUsers();
    }

    interface ILoan 
    {
        void CreateLoan();
    }
    public interface IUserRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }

}
