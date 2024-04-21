using Bank.Entities;
using Bankbank.DataContext;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.DataAccess
{
    public class AccountRepository 
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            return _context.Accounts.Where(item => item.CustomerId == customerId).ToList();
        }

        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public void UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }

        public void DepositAmount(int accountId, decimal amount)
        {
            var account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                account.CurrentBalance += amount;
                _context.SaveChanges();
            }
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                account.CurrentBalance -= amount;
                _context.SaveChanges();
            }
        }
    }
}
