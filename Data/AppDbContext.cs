// demo-store-product-service/Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using demo_store_product_service.Models;

namespace demo_store_product_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // This DbSet represents the Products table in your database
    public DbSet<Product> Products { get; set; }

    // This DbSet represents the Products table in your database
    public DbSet<Category> Categories { get; set; }

    // You can override OnModelCreating to configure your models further
    // For example, to define relationships, composite keys, or specific table names
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Example: Ensure Product name is unique (optional, but good for some scenarios)
        // modelBuilder.Entity<Product>()
        //    .HasIndex(p => p.Name)
        //    .IsUnique();
    }
}