using Bank.Repositories.IRepository;
using Bank.Repositories.Repository;
using Bankbank.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bank.API.Controllers.AccountC

{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPut]
        public IActionResult UpdateAccount(int accountId, [FromBody] Account model)
        {
            if (model == null)
                return BadRequest();
            var accountToUpdate = _accountRepository.GetAll().Where(x => x.Id == accountId).FirstOrDefault();
            if (accountToUpdate == null)
                return NotFound();
            // Check if AccountType is provided in the request before updating

            if (model.AccountType != null)
            {
                accountToUpdate.AccountType = model.AccountType;
            }
            accountToUpdate.AccountStatus = model.AccountStatus;
            accountToUpdate.CurrentBalance = model.CurrentBalance;


            _accountRepository.Update(accountToUpdate);
            return Ok("Account updated successfully");
        }
        [HttpPost]
        public IActionResult AddNewAccount([FromBody] Account model)
        {
            if (model == null)
                return BadRequest();
            var lastAccount = _accountRepository.GetAll().OrderBy(a => a.Id).LastOrDefault();

            Account account = new Account
            {
                CurrentBalance = model.CurrentBalance,
                AccountStatus = AccountStatus.Active,
                CustomerId = model.CustomerId,
                DateOpened = DateTime.Now,
            };

            _accountRepository.Add(account);
            return Ok("New account added successfully");
        }

        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            var account = _accountRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound($"Account with Id = {id} not found");
            }
            _accountRepository.Delete(account);
            return Ok("Account deleted successfully");
        }
        [HttpGet]
        public IActionResult GetAllAccount()
        {
            var result = _accountRepository.GetAll().ToList();
            string jsonString = JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAccountById(int id)
        {
            var account = _accountRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound($"Account with Id = {id} not found");
            }

            return Ok(account);
        }

        //another method to Get GetAccountById2
        [HttpGet]
        public IActionResult GetAccountById2(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
            {
                return NotFound($"Account with Id = {id} not found");
            }
            return Ok(account);
        }

        [HttpPut]
        public IActionResult Withdraw(int accountId, decimal amount)
        {
            _accountRepository.Withdraw(accountId, amount);
            return Ok("Withdrawal successful");
        }

        [HttpPut]
        public IActionResult Deposit(int accountId, decimal amount)
        {
            _accountRepository.Deposit(accountId, amount);
            return Ok("Deposit successful");
        }
    }
}











