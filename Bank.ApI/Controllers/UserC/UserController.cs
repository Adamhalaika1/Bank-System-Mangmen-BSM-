using Bank.Repositories.IRepository;
using Bankbank.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers.UserC
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly IAdminRepository _adminRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UserController(ICustomerRepository customerRepository,IAdminRepository adminRepository, IEmployeeRepository employeeRepository)
        {
            _customerRepository = customerRepository;
            _adminRepository = adminRepository;
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _customerRepository.GetAll();
            return Ok(users);
        }
        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var Customer = _customerRepository.GetAll().Where(x => x.UserType == Role.Customer).ToList();
            return Ok(Customer);
        }
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var Employee = _customerRepository.GetAll().Where(x => x.UserType == Role.Employee).ToList();
            return Ok(Employee);
        }
        [HttpGet]
        public IActionResult GetCustomerByEmail(string Email)
        {
            var Customer = _customerRepository.GetAll().FirstOrDefault(x => x.Email == Email && x.UserType ==Role.Customer);
            if (Customer != null)
            {
                return Ok(Customer);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddNewCustomer([FromBody] User model)
        {
            if (model == null)
                return BadRequest();
            User user = new User
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                Password=model.Password,
                UserType=Role.Customer,
                Address=model.Address,
                DateOfBirth=model.DateOfBirth,
            };
            _customerRepository.Add(user);
            return Ok("New Customer added successfully");
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(string email)
        {
            var customer = _customerRepository.GetAll().FirstOrDefault(x => x.Email == email && x.UserType == Role.Customer);
            if (customer == null)
            {
                return NotFound($"Customer with Id = {email} not found");
            }
            _customerRepository.Delete(customer);
            return Ok("Customer deleted successfully");
        }

        [HttpPut]
        public IActionResult UpdateCustomer(string email, [FromBody] User model)
        {
            if (model == null)
                return BadRequest();

            var customerToUpdate = _customerRepository.GetAll().FirstOrDefault(x => x.Email == email && x.UserType == Role.Customer);
            if (customerToUpdate == null)
                return NotFound();

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                customerToUpdate.FirstName = model.FirstName;
            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                customerToUpdate.LastName = model.LastName;
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                customerToUpdate.Address = model.Address;
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                customerToUpdate.Password = model.Password;
            }
            if (model.UserType != default(Role))
            {
                customerToUpdate.UserType = model.UserType;
            }
            if (!string.IsNullOrEmpty(model.DateOfBirth.ToString()))
            {
                customerToUpdate.DateOfBirth = model.DateOfBirth;
            }
            _customerRepository.Update(customerToUpdate);
            return Ok("Customer updated successfully");
        }



    }
}
