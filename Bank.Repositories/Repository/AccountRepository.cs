using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bank.Repositories.Repository
{
    public class AccountRepository : Repository<Account>, IRepository.IAccountRepository
    {
        public void Withdraw(int accountId, decimal amount)
        {
            var account = context.Accounts.Find(accountId);
            if (account == null)
            {
                throw new ArgumentException($"Account with Id = {accountId} not found");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }

            if (account.CurrentBalance < amount)
            {
                throw new InvalidOperationException("Insufficient balance");
            }
            account.CurrentBalance -= amount;
            context.SaveChanges();
        }
        public void Deposit(int accountId, decimal amount)
        {
            var account = context.Accounts.Find(accountId);
            if (account == null)
            {
                throw new ArgumentException($"Account with Id = {accountId} not found");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }

            account.CurrentBalance += amount;
            context.SaveChanges();
        }

    }

    
}