using Microsoft.AspNetCore.Mvc;
using ETechEnergie.Server.Data;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;
    private readonly ILogger<ContactController> _logger;
    public ContactController(AppDbContext context, IEmailService emailService, ILogger<ContactController> logger)
    {
        _context = context;
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ContactRequest>> SubmitContact(ContactRequest request)
    {
        try
        {
            // Sauvegarder dans la base de données
            request.SubmittedAt = DateTime.UtcNow;
            _context.ContactRequests.Add(request);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Nouvelle demande de contact de {request.Name}");

            // Envoyer les emails en arrière-plan (ne pas bloquer la réponse)
            _ = Task.Run(async () =>
            {
                try
                {
                    // Email à l'admin
                    await _emailService.SendContactNotificationAsync(request);
                    
                    // Email de confirmation au client
                    await _emailService.SendConfirmationToClientAsync(request);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'envoi des emails");
                }
            });

            return CreatedAtAction(nameof(SubmitContact), new { id = request.Id }, request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du traitement de la demande de contact");
            return StatusCode(500, new { error = "Une erreur s'est produite" });
        }
    }
}