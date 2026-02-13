using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        IWebHostEnvironment environment, 
        AppDbContext context,
        ILogger<ProductsController> logger)
    {
        _context = context;
        _environment = environment;
        _logger = logger;
    }

    /// <summary>
    /// GET /api/products - Accessible à tous
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsAvailable)
            .ToListAsync();
    }

    /// <summary>
    /// GET /api/products/{id} - Accessible à tous
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        return product;
    }

    /// <summary>
    /// GET /api/products/category/{categoryId} - Accessible à tous
    /// </summary>
    [HttpGet("category/{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && p.IsAvailable)
            .ToListAsync();
    }

    /// <summary>
    /// POST /api/products - RÉSERVÉ AUX ADMINS
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _logger.LogInformation("Admin {Username} crée un produit: {ProductName}", 
            User.Identity?.Name, product.Name);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    /// <summary>
    /// PUT /api/products/{id} - RÉSERVÉ AUX ADMINS
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _logger.LogInformation("Admin {Username} modifie le produit ID {ProductId}", 
            User.Identity?.Name, id);

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// POST /api/products/upload-image - RÉSERVÉ AUX ADMINS
    /// </summary>
    [HttpPost("upload-image")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Aucun fichier fourni");

        // Validation du type de fichier
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp", "image/jpg" };
        if (!allowedTypes.Contains(file.ContentType.ToLower()))
        {
            return BadRequest("Format de fichier non supporté. Utilisez JPG, PNG ou WebP.");
        }

        // Validation de la taille (max 5MB)
        if (file.Length > 5_000_000)
        {
            return BadRequest("Le fichier est trop volumineux. Taille maximale: 5MB");
        }

        try
        {
            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "images", "products");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var imageUrl = $"{baseUrl}/images/products/{uniqueFileName}";

            _logger.LogInformation("Admin {Username} a uploadé une image: {ImageUrl}", 
                User.Identity?.Name, imageUrl);
        
            return Ok(imageUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'upload d'image");
            return StatusCode(500, $"Erreur: {ex.Message}");
        }
    }

    /// <summary>
    /// DELETE /api/products/{id} - RÉSERVÉ AUX ADMINS
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        _logger.LogWarning("Admin {Username} supprime le produit ID {ProductId}: {ProductName}", 
            User.Identity?.Name, id, product.Name);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}