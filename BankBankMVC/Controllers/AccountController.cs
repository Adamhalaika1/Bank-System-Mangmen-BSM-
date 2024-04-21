using Bank.DataAccess;
using System;
using System.Collections.Generic;

namespace Bank.Controllers
{
    public class AccountController
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void DepositAmount(int accountId, decimal amount)
        {
            _accountRepository.DepositAmount(accountId, amount);
        }

        public void Withdraw(int accountId, decimal amount)
        {
            _accountRepository.Withdraw(accountId, amount);
        }
    }
}
