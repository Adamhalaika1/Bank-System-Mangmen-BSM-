using BankSystem1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Xml.Linq;

class BankSystem
{
    //static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "A.txt");

    private static bool isRunning = true;
    static List<Account> accounts = new List<Account>();
    static void Main()
    {
        Console.WriteLine("Welcome to the Bank System");
        while (isRunning)
        {
            User user = null;
            do
            {
                string inputUsername = GetInput("Enter username: ");
                string inputPassword = GetInput("Enter password: ");
                user = ValidateUser(inputUsername, inputPassword);
                if (user == null)
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                }
            } while (user == null);

            user.PrintUserType();
            MainMenu(user);
        }
    }
    static private string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
    static User ValidateUser(string username, string password)
    {
        User user = null;
        string[] lines = File.ReadAllLines("Files/A.txt");

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            int Id = int.Parse(parts[0].Trim());
            string Email = parts[1].Trim();
            string Name = parts[2].Trim();
            int Age = int.Parse(parts[3].Trim());
            decimal Balance = decimal.Parse(parts[4].Trim());
            string Status = parts[5].Trim();
            string Role = parts[6].Trim();
            if (Email == username && Name == password)
            {
                user = new User(Id, Email, Name, Age, Balance, Status, Role == "Admin" ? User.UserTypes.Admin : User.UserTypes.User);
            }
        }
        return user;
    }
    private static void MainMenu(User user)
    {
        if (user.UserType== User.UserTypes.Admin)
        {
            AdminMainMenu(user);
        }
        else if (user.UserType == User.UserTypes.User)
        {
            UserMainMenu(user);
        }
    }
    private static void AdminMainMenu(User user)
    {
        //string filePath = @"E:\\BankSystem1-master\\A.txt";

        while (isRunning)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. View Active Accounts");
            Console.WriteLine("4. Activate/Deactivate Account");
            Console.WriteLine("5. Withdraw Money");
            Console.WriteLine("6. Update Customer Info");
            Console.WriteLine("7. Delete Account");
            Console.WriteLine("8. Exit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        ViewActiveAccounts("Files/A.txt");
                        break;
                    case 4:
                        ActivateDeactivateAccount();
                        break;
                    case 5:
                        WithdrawMoney();
                        break;
                    case 6:
                        UpdateCustomerInfo();
                        break;
                    case 7:
                        DeleteAccount();
                        break;
                    case 8:
                        Main();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
    private static void UserMainMenu(User user)
    {
        Account account = accounts.FirstOrDefault(a => a.Email == user.Email);

        while (isRunning)
        {

            Console.WriteLine("\nCustomer Menu:");
            Console.WriteLine("1. View Account Info");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Deposit Money");
            Console.WriteLine("5. Update my account");
            Console.WriteLine("4. Logout");

            int customerChoice;
            if (int.TryParse(Console.ReadLine(), out customerChoice))
            {

                Transaction transaction = new Transaction(customerChoice,user,account);
                transaction.ProcessTransaction();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
    static void DepositMoney()
    {
        Console.WriteLine("\nDepositing Money...");
        Console.Write("Enter Account Email to Deposit Money: ");
        string email = Console.ReadLine();
        Account account = accounts.Find(a => a.Email == email);

        if (account != null)
        {
            Console.Write("Enter Amount to Deposit: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                account.Deposit(amount);
                Console.WriteLine($"Successfully deposited ${amount} to {account.Email}. New balance: ${account.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid amount. Deposit failed.");
            }
        }
        else
        {
            Console.WriteLine("Account not found. Deposit failed.");
        }
    }
    static void ViewActiveAccounts(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        Console.WriteLine("Active Accounts:");
        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            if (data.Length >= 2 && data[5].Equals("True", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"ID: {data[0]}, Email: {data[1]}, Name: {data[2]}, Age: {data[3]}, Balance: {data[4]} Role: {data[6]}");
            }
        }
    }
    static void ActivateDeactivateAccount()
    {
        Console.WriteLine("\nActivating/Deactivating Account...");
        Console.Write("Enter Account Email to Activate/Deactivate: ");
        string email = Console.ReadLine();
        Account account = accounts.Find(a => a.Email == email);
        if (account != null)
        {
            account.ToggleActiveStatus();
            string status = account.IsActive ? "activated" : "deactivated";
            Console.WriteLine($"Account {email} {status} successfully.");
        }
        else
        {
            Console.WriteLine("Account not found. Operation failed.");
        }
    }
    static void CreateAccount()
    {
        Console.WriteLine("\nCreating Account...");
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter Role: ");
        Console.WriteLine("1.Admin");
        Console.WriteLine("2.User");
        string role = Console.ReadLine();
        switch (role)
        {
            case "1":
                Console.WriteLine("You selected Admin");
                break;
            case "2":
                Console.WriteLine("You selected User");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        Console.Write("Enter Date of Birth (MM/DD/YYYY): ");
        DateTime dob;
        if (!DateTime.TryParse(Console.ReadLine(), out dob) || dob > DateTime.Now)
        {
            Console.WriteLine("Invalid date of birth. Account creation failed.");
            return;
        }
        int age = DateTime.Now.Year - dob.Year;
        if (DateTime.Now.Month < dob.Month || (DateTime.Now.Month == dob.Month && DateTime.Now.Day < dob.Day))
        {
            age--;
        }
        Console.Write("Enter Initial Balance: ");
        decimal balance;
        if (!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0)
        {
            Console.WriteLine("Invalid initial balance. Account creation failed.");
            return;
        }

        Console.WriteLine("Account created successfully!");
        Account account = new Account(email, name, age, balance, role =="Admin" ? Account.UserTypes.Admin : Account.UserTypes.User);
        accounts.Add(account);
        accounts.Find(a => a.Email == email);
        Console.WriteLine($"Email: {account.Email}, Name: {account.Name},Balance: {account.Balance} {account.Age},UserType: {account.UserType}");
        FileHandler.WriteAccountsToFile(accounts);
    }
    static void DeleteAccount()
    {
        Console.WriteLine("\nDeleting Account...");
        Console.Write("Enter Account Email to Delete: ");
        string email = Console.ReadLine();
        // Find the account by email
        Account account = accounts.Find(a => a.Email == email);
        if (account != null)
        {
            accounts.Remove(account);
            Console.WriteLine($"Account {email} deleted successfully.");
        }
        else
        {
            Console.WriteLine("Account not found. Deletion failed.");
        }
    }
    static void WithdrawMoney()
    {
        Console.WriteLine("\nWithdrawing Money...");
        Console.Write("Enter Account Email to Withdraw Money: ");
        string email = Console.ReadLine();
        Account account = accounts.Find(a => a.Email == email);
        if (account != null)
        {
            Console.Write("Enter Amount to Withdraw: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
            {
                if (account.Withdraw(amount))
                {
                    Console.WriteLine($"Successfully withdrew ${amount} from {account.Email}. New balance: ${account.Balance}");
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
        else
        {
            Console.WriteLine("Account not found. Withdrawal failed.");
        }
    }
    static void UpdateCustomerInfo()
    {
        Console.WriteLine("\nUpdating Customer Information...");
        Console.Write("Enter the Email of the customer you want to update: ");
        string emailToUpdate = Console.ReadLine();




        Account customer = accounts.Find(a => a.Email == emailToUpdate);


        if (customer == null)
        {
            Console.WriteLine("Customer not found. Update failed.");
            return;
        }

        Console.WriteLine("Enter the new Name: ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName))
        {
            customer.Name = newName;
        }
        Console.WriteLine("Enter the new Role (1 for Admin, 2 for User): ");
        string newRole = Console.ReadLine();
        if (!string.IsNullOrEmpty(newRole))
        {
            switch (newRole)
            {
                case "1":
                    customer.UserType = Account.UserTypes.Admin;
                    break;
                case "2":
                    customer.UserType = Account.UserTypes.User;
                    break;
                default:
                    Console.WriteLine("Invalid role. Update failed.");
                    return;
            }
        }
        Console.WriteLine("Enter the new Date of Birth (MM/DD/YYYY, leave blank to keep current): ");
        string dobInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(dobInput))
        {
            if (!DateTime.TryParse(dobInput, out DateTime newDOB) || newDOB > DateTime.Now)
            {
                Console.WriteLine("Invalid date of birth. Update failed.");
                return;
            }
            customer.Age = DateTime.Now.Year - newDOB.Year;
            if (DateTime.Now.Month < newDOB.Month || (DateTime.Now.Month == newDOB.Month && DateTime.Now.Day < newDOB.Day))
            {
                customer.Age--;
            }
        }
        Console.WriteLine("Enter the new Balance : ");
        string balanceInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(balanceInput))
        {
            if (!decimal.TryParse(balanceInput, out decimal newBalance) || newBalance < 0)
            {
                Console.WriteLine("Invalid balance. Update failed.");
                return;
            }
            customer.Balance = newBalance;
        }
        Console.WriteLine("Customer information updated successfully!");
        Console.WriteLine($"Email: {customer.Email}, Name: {customer.Name}, Balance: {customer.Balance}, Age: {customer.Age}, UserType: {customer.UserType}");
        FileHandler.WriteAuditToFile(accounts);
    }

}












