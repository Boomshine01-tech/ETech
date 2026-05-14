using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETechEnergie.Server.Data;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

private static DateTime ToUtc(DateTime dt) =>
    dt.Kind == DateTimeKind.Utc ? dt : DateTime.SpecifyKind(dt, DateTimeKind.Utc);

[ApiController]
[Route("api/[controller]")]
public class FormationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<FormationsController> _logger;

    public FormationsController(AppDbContext context, ILogger<FormationsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Formation>>> GetFormations()
    {
        return await _context.Formations
            .OrderByDescending(f => f.DateDebut)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Formation>> GetFormation(int id)
    {
        var formation = await _context.Formations.FindAsync(id);

        if (formation == null)
        {
            return NotFound();
        }

        return formation;
    }

    [HttpGet("statut/{statut}")]
    public async Task<ActionResult<IEnumerable<Formation>>> GetFormationsByStatut(string statut)
    {
        return await _context.Formations
            .Where(f => f.Statut == statut)
            .OrderBy(f => f.DateDebut)
            .ToListAsync();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Formation>> CreateFormation(Formation formation)
    {
        formation.CreatedAt   = DateTime.UtcNow;
        formation.UpdatedAt   = DateTime.UtcNow;
        formation.DateDebut   = ToUtc(formation.DateDebut);
        formation.DateFin     = ToUtc(formation.DateFin);
        formation.PlacesRestantes = formation.CapaciteMax;
        
        _context.Formations.Add(formation);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin a créé une formation: {Titre}", formation.Titre);

        return CreatedAtAction(nameof(GetFormation), new { id = formation.Id }, formation);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateFormation(int id, Formation formation)
    {
        if (id != formation.Id)
        {
            return BadRequest();
        }

        var existing = await _context.Formations.FindAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Titre = formation.Titre;
        existing.Description = formation.Description;
        existing.Prix = formation.Prix;
        existing.DateDebut = ToUtc(formation.DateDebut);
        existing.DateFin   = ToUtc(formation.DateFin);
        existing.UpdatedAt = DateTime.UtcNow;
        existing.Horaires = formation.Horaires;
        existing.Statut = formation.Statut;
        existing.Lieu = formation.Lieu;
        existing.Partenaires = formation.Partenaires;
        existing.InscriptionOuverte = formation.InscriptionOuverte;
        existing.CapaciteMax = formation.CapaciteMax;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin a modifié la formation ID {Id}: {Titre}", id, formation.Titre);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteFormation(int id)
    {
        var formation = await _context.Formations.FindAsync(id);
        if (formation == null)
        {
            return NotFound();
        }

        _context.Formations.Remove(formation);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Admin a supprimé la formation ID {Id}: {Titre}", id, formation.Titre);

        return NoContent();
    }

    [HttpPost("{id}/inscrire")]
    public async Task<ActionResult<Inscription>> Inscrire(int id, Inscription inscription)
    {
        var formation = await _context.Formations.FindAsync(id);
        if (formation == null)
        {
            return NotFound("Formation non trouvée");
        }

        if (!formation.InscriptionOuverte)
        {
            return BadRequest("Les inscriptions sont fermées pour cette formation");
        }

        if (formation.PlacesRestantes <= 0)
        {
            return BadRequest("Plus de places disponibles");
        }

        inscription.FormationId = id;
        inscription.DateInscription = DateTime.UtcNow;
        inscription.Statut = "Confirmée";

        _context.Inscriptions.Add(inscription);
        formation.PlacesRestantes--;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Nouvelle inscription pour la formation {FormationId} par {Email}", id, inscription.Email);

        return CreatedAtAction(nameof(GetInscription), new { id = inscription.Id }, inscription);
    }

    [HttpGet("inscriptions/{id}")]
    public async Task<ActionResult<Inscription>> GetInscription(int id)
    {
        var inscription = await _context.Inscriptions.FindAsync(id);

        if (inscription == null)
        {
            return NotFound();
        }

        return inscription;
    }

    [HttpGet("{id}/inscriptions")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Inscription>>> GetInscriptionsByFormation(int id)
    {
        return await _context.Inscriptions
            .Where(i => i.FormationId == id)
            .OrderByDescending(i => i.DateInscription)
            .ToListAsync();
    }
}
