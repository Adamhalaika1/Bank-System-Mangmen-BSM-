using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bank.Repositories.Repository
{
    public class UserRepository : Repository<User>,
        IRepository.IUserRepository,
        IRepository.IAdminRepository,
        IRepository.IEmployeeRepository,
        IRepository.ICustomerRepository
    {
        public User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }

            return context.Set<User>().FirstOrDefault(u => u.Email == email);
        }
    }


}