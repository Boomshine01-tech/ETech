using Microsoft.AspNetCore.Mvc;
using ETechEnergie.Server.Services;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IEmailService emailService, ILogger<ContactController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint pour les messages de contact simple (sans panier)
        /// </summary>
        [HttpPost("contact")]
        public async Task<IActionResult> SendContact([FromBody] ContactRequest request)
        {
            if (request == null)
            {
                return BadRequest("Les données de contact sont invalides");
            }

            try
            {
                _logger.LogInformation("Réception d'une demande de contact de {Email}", request.Email);

                // Envoi des emails de contact (sans panier)
                await _emailService.SendContactNotificationAsync(request);
                await _emailService.SendContactConfirmationAsync(request);

                _logger.LogInformation("Emails de contact envoyés avec succès pour {Email}", request.Email);

                return Ok(new { success = true, message = "Message envoyé avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'envoi du message de contact pour {Email}", request.Email);
                return StatusCode(500, new { success = false, message = "Erreur lors de l'envoi du message" });
            }
        }

        /// <summary>
        /// Endpoint pour les commandes (avec panier)
        /// </summary>
        [HttpPost("order")]
        public async Task<IActionResult> SendOrder([FromBody] OrderRequest request)
        {
            if (request == null)
            {
                return BadRequest("Les données de commande sont invalides");
            }

            if (string.IsNullOrEmpty(request.CartItemsJson))
            {
                return BadRequest("Le panier est vide");
            }

            try
            {
                _logger.LogInformation("Réception d'une commande de {Email}", request.Email);

                // Envoi des emails de commande (avec panier)
                await _emailService.SendOrderNotificationAsync(request);
                await _emailService.SendOrderConfirmationAsync(request);

                _logger.LogInformation("Emails de commande envoyés avec succès pour {Email}", request.Email);

                return Ok(new { success = true, message = "Commande envoyée avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'envoi de la commande pour {Email}", request.Email);
                return StatusCode(500, new { success = false, message = "Erreur lors de l'envoi de la commande" });
            }
        }
    }
}
