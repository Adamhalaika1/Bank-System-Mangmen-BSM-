using Bankbank.Entities;
using Bankbank.Models;
using Microsoft.EntityFrameworkCore;
namespace Bankbank.DataContext
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-HTF1MOF;Database=Bank;User Id=sa;Password=12345;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBranch>()
                .HasKey(ub => new { ub.UserId, ub.BranchId });

            modelBuilder.Entity<UserBranch>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBranches)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBranch>()
                .HasOne(ub => ub.Branch)
                .WithMany(b => b.UserBranches)
                .HasForeignKey(ub => ub.BranchId);
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<LoanPayment> LoanPayment { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public DbSet<UserBranch> UserBranches { get; set; }


    }
}
