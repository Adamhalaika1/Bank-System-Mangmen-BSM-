﻿// <auto-generated />
using System;
using Bankbank.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bankbank.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240317231204_sadadsdasddhjvhj")]
    partial class sadadsdasddhjvhj
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bankbank.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountStatus")
                        .HasColumnType("int");

                    b.Property<string>("AccountType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOpened")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Bankbank.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("Bankbank.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("InterestAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("InterestRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LoanAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("LoanStatus")
                        .HasColumnType("int");

                    b.Property<int>("LoanType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("Bankbank.Entities.LoanPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LoanId")
                        .HasColumnType("int");

                    b.Property<decimal?>("MonthlyPayment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("SchedulePaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LoanId")
                        .IsUnique()
                        .HasFilter("[LoanId] IS NOT NULL");

                    b.ToTable("LoanPayment");
                });

            modelBuilder.Entity("Bankbank.Entities.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("LoanPaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Withdraw")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LoanPaymentId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Bankbank.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bankbank.Models.UserBranch", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BranchId");

                    b.HasIndex("BranchId");

                    b.ToTable("UserBranches");
                });

            modelBuilder.Entity("Bankbank.Entities.Account", b =>
                {
                    b.HasOne("Bankbank.Entities.User", "Customer")
                        .WithMany("Account")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bankbank.Entities.Loan", b =>
                {
                    b.HasOne("Bankbank.Entities.User", "Customer")
                        .WithMany("Loan")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Bankbank.Entities.LoanPayment", b =>
                {
                    b.HasOne("Bankbank.Entities.Loan", "Loan")
                        .WithOne("LoanPayment")
                        .HasForeignKey("Bankbank.Entities.LoanPayment", "LoanId");
                });

            modelBuilder.Entity("Bankbank.Entities.Transactions", b =>
                {
                    b.HasOne("Bankbank.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");

                    b.HasOne("Bankbank.Entities.User", "Employee")
                        .WithMany("Transactions")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Bankbank.Entities.LoanPayment", "LoanPayment")
                        .WithMany("Transactions")
                        .HasForeignKey("LoanPaymentId");
                });

            modelBuilder.Entity("Bankbank.Entities.User", b =>
                {
                    b.HasOne("Bankbank.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId");
                });

            modelBuilder.Entity("Bankbank.Models.UserBranch", b =>
                {
                    b.HasOne("Bankbank.Entities.Branch", "Branch")
                        .WithMany("UserBranches")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bankbank.Entities.User", "User")
                        .WithMany("UserBranches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
