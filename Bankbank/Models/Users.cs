using Bankbank.Entities;
using System;
using System.Collections.Generic;
namespace Bankbank.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CustomerType { get; set; }
        public string City { get; set; }
        public enum Role { Admin, User }
        public Role UserType { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Loan> Loan { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }

}
