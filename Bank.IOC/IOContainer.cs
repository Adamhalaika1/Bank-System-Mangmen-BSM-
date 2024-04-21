using Bank.Repositories;
using Bank.Repositories.IRepository;
using Bank.Repositories.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.IOC
{
    public static class IOContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanPaymentRepository, LoanPaymentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }
    }
}