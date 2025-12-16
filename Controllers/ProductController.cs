using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerCoreApi.Data;
using CustomerCoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;
        public ProductController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _dbContext.Products
                .Include(p => p.ProductDetails)
                .ThenInclude(pd => pd.Customer)
                .ToList();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid ProductId)
        {
            var product = _dbContext.Products
                .Include(p => p.ProductDetails)
                .ThenInclude(pd => pd.Customer)
                .FirstOrDefault(p => p.ProductId == ProductId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var newProduct = new Product()
            {
                PName = product.PName,
                Description = product.Description
            };
            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            if (product.ProductDetails != null)
            {
                foreach (var pd in product.ProductDetails)
                {
                    var existingCustomer = _dbContext.Customers.Find(pd.CustomerId);
                    if (existingCustomer != null)
                    {
                        var newProductDetails = new ProductDetails()
                        {
                            CustomerId = pd.CustomerId,
                            ProductId = newProduct.ProductId
                        };
                        _dbContext.ProductDetails.Add(newProductDetails);
                    }
                }
                _dbContext.SaveChanges();
            }
            return Ok(newProduct);
        }
    }
}
