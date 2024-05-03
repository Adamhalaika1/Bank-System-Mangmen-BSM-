using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repositories.Repository
{
    public class EmployeeRepository : Repository<User>, IRepository.IEmployeeRepository
    {

    }
}
