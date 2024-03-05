using Bankbank.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Bankbank.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal? CurrentBalance { get; set; }
        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public string AccountStatus { get; set; }
        public int? CustomerId { get; set; }
        public virtual Users Customer { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public Account(string accountNumber, string accountType, decimal? currentBalance, DateTime? dateOpened)
        {
            AccountNumber = accountNumber;
            AccountType = accountType;
            CurrentBalance = currentBalance;
            DateOpened = dateOpened;
        }



        public void CreatAccount(string email)
        {
            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    Console.Write("Enter Account Number: ");
                    string accountNumber = Console.ReadLine();

                    Console.Write("Enter Account Type: ");
                    string accountType = Console.ReadLine();

                    Console.Write("Enter Current Balance: ");
                    decimal currentBalance = Convert.ToDecimal(Console.ReadLine());

                    DateTime dateTime = DateTime.Now;
                    Account account = new Account(accountNumber, accountType, currentBalance, dateTime);
                    user.Account.Add(account);
                    context.SaveChanges();
                }
            }
        }

    }
}
