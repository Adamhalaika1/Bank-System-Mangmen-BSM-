// FileHandler.cs
using BankSystem1;
using System;
using System.Collections.Generic;
using System.IO;

class FileHandler 
{
    public static void WriteAccountsToFile(List<Account> accounts)
    {
        try
        {
            //when Add New Account I want to save the last Id .
            List<string> lines = new List<string>();
            int newId = 1;

            if (File.Exists("Files/A.txt"))
            {
                string[] existingLines = File.ReadAllLines("Files/A.txt");
                if (existingLines.Length > 0)
                {
                    string lastLine = existingLines[existingLines.Length - 1];
                    string[] lastLineParts = lastLine.Split(',');
                    int lastId = int.Parse(lastLineParts[0]);
                    newId = lastId + 1;
                }
            }

            using (StreamWriter writer = new StreamWriter("Files/A.txt", append: true))
            {
                foreach (var user in accounts)
                {
                    writer.WriteLine($"{newId},{user.Email},{user.Name},{user.Age},{user.Balance},{user.IsActive},{user.UserType}");
                    newId++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing accounts file: {ex.Message}");
        }
    }
    public static void WriteAuditToFile(List<Account> accounts)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("Files/B.txt", append: true))
            {
                writer.WriteLine($"Audit - {DateTime.Now}");
                writer.WriteLine("-------------------------------------------------");

                foreach (var account in accounts)
                {
                    writer.WriteLine($"Email: {account.Email}, Name: {account.Name},{account.Age}, Balance: ${account.Balance},{account.PhoneNumber}");
                }

                writer.WriteLine("-------------------------------------------------\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing audit file: {ex.Message}");
        }
    }
}
