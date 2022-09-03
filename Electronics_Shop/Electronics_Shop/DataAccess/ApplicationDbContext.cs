using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Electronics_Shop.Models;

namespace Electronics_Shop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<Electronics_Shop.Models.OrderHeader> OrderHeader { get; set; }
    }
}
