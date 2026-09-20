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

    // ═══════════════════════════ LECTURE ═══════════════════════════

    /// <summary>Partenaires actifs, pour la section "Nos partenaires" de la page d'accueil.</summary>
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

    /// <summary>Tous les partenaires (actifs et inactifs), pour la page d'administration.</summary>
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Partner>>> GetPartnersForAdmin()
    {
        var partners = await _context.Partners
            .OrderBy(p => p.DisplayOrder)
            .ToListAsync();

        return Ok(partners);
    }

    // ═══════════════════════════ ÉCRITURE (admin) ═══════════════════════════

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

        _logger.LogInformation("Admin {Username} a créé le partenaire '{Name}'", User.Identity?.Name, partner.Name);

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

        // Le logo n'est remplacé que si une nouvelle URL a bien été fournie
        // (évite d'écraser le logo existant si le formulaire est soumis sans ré-upload).
        if (!string.IsNullOrWhiteSpace(partner.LogoUrl))
        {
            existing.LogoUrl = partner.LogoUrl;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a modifié le partenaire {Id}", User.Identity?.Name, id);

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

        // Suppression best-effort du logo sur Supabase Storage (n'échoue jamais la requête).
        await _storageService.DeleteImageAsync(logoUrl);

        _logger.LogInformation("Admin {Username} a supprimé le partenaire '{Name}'", User.Identity?.Name, name);

        return NoContent();
    }

    /// <summary>Déplace un partenaire vers le haut ou le bas dans l'ordre d'affichage.</summary>
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
            return Ok(); // déjà en haut/bas de la liste, rien à faire
        }

        (partners[index].DisplayOrder, partners[swapIndex].DisplayOrder) =
            (partners[swapIndex].DisplayOrder, partners[index].DisplayOrder);

        await _context.SaveChangesAsync();

        return Ok();
    }

    // ═══════════════════════════ UPLOAD DU LOGO ═══════════════════════════

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

            _logger.LogInformation(
                "Logo de partenaire uploadé sur Supabase Storage par {Username}: {Url} ({Size}KB)",
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
            _logger.LogError(ex, "Erreur lors de l'upload du logo partenaire");
            return StatusCode(500, new { error = "Erreur lors de l'upload de l'image" });
        }
    }
}
