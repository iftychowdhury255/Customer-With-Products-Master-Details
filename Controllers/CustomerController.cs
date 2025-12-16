using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerCoreApi.Data;
using CustomerCoreApi.Models;

namespace CustomerCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;
        public CustomerController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _dbContext.Customers.ToList();
            return Ok(customers);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetCustomerID(Guid CustomerId)
        {
            var customer = _dbContext.Customers.Find(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var newCustomer = new Customer()
            {
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                TotalOrders = customer.TotalOrders
            };
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
            return Ok(newCustomer);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateCustomer(Guid CustomerId, Customer customer)
        {
            var existingCustomer = _dbContext.Customers.Find(CustomerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.TotalOrders = customer.TotalOrders;
            _dbContext.SaveChanges();
            return Ok(existingCustomer);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteCustomer(Guid CustomerId)
        {
            var existingCustomer = _dbContext.Customers.Find(CustomerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _dbContext.Customers.Remove(existingCustomer);
            _dbContext.SaveChanges();
            return Ok(existingCustomer);
        }
    }
}
