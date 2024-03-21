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

}
