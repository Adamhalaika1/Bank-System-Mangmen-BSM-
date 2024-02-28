using System;
using System.Collections.Generic;

namespace BankSystem1.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int UsersId { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public string? Position { get; set; }
        public int? BranchId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Employee? Manager { get; set; }
        public virtual User Users { get; set; } = null!;
        public virtual ICollection<Employee> InverseManager { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
