// demo-store-product-service/DTOs/ProductDto.cs
namespace demo_store_product_service.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
}