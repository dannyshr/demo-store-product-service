// demo-store-product-service/DTOs/CategoryDto.cs
using System.Collections.Generic;

namespace demo_store_product_service.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}