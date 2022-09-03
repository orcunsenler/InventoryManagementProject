using InventoryManagementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}
