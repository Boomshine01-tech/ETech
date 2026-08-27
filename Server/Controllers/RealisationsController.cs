using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RealisationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<RealisationsController> _logger;
    private readonly IWebHostEnvironment _environment;

    public RealisationsController(AppDbContext context, ILogger<RealisationsController> logger, IWebHostEnvironment environment)
    {
        _context = context;
        _logger = logger;
        _environment = environment;
    }

    // ═══════════════════════════ LECTURE (public) ═══════════════════════════

    /// <summary>Sections actives avec leurs images, pour la page publique "Réalisations".</summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RealisationSection>>> GetSections()
    {
        var sections = await _context.RealisationSections
            .Where(s => s.IsActive)
            .Include(s => s.Images.OrderBy(i => i.DisplayOrder))
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        return Ok(sections);
    }

    /// <summary>Toutes les sections (actives et inactives), pour la page d'administration.</summary>
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<RealisationSection>>> GetSectionsForAdmin()
    {
        var sections = await _context.RealisationSections
            .Include(s => s.Images.OrderBy(i => i.DisplayOrder))
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        return Ok(sections);
    }

    // ═══════════════════════════ SECTIONS (admin) ═══════════════════════════

    [HttpPost("sections")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RealisationSection>> CreateSection([FromBody] RealisationSection section)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        section.Slug = section.Slug.Trim().ToLowerInvariant();

        if (await _context.RealisationSections.AnyAsync(s => s.Slug == section.Slug))
        {
            return BadRequest(new { error = $"L'identifiant '{section.Slug}' est déjà utilisé par une autre section" });
        }

        if (!section.ShowBadge)
        {
            section.BadgeIcon = null;
            section.BadgeLabel = null;
            section.BadgeHighlight = null;
        }

        var maxOrder = await _context.RealisationSections.AnyAsync()
            ? await _context.RealisationSections.MaxAsync(s => s.DisplayOrder)
            : -1;
        section.DisplayOrder = maxOrder + 1;
        section.Id = 0;

        _context.RealisationSections.Add(section);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a créé la section réalisation '{Slug}'", User.Identity?.Name, section.Slug);

        return Ok(section);
    }

    [HttpPut("sections/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSection(int id, [FromBody] RealisationSection section)
    {
        if (id != section.Id)
        {
            return BadRequest(new { error = "L'ID de la section ne correspond pas" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.RealisationSections.FindAsync(id);
        if (existing == null)
        {
            return NotFound(new { error = "Section introuvable" });
        }

        var newSlug = section.Slug.Trim().ToLowerInvariant();
        if (await _context.RealisationSections.AnyAsync(s => s.Slug == newSlug && s.Id != id))
        {
            return BadRequest(new { error = $"L'identifiant '{newSlug}' est déjà utilisé par une autre section" });
        }

        existing.Slug = newSlug;
        existing.NavLabel = section.NavLabel;
        existing.NavIcon = section.NavIcon;
        existing.Eyebrow = section.Eyebrow;
        existing.TitleMain = section.TitleMain;
        existing.TitleAccent = section.TitleAccent;
        existing.Body = section.Body;
        existing.ProjectLabel = section.ProjectLabel;
        existing.Theme = section.Theme;
        existing.ReverseLayout = section.ReverseLayout;
        existing.ShowBadge = section.ShowBadge;
        existing.BadgeIcon = section.ShowBadge ? section.BadgeIcon : null;
        existing.BadgeLabel = section.ShowBadge ? section.BadgeLabel : null;
        existing.BadgeHighlight = section.ShowBadge ? section.BadgeHighlight : null;
        existing.IsActive = section.IsActive;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a modifié la section réalisation {Id}", User.Identity?.Name, id);

        return NoContent();
    }

    [HttpDelete("sections/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var section = await _context.RealisationSections.FindAsync(id);
        if (section == null)
        {
            return NotFound(new { error = "Section introuvable" });
        }

        _context.RealisationSections.Remove(section);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a supprimé la section réalisation '{Slug}' (et ses images)", User.Identity?.Name, section.Slug);

        return NoContent();
    }

    /// <summary>Déplace une section vers le haut ou le bas dans l'ordre d'affichage.</summary>
    [HttpPost("sections/{id}/move")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MoveSection(int id, [FromQuery] string direction)
    {
        var sections = await _context.RealisationSections.OrderBy(s => s.DisplayOrder).ToListAsync();
        var index = sections.FindIndex(s => s.Id == id);

        if (index == -1)
        {
            return NotFound(new { error = "Section introuvable" });
        }

        var swapIndex = direction == "up" ? index - 1 : index + 1;
        if (swapIndex < 0 || swapIndex >= sections.Count)
        {
            return Ok(); // déjà en haut/bas de la liste, rien à faire
        }

        (sections[index].DisplayOrder, sections[swapIndex].DisplayOrder) =
            (sections[swapIndex].DisplayOrder, sections[index].DisplayOrder);

        await _context.SaveChangesAsync();

        return Ok();
    }

    // ═══════════════════════════ IMAGES (admin) ═══════════════════════════

    [HttpPost("images")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RealisationImage>> AddImage([FromBody] RealisationImage image)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var section = await _context.RealisationSections.FindAsync(image.SectionId);
        if (section == null)
        {
            return BadRequest(new { error = "Section introuvable" });
        }

        var maxOrder = await _context.RealisationImages.Where(i => i.SectionId == image.SectionId).AnyAsync()
            ? await _context.RealisationImages.Where(i => i.SectionId == image.SectionId).MaxAsync(i => i.DisplayOrder)
            : -1;

        image.Id = 0;
        image.DisplayOrder = maxOrder + 1;

        _context.RealisationImages.Add(image);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a ajouté une image à la section {SectionId}", User.Identity?.Name, image.SectionId);

        return Ok(image);
    }

    [HttpPut("images/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateImage(int id, [FromBody] RealisationImage image)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.RealisationImages.FindAsync(id);
        if (existing == null)
        {
            return NotFound(new { error = "Image introuvable" });
        }

        existing.Titre = image.Titre;
        existing.Lieu = image.Lieu;
        existing.Description = image.Description;
        if (!string.IsNullOrWhiteSpace(image.ImageUrl))
        {
            existing.ImageUrl = image.ImageUrl;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("images/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var image = await _context.RealisationImages.FindAsync(id);
        if (image == null)
        {
            return NotFound(new { error = "Image introuvable" });
        }

        _context.RealisationImages.Remove(image);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin {Username} a supprimé une image de réalisation ({Id})", User.Identity?.Name, id);

        return NoContent();
    }

    /// <summary>Déplace une image vers le haut ou le bas au sein de sa section.</summary>
    [HttpPost("images/{id}/move")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MoveImage(int id, [FromQuery] string direction)
    {
        var image = await _context.RealisationImages.FindAsync(id);
        if (image == null)
        {
            return NotFound(new { error = "Image introuvable" });
        }

        var images = await _context.RealisationImages
            .Where(i => i.SectionId == image.SectionId)
            .OrderBy(i => i.DisplayOrder)
            .ToListAsync();

        var index = images.FindIndex(i => i.Id == id);
        var swapIndex = direction == "up" ? index - 1 : index + 1;

        if (swapIndex < 0 || swapIndex >= images.Count)
        {
            return Ok();
        }

        (images[index].DisplayOrder, images[swapIndex].DisplayOrder) =
            (images[swapIndex].DisplayOrder, images[index].DisplayOrder);

        await _context.SaveChangesAsync();

        return Ok();
    }

    // ═══════════════════════════ UPLOAD ═══════════════════════════

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

            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "images", "realisations");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var imageUrl = $"{baseUrl}/images/realisations/{uniqueFileName}";

            _logger.LogInformation(
                "Image de réalisation uploadée par {Username}: {FileName} ({Size}KB)",
                User.Identity?.Name, uniqueFileName, file.Length / 1024);

            return Ok(imageUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'upload d'image de réalisation");
            return StatusCode(500, new { error = "Erreur lors de l'upload de l'image" });
        }
    }
}
