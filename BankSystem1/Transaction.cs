using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem1
{
    public class Transaction
    {
        public User _user;
        public Account _account;
        private readonly int _id;
        public Transaction(int id, User user,Account account)
        {
            _id = id;
            _user = user;
            _account = account;
        }

        public void ProcessTransaction()
        {
            switch (_id)
            {
                case 1:
                    ViewLoggedInUserAccountInfo(_user);
                    break;
                case 2:
                    WithdrawMoney();
                    break;
                case 3:
                    DepositMoney();
                    break;
                case 4:
                    Logout();
                    break;
                case 5:
                    UpdateCustomerInfo();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
        private void ViewLoggedInUserAccountInfo(User user)
        {
            Console.WriteLine("User Information:");
            Console.WriteLine($"Id: {user.Id}");
            Console.WriteLine($"Email: {_account.Email}");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Age: {user.Age}");
            Console.WriteLine($"Balance: {_account.Balance}");
            Console.WriteLine($"Status: {user.Status}");
            Console.WriteLine($"Role: {user.UserType}");
        }
        public void WithdrawMoney()
        {
            Console.WriteLine("Enter the amount");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                Console.WriteLine($"amount=: {amount}");
                if (_account.Withdraw(amount))
                {
                    Console.WriteLine($"Successfully withdrew ${amount} from {_account.Email}. New balance: ${_account.Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds. Withdrawal failed.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Withdrawal failed.");
            }
        }
        private void DepositMoney()
        {
            Console.Write("Enter Amount to Deposit: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                _account.Deposit(amount);

                Console.WriteLine($"Successfully deposited ${amount} to {_account.Email}. New balance: ${_account.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Deposit failed.");
            }
        }
        private void UpdateCustomerInfo()
        {
            Console.WriteLine("Updating customer info...");

            if (_account != null)
            {
                Console.Write("Enter New Email: ");
                string newEmail = Console.ReadLine();

                _account.UpdateInfo(newEmail);
                Console.WriteLine($"Customer info for {_account.Email} updated successfully.");
            }

        }
        private void Logout()
        {
            // Implement logic to logout
            Console.WriteLine("Logged out as customer.");

        }
    }

}
