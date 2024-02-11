// BankSystem.cs
using BankSystem1;
using System;
using System.Collections.Generic;
using System.Security.Principal;

class BankSystem
{
    static Account loggedInAccount;
    static List<Account> accounts;
    static void Main()
    {
        accounts = FileHandler.ReadAccountsFromFile();
        bool isAdmin = false;
        bool isUser = false;
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Welcome to the Bank System");
            Console.Write("Enter username: ");
            string inputUsername = Console.ReadLine();

            Console.Write("Enter password: ");
            string inputPassword = Console.ReadLine();

            string filePath = @"E:\\BankSystem1-master\\A.txt";
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2 && parts[1].Trim() == inputUsername && parts[2].Trim() == inputPassword)
                {
                    if (parts[6].Trim() == "Admin")
                    {
                        isAdmin = true;

                    }
                    else
                    {
                        isAdmin = false;
                    }
                }
                if (parts.Length >= 2 && parts[1].Trim() == inputUsername && parts[2].Trim() == inputPassword)
                {

                    if (parts[6].Trim() == "User")
                    {
                        isUser = true;

                    }
                    else
                    {
                        isUser = true;
                    }
                }
            }
            break;
        }
        if (isAdmin)
        {
            Console.WriteLine("Login successful as admin!");

            // Main menu loop for Admin

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
                            ViewActiveAccounts();
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
                        case 8:
                            isRunning = false;
                            break;
                        case 7:
                            DeleteAccount();
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



        if (isUser)
        {
            bool customerMenu = true;
            while (customerMenu)
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
                    switch (customerChoice)
                    {
                        case 1:
                            ViewAccountInfo();
                            break;
                        case 2:
                            WithdrawMoney();
                            break;
                        case 3:
                            DepositMoney();
                            break;
                        case 4:
                            customerMenu = false;
                            Console.WriteLine("Logged out as customer.");
                            break;
                        case 5:
                            UpdateCustomerInfo();
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



        else
        {
            Console.WriteLine("Login failed. Exiting...");
        }

        //// Save accounts to file
        FileHandler.WriteAccountsToFile(accounts);

        //// Save audit file
        FileHandler.WriteAuditToFile(accounts, loggedInAccount);

    }
    static void DepositMoney()
    {
        Console.WriteLine("\nDepositing Money...");

        // Display active accounts
        ViewActiveAccounts();

        Console.Write("Enter Account Email to Deposit Money: ");
        string email = Console.ReadLine();

        // Find the account by email
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
    static void ViewActiveAccounts()
    {
        Console.WriteLine("\nActive Accounts:");
        //List<Account> accounts = FileHandler.ReadAccountsFromFile();
        foreach (var account in accounts)
        {
            if (account.IsActive)
            {
                Console.WriteLine($"Email: {account.Email}, Name: {account.Name}, Balance: ${account.Balance}");
            }
        }
    }



    static void ActivateDeactivateAccount()
    {
        Console.WriteLine("\nActivating/Deactivating Account...");

        // Display active accounts
        ViewActiveAccounts();

        Console.Write("Enter Account Email to Activate/Deactivate: ");
        string email = Console.ReadLine();

        // Find the account by email
        Account account = accounts.Find(a => a.Email == email);
        //Account account = accounts.Find(a => a.Id == Id);

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

        // Get user input for account creation
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Role: ");
        string role = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age;
        if (!int.TryParse(Console.ReadLine(), out age) || age < 0)
        {
            Console.WriteLine("Invalid age. Account creation failed.");
            return;
        }

        Console.Write("Enter Initial Balance: ");
        decimal balance;
        if (!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0)
        {
            Console.WriteLine("Invalid initial balance. Account creation failed.");
            return;
        }

        // Create account and add to the list
        Account newAccount = new Account(email, name, age, balance,role);
        accounts.Add(newAccount);

        Console.WriteLine("Account created successfully!");
    }


    static void DeleteAccount()
    {
        Console.WriteLine("\nDeleting Account...");

        // Display active accounts
        ViewActiveAccounts();

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

        // Display active accounts
        ViewActiveAccounts();

        Console.Write("Enter Account Email to Withdraw Money: ");
        string email = Console.ReadLine();

        // Find the account by email
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
        Console.WriteLine("\nUpdating Customer Info...");

        // Display active accounts
        ViewActiveAccounts();

        Console.Write("Enter Account Email to Update Info: ");
        string email = Console.ReadLine();

        // Find the account by email
        Account account = accounts.Find(a => a.Email == email);

        if (account != null)
        {
            Console.Write("Enter New Email: ");
            string newEmail = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string newPhone = Console.ReadLine();

            account.UpdateInfo(newEmail, newPhone);
            Console.WriteLine($"Customer info for {account.Email} updated successfully.");
        }
        else
        {
            Console.WriteLine("Account not found. Operation failed.");
        }
    }


    static void ViewAccountInfo()
    {
        List<Account> accounts = FileHandler.ReadAccountsFromFile();
        Console.WriteLine(accounts);



    }


}