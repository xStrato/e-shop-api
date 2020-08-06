using EShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options):base(options) {}

        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<User> Users {get; set;}

    }
}