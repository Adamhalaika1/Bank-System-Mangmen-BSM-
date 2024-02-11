// FileHandler.cs
using BankSystem1;
using System;
using System.Collections.Generic;
using System.IO;

class FileHandler
{
    private static string accountsFilePath = @"E:\\BankSystem1-master\\A.txt";
    private static string auditFilePath = @"E:\\BankSystem1-master\\B.txt";

    public static List<Account> ReadAccountsFromFile()
    {
        List<Account> accounts = new List<Account>();

        try
        {
            using (StreamReader reader = new StreamReader(accountsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] accountData = line.Split(',');

                }
            }


        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error reading accounts file: {ex.Message}");
        }

        return accounts;
    }

    public static void WriteAccountsToFile(List<Account> accounts)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(accountsFilePath ,append: true))
            {
                foreach (var account in accounts)
                {
                    writer.WriteLine($"{account.Id},{account.Email},{account.Name},{account.Age},{account.Balance},{account.IsActive},{account.Role}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing accounts file: {ex.Message}");
        }
    }

    public static void WriteAuditToFile(List<Account> accounts, Account loggedInAccount)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(auditFilePath, append: true))
            {
                writer.WriteLine($"Audit - {DateTime.Now}");
                writer.WriteLine("-------------------------------------------------");

                foreach (var account in accounts)
                {
                    writer.WriteLine($"Email: {account.Email}, Name: {account.Name}, Balance: ${account.Balance}");
                }

                writer.WriteLine("-------------------------------------------------\n");

                if (loggedInAccount != null)
                {
                    writer.WriteLine($"Operation by Admin - Customer: {loggedInAccount.Email}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing audit file: {ex.Message}");
        }
    }
}
