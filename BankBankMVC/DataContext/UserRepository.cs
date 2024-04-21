using Bankbank.DataContext;
using Bankbank.Entities;
using System;
using System.Linq;

namespace Bank.DataAccess
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public static User GetUserByEmailAndPassword(string email, string password)
        {

            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public virtual void ReadUser()
        {

        }
        public virtual void ReadAllUsers()
        {

        }
    }
}
