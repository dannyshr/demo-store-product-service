// demo-store-product-service/Data/AppDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; // This is crucial for IDesignTimeDbContextFactory
using Microsoft.Extensions.Configuration; // To read appsettings.json configuration
using System.IO; // For Directory.GetCurrentDirectory()

namespace demo_store_product_service.Data; // Ensure this namespace matches your AppDbContext's namespace

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> // Specify AppDbContext here
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Build configuration to read appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Tells it to look for appsettings.json in the project's root
            .AddJsonFile("appsettings.json") // Loads your appsettings.json
            .Build();

        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure DbContextOptions for SQL Server using the connection string
        var builder = new DbContextOptionsBuilder<AppDbContext>(); // Specify AppDbContext here
        builder.UseSqlServer(connectionString);

        // Return a new instance of your AppDbContext
        return new AppDbContext(builder.Options); // Return an AppDbContext instance
    }
}