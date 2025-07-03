using DapperCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperCrudApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductDetails> ProductDetails { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table names
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductDetails>().ToTable("ProductDetails");

            // Configure primary keys
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<ProductDetails>().HasKey(pd => pd.Id);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductDetails)
                .WithOne(pd => pd.Product)
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductDetails>()
            //    .HasOne(pd => pd.Product)
            //    .WithMany(p => p.ProductDetails)
            //    .HasForeignKey(pd => pd.ProductId)
            //    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }   
}
