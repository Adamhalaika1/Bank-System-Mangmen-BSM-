using Bankbank.DataContext;
using Bankbank.Entities;
using Bankbank.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bankbank.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanRepository _loanRepository;

        public LoanController(AppDbContext context)
        {
            _loanRepository = new LoanRepository(context);
        }



    }
}
