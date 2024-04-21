using Bank.Entities;
using Bank.DataAccess;
using System;
using Bankbank.Entities;

namespace Bank.Controllers
{

    public class UserController
    {
        public static User currentUser;
        public static User Login()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║        Welcome to the Banking      ║");
            Console.WriteLine("║             System                 ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();

            bool isValidLogin = false;
            int attempts = 3; // Number of login attempts allowed
            while (!isValidLogin && attempts > 0)
            {
                Console.Write(" - Enter your email: ");
                string email = Console.ReadLine();
                Console.Write(" - Enter your password: ");
                string password = Console.ReadLine();

                // Check if the credentials are valid
                currentUser = UserRepository.GetUserByEmailAndPassword(email, password);
                Console.Clear();
                if (currentUser != null)
                {
                    isValidLogin = true;
                    Console.WriteLine("Login successful....");
                    Console.WriteLine();
                    Console.WriteLine("Welcome >> " + currentUser.UserName);
                }
                else
                {
                    attempts--;
                    if (attempts > 0)
                    {
                        Console.WriteLine("Invalid credentials. Please try again. Attempts left: " + attempts);
                    }
                    else
                    {
                        Console.WriteLine("Maximum login attempts reached. Please try again after 5 minutes.");
                        int remainingTime = 300; // 5 minutes in seconds
                        while (remainingTime > 0)
                        {
                            Console.Write("Try again in: " + TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss") + "\r"); // \r moves cursor to beginning of line
                            System.Threading.Thread.Sleep(1000); // 1 second delay
                            remainingTime--;
                        }
                        Console.Clear();
                        Console.WriteLine("Try again in: 00:00");
                        attempts = 3;
                    }
                }
            }

            return currentUser;
        }
        public static string Back()
        {
            var key = Console.ReadKey(intercept: true);
            if (key.KeyChar == 'b' || key.KeyChar == 'B')
            {
                Console.WriteLine("\nGoing back...");
                Thread.Sleep(2000);
                return null; // Return null to indicate 'b' was pressed
            }
            string Back = key.KeyChar + Console.ReadLine();
            return Back;
        }
    }
}
