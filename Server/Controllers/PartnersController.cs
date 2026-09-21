using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartnersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<PartnersController> _logger;
    private readonly ISupabaseStorageService _storageService;

    public PartnersController(AppDbContext context, ILogger<PartnersController> logger, ISupabaseStorageService storageService)
    {
        _context = context;
        _logger = logger;
        _storageService = storageService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Partner>>> GetPartners()
    {
        var partners = await _context.Partners
            .Where(p => p.IsActive)
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();

        return Ok(partners);
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Partner>>> GetPartnersForAdmin()
    {
        var partners = await _context.Partners
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();

        return Ok(partners);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Partner>> CreatePartner([FromBody] Partner partner)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var maxOrder = await _context.Partners.AnyAsync()
            ? await _context.Partners.MaxAsync(p => p.DisplayOrder)
            : -1;

        partner.Id = 0;
        partner.DisplayOrder = maxOrder + 1;
        partner.CreatedAt = DateTime.UtcNow;

        _context.Partners.Add(partner);
        await _context.SaveChangesAsync();

        return Ok(partner);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePartner(int id, [FromBody] Partner partner)
    {
        if (id != partner.Id)
        {
            return BadRequest(new { error = "L'ID du partenaire ne correspond pas" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Partners.FindAsync(id);
        if (existing == null)
        {
            return NotFound(new { error = "Partenaire introuvable" });
        }

        existing.Name = partner.Name;
        existing.Description = partner.Description;
        existing.WebsiteUrl = partner.WebsiteUrl;
        existing.IsActive = partner.IsActive;

        if (!string.IsNullOrWhiteSpace(partner.LogoUrl))
        {
            existing.LogoUrl = partner.LogoUrl;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePartner(int id)
    {
        var partner = await _context.Partners.FindAsync(id);
        if (partner == null)
        {
            return NotFound(new { error = "Partenaire introuvable" });
        }

        var logoUrl = partner.LogoUrl;
        var name = partner.Name;

        _context.Partners.Remove(partner);
        await _context.SaveChangesAsync();

        await _storageService.DeleteImageAsync(logoUrl);

        return NoContent();
    }

    [HttpPost("{id}/move")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MovePartner(int id, [FromQuery] string direction)
    {
        var partners = await _context.Partners.OrderBy(p => p.DisplayOrder).ToListAsync();
        var index = partners.FindIndex(p => p.Id == id);

        if (index == -1)
        {
            return NotFound(new { error = "Partenaire introuvable" });
        }

        var swapIndex = direction == "up" ? index - 1 : index + 1;
        if (swapIndex < 0 || swapIndex >= partners.Count)
        {
            return Ok(); 
        }

        (partners[index].DisplayOrder, partners[swapIndex].DisplayOrder) =
            (partners[swapIndex].DisplayOrder, partners[index].DisplayOrder);

        await _context.SaveChangesAsync();

        return Ok();
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

            var imageUrl = await _storageService.UploadImageAsync(file, "partenaires");

            return Ok(imageUrl);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Configuration Supabase Storage manquante ou invalide");
            return StatusCode(500, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'upload du logo partenaire");
            return StatusCode(500, new { error = "Erreur lors de l'upload de l'image" });
        }
    }
}
