// demo-store-product-service/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;        // For [ApiController], ControllerBase, ActionResult, etc.
using Microsoft.EntityFrameworkCore;    // For ToListAsync, FindAsync, SaveChangesAsync etc.
using demo_store_product_service.Data;  // Your DbContext's namespace
using demo_store_product_service.Models; // Your Product entity's namespace
using System.Collections.Generic;      // For IEnumerable
using System.Threading.Tasks;          // For Task (asynchronous operations)

namespace demo_store_product_service.Controllers; // File-scoped namespace, like a Java package declaration at the top

[ApiController] // <-- Equivalent to Spring Boot's @RestController
[Route("api/[controller]")] // <-- Equivalent to Spring Boot's @RequestMapping("/api/products")
public class ProductsController : ControllerBase // <-- Like extending a base controller, provides HTTP-specific methods
{
    private readonly AppDbContext _context; // <-- Like your JPA Repository or EntityManager

    // Constructor Injection: Equivalent to Spring Boot's @Autowired on a constructor
    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Products
    [HttpGet] // <-- Equivalent to Spring Boot's @GetMapping
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        // Check if the DbSet is null (shouldn't happen if configured correctly)
        if (_context.Products == null)
        {
            // Return a 404 Not Found if the resource is not available
            return NotFound("Products DbSet is null.");
        }

        // _context.Products is like your JpaRepository.findAll()
        // ToListAsync() fetches all products from the database asynchronously
        return await _context.Products.ToListAsync();
    }

    // GET: api/Products/5
    [HttpGet("{id}")] // <-- @GetMapping("/{id}")
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        // FindAsync() attempts to find a model by primary key asynchronously
        // Similar to JpaRepository.findById(id).orElse(null)
        var modelItem = await _context.Products.FindAsync(id);

        if (modelItem == null)
        {
            return NotFound(); // <-- Returns HTTP 404 Not Found (like ResponseEntity.notFound().build())
        }

        return modelItem; // <-- Returns HTTP 200 OK with the modelItem (like ResponseEntity.ok(product))
    }

    // POST: api/Products
    [HttpPost] // <-- Equivalent to Spring Boot's @PostMapping
    public async Task<ActionResult<Product>> PostProduct(Product product) // @RequestBody Product product
    {
        _context.Products.Add(product); // <-- Like JpaRepository.save(product) for a new entity
        await _context.SaveChangesAsync(); // <-- Essential! This actually commits changes to the database.
                                           // Analogous to Spring's @Transactional commit

        // Returns HTTP 201 Created, with the location of the new resource and the resource itself.
        // Similar to ResponseEntity.created(URI.create(...)).body(product)
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")] // <-- Equivalent to Spring Boot's @PutMapping("/{id}")
    public async Task<IActionResult> PutProduct(int id, Product product) // @PathVariable int id, @RequestBody Product product
    {
        if (id != product.Id)
        {
            return BadRequest(); // <-- Returns HTTP 400 Bad Request (like ResponseEntity.badRequest().build())
        }

        // Tells EF Core that this 'product' object is an existing entity
        // that has been modified, and needs to be updated in the database.
        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync(); // Commit the update
        }
        catch (DbUpdateConcurrencyException) // Handles optimistic concurrency conflicts
        {
            if (!ItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw; // Re-throw if it's not a "not found" issue but a genuine concurrency conflict
            }
        }

        return NoContent(); // <-- Returns HTTP 204 No Content (like ResponseEntity.noContent().build())
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")] // <-- Equivalent to Spring Boot's @DeleteMapping("/{id}")
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id); // Find the product to delete
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product); // Mark for removal
        await _context.SaveChangesAsync(); // Commit the deletion

        return NoContent();
    }

    // Private helper method to check if a model item exists (used by PUT/DELETE)
    private bool ItemExists(int id)
    {
        // Any() checks if any item matches the condition
        return _context.Products.Any(e => e.Id == id);
    }
}