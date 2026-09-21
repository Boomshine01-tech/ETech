using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISupabaseStorageService _storageService;
    private readonly ILogger<ProductsController> _logger;
    private readonly IMemoryCache _cache;

    private const string ProductCacheKeyPrefix = "product_";

    private static CancellationTokenSource _productsCacheToken = new();

    public ProductsController(
        ISupabaseStorageService storageService,
        AppDbContext context,
        ILogger<ProductsController> logger,
        IMemoryCache cache)
    {
        _context = context;
        _storageService = storageService;
        _logger = logger;
        _cache = cache;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedResult<Product>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? search = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;
        if (pageSize > 100) pageSize = 100;

        try
        {
            var cacheKey = $"products_page{page}_size{pageSize}_cat{categoryId}_search{search}";

            if (!_cache.TryGetValue(cacheKey, out PagedResult<Product>? cachedResult))
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.IsAvailable)
                    .AsQueryable();

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchLower = search.ToLower();
                    query = query.Where(p =>
                        p.Name.ToLower().Contains(searchLower) ||
                        p.Description.ToLower().Contains(searchLower)
                    );
                }

                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                if (page > totalPages && totalPages > 0)
                {
                    page = totalPages;
                }

                var products = await query
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                cachedResult = new PagedResult<Product>
                {
                    Items = products,
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                };

              
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
                    .SetPriority(CacheItemPriority.Normal)
                    .AddExpirationToken(new CancellationChangeToken(_productsCacheToken.Token));

                _cache.Set(cacheKey, cachedResult, cacheOptions);

            }
            

            return Ok(cachedResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erreur lors du chargement des produits" });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var cacheKey = $"{ProductCacheKeyPrefix}{id}";

        if (!_cache.TryGetValue(cacheKey, out Product? product))
        {
            product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { error = "Produit introuvable" });
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                .AddExpirationToken(new CancellationChangeToken(_productsCacheToken.Token));

            _cache.Set(cacheKey, product, cacheOptions);
            _logger.LogInformation("Produit {ProductId} chargé depuis DB", id);
        }
        else
        {
            _logger.LogInformation("Produit {ProductId} chargé depuis cache", id);
        }

        return Ok(product);
    }

    private bool ValidateDiscount(Product product, out string? error)
    {
        error = null;

        if (product.DiscountPrice.HasValue)
        {
            if (product.DiscountPrice.Value <= 0)
            {
                error = "Le prix promotionnel doit être supérieur à 0";
                return false;
            }

            if (product.DiscountPrice.Value >= product.Price)
            {
                error = "Le prix promotionnel doit être inférieur au prix normal";
                return false;
            }
        }

        if (product.DiscountEndDate.HasValue)
        {
            product.DiscountEndDate = ToUtc(product.DiscountEndDate.Value);

            if (!product.DiscountPrice.HasValue)
            {
                error = "Une date de fin de remise nécessite un prix promotionnel";
                return false;
            }
        }

        return true;
    }

    private static DateTime ToUtc(DateTime dt) =>
        dt.Kind == DateTimeKind.Utc ? dt : DateTime.SpecifyKind(dt, DateTimeKind.Utc);

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!ValidateDiscount(product, out var discountError))
        {
            return BadRequest(new { error = discountError });
        }

        product.CreatedAt = DateTime.UtcNow;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        ClearProductsCache();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest(new { error = "L'ID du produit ne correspond pas" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!ValidateDiscount(product, out var discountError))
        {
            return BadRequest(new { error = discountError });
        }
        
        var existing = await _context.Products.FindAsync(id);

        if (existing == null)
        {
            return NotFound(new { error = "Produit introuvable" });
        }

        existing.Name              = product.Name;
        existing.Description       = product.Description;
        existing.Price             = product.Price;
        existing.CategoryId        = product.CategoryId;
        existing.Stock             = product.Stock;
        existing.ImageUrl          = product.ImageUrl;
        existing.IsAvailable       = product.IsAvailable;
        existing.DiscountPrice     = product.DiscountPrice;
        existing.DiscountEndDate   = product.DiscountEndDate;

        try
        {
            await _context.SaveChangesAsync();

            ClearProductsCache();

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == id))
            {
                return NotFound(new { error = "Produit introuvable" });
            }
            throw;
        }

        return NoContent();
    }

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

            var imageUrl = await _storageService.UploadImageAsync(file, "products");

            return Ok(imageUrl);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erreur lors de l'upload de l'image" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(new { error = "Produit introuvable" });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        ClearProductsCache();

        return NoContent();
    }

   
    private void ClearProductsCache()
    {
        _productsCacheToken.Cancel();
        _productsCacheToken = new CancellationTokenSource();
        _logger.LogInformation("Cache des produits invalidé");
    }
}
