using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

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

        return Ok(existing);
    }
}
