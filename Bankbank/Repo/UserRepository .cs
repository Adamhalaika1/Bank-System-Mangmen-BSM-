using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    public class UserRepository : IUserRepository
    {
        //private readonly AppDbContext context;
        //public UserRepository(AppDbContext context)
        //{
        //    this.context = context;
        //}


        public User GetUserById(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new AppDbContext())
            {
                return context.Users.ToList();
            }
        }

        public void AddUser(User user)
        {
            using (var context = new AppDbContext())
            {
                context.Add(user);
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new AppDbContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    // Update user properties
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Password = user.Password;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.CustomerType = user.CustomerType;
                    existingUser.City = user.City;
                    existingUser.UserType = user.UserType;
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    context.Remove(user);
                }
            }
        }
    }

}
