using Bankbank.Entities;
using Bankbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankbank.Repo
{
    internal class GetFunctionUser 
    {

        public void AddUser(UserService userService)
        {
            User newUser = new User
            {
                //Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 5, 15),
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Address = "123 Main St",
                CustomerType = CustomerType.Legal,
                City = "New York",
                UserType = User.Role.Customer
            };

            userService.AddUser(newUser);
        }

        public void RetrieveUser(UserService userService)
        {
            User retrievedUser = userService.GetUserById(1);
            if (retrievedUser != null)
            {
                Console.WriteLine($"Retrieved User: {retrievedUser.FirstName} {retrievedUser.LastName}");
            }
        }

        public void UpdateUser(UserService userService)
        {
            User retrievedUser = userService.GetUserById(1);
            if (retrievedUser != null)
            {
                retrievedUser.FirstName = "Jane";
                userService.UpdateUser(retrievedUser);
            }
        }

        public void DeleteUser(UserService userService)
        {
            userService.DeleteUser(1);
        }

        public void ListAllUsers(UserService userService)
        {
            IEnumerable<User> allUsers = userService.GetAllUsers();
            foreach (var user in allUsers)
            {
                Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            }

        }
    }
}
