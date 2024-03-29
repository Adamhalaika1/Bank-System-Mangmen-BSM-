﻿// FileHandler.cs
using BankSystem1;
using System;
using System.Collections.Generic;
using System.IO;

class FileHandler 
{
    static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "A.txt");
    static string auditFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "B.txt");
    public static void WriteAccountsToFile(List<Account> accounts)
    {
        try
        {
            //when Add New Account I want to save the last Id .
            List<string> lines = new List<string>();
            int newId = 1;

            if (File.Exists(filePath))
            {
                string[] existingLines = File.ReadAllLines(filePath);
                if (existingLines.Length > 0)
                {
                    string lastLine = existingLines[existingLines.Length - 1];
                    string[] lastLineParts = lastLine.Split(',');
                    int lastId = int.Parse(lastLineParts[0]);
                    newId = lastId + 1;
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath, append: true))
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
            using (StreamWriter writer = new StreamWriter(auditFilePath, append: true))
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
