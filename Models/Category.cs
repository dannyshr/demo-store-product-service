// demo-store-product-service/Models/Category.cs
using System.ComponentModel.DataAnnotations; // For [Key], [Required]

namespace demo_store_product_service.Models;

// This class represents a "Category" in your database.
// Entity Framework Core will map this to a table named 'Categories' by default.
public class Category
{
    // [Key] attribute indicates that 'Id' is the primary key for this table.
    // EF Core will automatically configure it as an identity column (auto-incrementing).
    [Key]
    public int Id { get; set; }

    // 'Name' property for the category name.
    // [Required] makes this field non-nullable in the database.
    // [MaxLength(100)] sets a maximum length for the string in the database.
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty; // Initialize to empty string to prevent null reference warnings

    public ICollection<Product> Products { get; set; } = new List<Product>(); // Navigation property to related
}
