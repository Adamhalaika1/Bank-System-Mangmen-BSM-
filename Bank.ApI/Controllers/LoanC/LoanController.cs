using Bank.Repositories.IRepository;
using Bank.Repositories.Repository;
using Bankbank.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bank.API.Controllers.LoanC

{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoanController : Controller
    {
        private readonly ILoanRepository _LoanRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ILoanPaymentRepository _LoanPaymentRepository;
        public LoanController(ILoanRepository LoanRepository, IUserRepository UserRepository,ILoanPaymentRepository LoanPaymentRepository)
        {
            _LoanRepository = LoanRepository;
            _UserRepository = UserRepository;
            _LoanPaymentRepository = LoanPaymentRepository;
        }

        [HttpPut]
        public IActionResult UpdateLoan(int customerId, int loanType, [FromBody] Loan model)
        {
            if (model == null)
                return BadRequest();
            var LoanToUpdate = _LoanRepository.GetAll()
                .Where(x => x.Customer.Id == customerId && x.LoanType == (LoanType)loanType)
                .FirstOrDefault();

            if (LoanToUpdate == null)
                return NotFound();
            if (model.LoanAmount != 0)
            {
                LoanToUpdate.LoanAmount = model.LoanAmount;
            }
            if (model.EndDate != null)
            {
                LoanToUpdate.EndDate = model.EndDate;
            }
            if (model.LoanType != default(LoanType))
            {
                LoanToUpdate.LoanType = model.LoanType;
            }
            if (model.LoanStatus != default(LoanStatus))
            {
                LoanToUpdate.LoanStatus = model.LoanStatus;
            }
            if (model.LoanAmount != null)
            {
                LoanToUpdate.LoanAmount = model.LoanAmount;
            }
            if (model.InterestAmount != null)
            {
                LoanToUpdate.InterestAmount = model.InterestAmount;
            }
            _LoanRepository.Update(LoanToUpdate);
            return Ok("Loan updated successfully");
        }
        [HttpPost]
        public IActionResult AddNewLoan(string Email, [FromBody] Loan model)
        {
            if (model == null)
                return BadRequest();
            var user = _UserRepository.GetByEmail(Email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            Loan loan = new Loan
            {
                InterestAmount = model.InterestAmount,
                LoanStatus = model.LoanStatus,
                LoanAmount = model.LoanAmount,
                LoanType = model.LoanType,
                EndDate = model.EndDate,
                CustomerId = model.CustomerId,
                InterestRate = model.InterestRate,
                StartDate = model.StartDate,
            };
            _LoanRepository.Add(loan);
            int loanId = loan.Id;
            LoanPayment loanPayment = new LoanPayment
            {
                PaymentAmount = 300,
                SchedulePaymentDate = DateTime.Now,
                MonthlyPayment = 300,
                LoanId = loanId,
            };
            _LoanPaymentRepository.Add(loanPayment);
            return Ok("New Loan added successfully");
        }
        [HttpDelete]
        public IActionResult DeleteAllLoanForUser(string Email)
        {
            var user = _UserRepository.GetByEmail(Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var loansToDelete = _LoanRepository.GetAll().Where(x => x.CustomerId == user.Id).ToList();
            if (loansToDelete.Count == 0)
            {
                return NotFound("No loans found for the user");
            }

            foreach (var loan in loansToDelete)
            {
                _LoanRepository.Delete(loan);
            }

            return Ok($"All loans deleted successfully for user with email: {Email}");
        }

        [HttpDelete]
        public IActionResult DeleteLoanForUser(string Email, LoanType loanType)
        {
            var user = _UserRepository.GetByEmail(Email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var loansToDelete = _LoanRepository.GetAll().Where(x => x.CustomerId == user.Id && x.LoanType == loanType).ToList();
            if (loansToDelete == null || !loansToDelete.Any())
            {
                return NotFound($"No {loanType} loans found for the user");
            }

            foreach (var loan in loansToDelete)
            {
                _LoanRepository.Delete(loan);
            }

            return Ok($"{loanType.ToString()} loans deleted successfully for user with email: {Email}");
        }




    }
}