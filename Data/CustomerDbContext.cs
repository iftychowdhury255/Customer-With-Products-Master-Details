using Microsoft.EntityFrameworkCore;
using CustomerCoreApi.Models;

namespace CustomerCoreApi.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Customer> Customers { get; set; }
    }
}
