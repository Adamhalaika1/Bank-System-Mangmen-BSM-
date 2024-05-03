using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repositories.IRepository
{
    //When making iniilize for the IAccountRepository Already make Account 
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);


    }
}
