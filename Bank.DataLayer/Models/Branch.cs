using Bankbank.Models;
using System;
using System.Collections.Generic;
namespace Bankbank.Entities
{
    public partial class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }
    }
}
