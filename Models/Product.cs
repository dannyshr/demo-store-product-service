// demo-store-product-service/Models/Product.cs
using System.ComponentModel.DataAnnotations; // For [Key], [Required]
using System.ComponentModel.DataAnnotations.Schema; // For [Column] (optional for simple cases)

namespace demo_store_product_service.Models; 

public class Product
{
    [Key] // This attribute makes Id the primary key
    public int Id { get; set; }

    public int CategoryId { get; set; } // Foreign key to Category

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; } // Navigation property to Category

    [Required] // This attribute makes the Name field non-nullable in the database
    [MaxLength(100)] // Sets a maximum length for the string column
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; } // Nullable string

    [Required]
    [Column(TypeName = "decimal(18, 2)")] // Specifies the exact data type in SQL Server
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; } // URL to product image
}