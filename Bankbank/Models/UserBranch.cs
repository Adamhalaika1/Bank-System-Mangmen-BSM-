using Bankbank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Models
{
    public class UserBranch
    {
        public int UserId { get; set; }
        public Users User { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
