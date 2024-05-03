using Bankbank.DataContext;
using Bankbank.Entities;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using static Bankbank.Entities.Account;
namespace Bankbank.Entities
{
    public enum CustomerType
    {
        Legal = 1,
        Individual = 2,
    }
    public enum Role 
    { 
        Admin,
        Employee,
        Customer
    }
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get { return $"{FirstName}.{LastName}{Id}"; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public string City { get; set; }
        public Role UserType { get; set; }
        //public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Loan> Loan { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }
        public static User currentUser;
    }

}
