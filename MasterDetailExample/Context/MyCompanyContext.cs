using MasterDetailExample.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailExample.Context
{
    public class MyCompanyContext : DbContext
    {
        public MyCompanyContext(DbContextOptions<MyCompanyContext> options)
            : base(options)
        {
        }
 
        public DbSet<Customer> Customers { get; set; }
 
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
