using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

/// <summary>
/// Gère la bande d'annonce affichée sur toutes les pages du site public.
/// Il n'y a qu'une seule annonce en base (Id = 1) : elle est créée à la volée
/// lors du premier enregistrement par l'admin.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<AnnouncementController> _logger;

    public AnnouncementController(AppDbContext context, ILogger<AnnouncementController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Renvoie l'annonce courante. Utilisé à la fois par le site public (qui doit vérifier IsActive)
    /// et par la page d'administration (pour pré-remplir le formulaire).
    /// Renvoie un objet "vide" et inactif si aucune annonce n'a encore été créée, plutôt qu'un 404,
    /// pour simplifier l'affichage côté client.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<Announcement>> GetAnnouncement()
    {
        var announcement = await _context.Announcements.FirstOrDefaultAsync();

        if (announcement == null)
        {
            return Ok(new Announcement { Id = 0, IsActive = false });
        }

        return Ok(announcement);
    }

    /// <summary>
    /// Crée ou met à jour l'annonce unique du site.
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Announcement>> UpsertAnnouncement([FromBody] Announcement announcement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Announcements.FirstOrDefaultAsync();

        if (existing == null)
        {
            existing = new Announcement
            {
                Message = announcement.Message,
                LinkUrl = string.IsNullOrWhiteSpace(announcement.LinkUrl) ? null : announcement.LinkUrl,
                LinkText = string.IsNullOrWhiteSpace(announcement.LinkText) ? null : announcement.LinkText,
                IsActive = announcement.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Announcements.Add(existing);
        }
        else
        {
            existing.Message = announcement.Message;
            existing.LinkUrl = string.IsNullOrWhiteSpace(announcement.LinkUrl) ? null : announcement.LinkUrl;
            existing.LinkText = string.IsNullOrWhiteSpace(announcement.LinkText) ? null : announcement.LinkText;
            existing.IsActive = announcement.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Admin {Username} a mis à jour la bande d'annonce (active: {IsActive})",
            User.Identity?.Name, existing.IsActive);

        return Ok(existing);
    }
}
