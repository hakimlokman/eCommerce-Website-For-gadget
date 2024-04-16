using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Models
{
    public class eCommerceDbContext:DbContext
    {
        public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options) : base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Customer> Customers   { get; set; }
        public DbSet<SalesOrder> SalesOrders  { get; set; }
        public DbSet<Products> Products  { get; set; }
        public DbSet<SalesItem> SalesItems  { get; set; }


    }
}
