using Bankbank.Entities;
using Microsoft.EntityFrameworkCore;
namespace Bankbank.DataContext
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-HTF1MOF;Database=Bank;User Id=sa;Password=12345;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<LoanPayment> LoanPayment { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

    }
}
