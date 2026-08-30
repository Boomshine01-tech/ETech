using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISupabaseStorageService _storageService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(AppDbContext context, ISupabaseStorageService storageService, ILogger<CategoriesController> logger)
    {
        _context = context;
        _storageService = storageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    [HttpGet("with-products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesWithProducts()
    {
        return await _context.Categories
            .Where(c => c.Products.Any())
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return NotFound();

        return category;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        category.Name = category.Name.Trim();

        if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower()))
        {
            return BadRequest(new { error = $"Une catégorie nommée '{category.Name}' existe déjà" });
        }

        category.Id = 0;
        category.Description ??= string.Empty;
        category.ImageUrl ??= string.Empty;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a créé la catégorie '{Name}'", User.Identity?.Name, category.Name);

        return Ok(category);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Categories.FindAsync(id);
        if (existing == null)
        {
            return NotFound(new { error = "Catégorie introuvable" });
        }

        var newName = category.Name.Trim();

        if (await _context.Categories.AnyAsync(c => c.Id != id && c.Name.ToLower() == newName.ToLower()))
        {
            return BadRequest(new { error = $"Une catégorie nommée '{newName}' existe déjà" });
        }

        existing.Name = newName;
        existing.Description = category.Description ?? string.Empty;
        existing.ImageUrl = category.ImageUrl ?? string.Empty;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a modifié la catégorie {Id} ('{Name}')", User.Identity?.Name, id, existing.Name);

        return Ok(existing);
    }

    /// <summary>
    /// Upload de l'image d'une catégorie. Optionnelle : cet endpoint n'est appelé que si
    /// l'admin choisit d'en ajouter/remplacer une.
    /// </summary>
    [HttpPost("upload-image")]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Aucun fichier fourni" });
            }

            if (file.Length > 5_000_000)
            {
                return BadRequest(new { error = $"Le fichier est trop volumineux ({file.Length / 1_000_000.0:F2}MB). Maximum: 5MB" });
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new { error = $"Extension non autorisée. Extensions acceptées: {string.Join(", ", allowedExtensions)}" });
            }

            var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
            {
                return BadRequest(new { error = "Type de fichier non autorisé" });
            }

            var imageUrl = await _storageService.UploadImageAsync(file, "categories");

            _logger.LogInformation(
                "Image de catégorie uploadée sur Supabase Storage par {Username}: {Url} ({Size}KB)",
                User.Identity?.Name, imageUrl, file.Length / 1024);

            return Ok(imageUrl);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Configuration Supabase Storage manquante ou invalide");
            return StatusCode(500, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'upload d'image de catégorie");
            return StatusCode(500, new { error = "Erreur lors de l'upload de l'image" });
        }
    }
}
