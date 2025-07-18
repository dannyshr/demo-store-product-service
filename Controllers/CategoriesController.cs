// demo-store-product-service/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;        // For [ApiController], ControllerBase, ActionResult, etc.
using Microsoft.EntityFrameworkCore;    // For ToListAsync, FindAsync, SaveChangesAsync etc.
using demo_store_product_service.Data;  // The DbContext's namespace
using demo_store_product_service.Models; // The Model entity's namespace
using System.Collections.Generic;      // For IEnumerable
using System.Threading.Tasks;          // For Task (asynchronous operations)

namespace demo_store_product_service.Controllers; // File-scoped namespace, like a Java package declaration at the top

// [ApiController] attribute enables API-specific behaviors like automatic model validation.
[ApiController]
// [Route] defines the base route for this controller. e.g., /api/categories
[Route("api/[controller]")] // [controller] is a placeholder that will be replaced by "Categories"
public class CategoriesController : ControllerBase // ControllerBase is the base class for API controllers without view support
{
    private readonly AppDbContext _context; // Declare a private field to hold our DbContext instance

    // Constructor for dependency injection.
    // The framework will automatically provide an instance of AppDbContext when CategoriesController is created.
    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    // This action method will respond to HTTP GET requests at /api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        // Check if the Categories DbSet is null (shouldn't happen if configured correctly)
        if (_context.Categories == null)
        {
            // Return a 404 Not Found if the resource is not available
            return NotFound("Categories DbSet is null.");
        }

        // Use Entity Framework Core to query all categories from the database.
        // .ToListAsync() executes the query asynchronously and returns a List of Category objects.
        // 'await' pauses execution until the asynchronous operation completes.
        // 'Task<...>' indicates that this method performs an asynchronous operation.
        return await _context.Categories.ToListAsync();
    }

    // GET: api/Categories/5
    [HttpGet("{id}")] // <-- @GetMapping("/{id}")
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        // FindAsync() attempts to find a model by primary key asynchronously
        // Similar to JpaRepository.findById(id).orElse(null)
        var modelItem = await _context.Categories.FindAsync(id);

        if (modelItem == null)
        {
            return NotFound(); // <-- Returns HTTP 404 Not Found (like ResponseEntity.notFound().build())
        }

        return modelItem; // <-- Returns HTTP 200 OK with the modelItem (like ResponseEntity.ok(product))
    }

    // POST: api/Categories
    [HttpPost] // <-- Equivalent to Spring Boot's @PostMapping
    public async Task<ActionResult<Category>> PostCategory(Category category) // @RequestBody Category category
    {
        _context.Categories.Add(category); // <-- Like JpaRepository.save(category) for a new entity
        await _context.SaveChangesAsync(); // <-- Essential! This actually commits changes to the database.
                                           // Analogous to Spring's @Transactional commit

        // Returns HTTP 201 Created, with the location of the new resource and the resource itself.
        // Similar to ResponseEntity.created(URI.create(...)).body(product)
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")] // <-- Equivalent to Spring Boot's @PutMapping("/{id}")
    public async Task<IActionResult> PutCategory(int id, Category category) // @PathVariable int id, @RequestBody Category category
    {
        if (id != category.Id)
        {
            return BadRequest(); // <-- Returns HTTP 400 Bad Request (like ResponseEntity.badRequest().build())
        }

        // Tells EF Core that this 'category' object is an existing entity
        // that has been modified, and needs to be updated in the database.
        _context.Entry(category).State = EntityState.Modified;

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

    // DELETE: api/Categories/5
    [HttpDelete("{id}")] // <-- Equivalent to Spring Boot's @DeleteMapping("/{id}")
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var item = await _context.Categories.FindAsync(id); // Find the item to delete
        if (item == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(item); // Mark for removal
        await _context.SaveChangesAsync(); // Commit the deletion

        return NoContent();
    }

    // Private helper method to check if a model item exists (used by PUT/DELETE)
    private bool ItemExists(int id)
    {
        // Any() checks if any item matches the condition
        return _context.Categories.Any(e => e.Id == id);
    }
}