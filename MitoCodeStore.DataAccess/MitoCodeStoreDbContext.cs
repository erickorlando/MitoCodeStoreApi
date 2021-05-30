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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Sale>()
                .Property(p => p.TotalAmount)
                .HasPrecision(8, 2);

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.Quantity)
                .HasPrecision(8, 2);

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.Total)
                .HasPrecision(8, 2);
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}