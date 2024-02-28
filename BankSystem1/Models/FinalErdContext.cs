using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankSystem1.Models
{
    public partial class FinalErdContext : DbContext
    {
        public FinalErdContext()
        {
        }

        public FinalErdContext(DbContextOptions<FinalErdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<LoanPayment> LoanPayments { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HTF1MOF;Database=FinalErd;Trusted_Connection=True;User Id=sa;password=12345;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.BranchId).HasColumnName("Branch_id");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_id");

                entity.Property(e => e.DateOpened).HasColumnType("date");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Account__Branch___4AB81AF0");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customer_Account");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CustomerType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Type");

                entity.Property(e => e.UsersId).HasColumnName("Users_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("Users_Customer");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.BranchId).HasColumnName("Branch_id");

                entity.Property(e => e.EmploymentDate).HasColumnType("date");

                entity.Property(e => e.ManagerId).HasColumnName("Manager_id");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsersId).HasColumnName("Users_id");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Employee__Branch__45F365D3");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("Users_Employee_Manager_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users_Employee");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("Loan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_id");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LoanStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LoanType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Customer_Loan");
            });

            modelBuilder.Entity<LoanPayment>(entity =>
            {
                entity.ToTable("LoanPayment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.LoanId).HasColumnName("Loan_id");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.SchedulePaymentDate).HasColumnType("date");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanPayments)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("Loan_Account");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("Account_id");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.LoanPaymentId).HasColumnName("LoanPayment_id");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Accou__4E88ABD4");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account_Customer_LoanPayment_Employee_Transactions");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Emplo__4F7CD00D");

                entity.HasOne(d => d.LoanPayment)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.LoanPaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__LoanP__4D94879B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
