using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess
{
    public class MitoCodeStoreDbContext : DbContext
    {
        public MitoCodeStoreDbContext(DbContextOptions<MitoCodeStoreDbContext> options)
        : base(options)
        {
            
        }

        public MitoCodeStoreDbContext()
        {
            
        }
        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}