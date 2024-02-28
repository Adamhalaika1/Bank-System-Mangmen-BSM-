using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Accounts = new HashSet<Account>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? BranchName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
