using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Shared.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ETechEnergie.Server.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendContactNotificationAsync(ContactRequest request)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(_emailSettings.AdminEmail));
            email.Subject = $"🔔 Nouvelle demande de devis - {request.Name}";

            List<CartItem>? cartItems = null;
            decimal cartTotal = 0;
            
            if (!string.IsNullOrEmpty(request.CartItemsJson))
            {
                try
                {
                    cartItems = JsonSerializer.Deserialize<List<CartItem>>(request.CartItemsJson);
                    if (cartItems != null && cartItems.Any())
                    {
                        cartTotal = cartItems.Sum(item => item.Total);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Erreur désérialisation panier: {ex.Message}");
                }
            }

            var cartHtml = string.Empty;
            if (cartItems != null && cartItems.Any())
            {
                var cartItemsHtml = string.Join("", cartItems.Select(item => $@"
                    <tr>
                        <td style='padding: 10px; border-bottom: 1px solid #e5e7eb;'>{item.ProductName}</td>
                        <td style='padding: 10px; border-bottom: 1px solid #e5e7eb; text-align: center;'>{item.Quantity}</td>
                        <td style='padding: 10px; border-bottom: 1px solid #e5e7eb; text-align: right;'>{item.Price:N0} FCFA</td>
                        <td style='padding: 10px; border-bottom: 1px solid #e5e7eb; text-align: right; font-weight: bold;'>{item.Total:N0} FCFA</td>
                    </tr>
                "));

                cartHtml = $@"
                    <div class='cart-section' style='margin-top: 30px; background: #f0f9ff; padding: 20px; border-radius: 8px; border-left: 4px solid #3b82f6;'>
                        <h3 style='color: #1e3a8a; margin-top: 0;'>🛒 Produits demandés</h3>
                        <table style='width: 100%; border-collapse: collapse; background: white; border-radius: 5px; overflow: hidden;'>
                            <thead>
                                <tr style='background: #3b82f6; color: white;'>
                                    <th style='padding: 12px; text-align: left;'>Produit</th>
                                    <th style='padding: 12px; text-align: center;'>Qté</th>
                                    <th style='padding: 12px; text-align: right;'>Prix Unit.</th>
                                    <th style='padding: 12px; text-align: right;'>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                {cartItemsHtml}
                            </tbody>
                            <tfoot>
                                <tr style='background: #fbbf24;'>
                                    <td colspan='3' style='padding: 15px; text-align: right; font-weight: bold; font-size: 1.1em;'>TOTAL :</td>
                                    <td style='padding: 15px; text-align: right; font-weight: bold; font-size: 1.2em;'>{cartTotal:N0} FCFA</td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                ";
            }

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%); 
                                     color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
                            .content {{ background: #f9fafb; padding: 30px; border-radius: 0 0 10px 10px; }}
                            .field {{ margin-bottom: 20px; padding: 15px; background: white; border-radius: 5px; }}
                            .label {{ font-weight: bold; color: #1e3a8a; margin-bottom: 5px; }}
                            .value {{ color: #4b5563; }}
                            .footer {{ text-align: center; padding: 20px; color: #6b7280; font-size: 14px; }}
                            .important {{ background: #fef3c7; padding: 15px; border-left: 4px solid #fbbf24; 
                                        margin: 20px 0; border-radius: 5px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>📬 Nouvelle Demande de Devis</h1>
                                <p>e-Tech Energie+</p>
                            </div>
                            <div class='content'>
                                <div class='important'>
                                    <strong>⏰ Reçu le:</strong> {request.SubmittedAt:dd/MM/yyyy à HH:mm}
                                </div>
                                
                                <div class='field'>
                                    <div class='label'>👤 Nom complet</div>
                                    <div class='value'>{request.Name}</div>
                                </div>
                                
                                <div class='field'>
                                    <div class='label'>📧 Email</div>
                                    <div class='value'><a href='mailto:{request.Email}'>{request.Email}</a></div>
                                </div>
                                
                                <div class='field'>
                                    <div class='label'>📱 Téléphone</div>
                                    <div class='value'><a href='tel:{request.Phone}'>{request.Phone}</a></div>
                                </div>
                                
                                {(string.IsNullOrEmpty(request.Company) ? "" : $@"
                                <div class='field'>
                                    <div class='label'>🏢 Entreprise</div>
                                    <div class='value'>{request.Company}</div>
                                </div>
                                ")}
                                
                                <div class='field'>
                                    <div class='label'>💬 Message</div>
                                    <div class='value'>{request.Message.Replace("\n", "<br>")}</div>
                                </div>

                                {cartHtml}
                            </div>
                            <div class='footer'>
                                <p>Cet email a été envoyé automatiquement depuis le site web e-Tech Energie+</p>
                                <p><strong>⚡ Répondre rapidement pour convertir ce prospect en client !</strong></p>
                            </div>
                        </div>
                    </body>
                    </html>
                "
            };

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation($"Email de notification envoyé pour {request.Name} avec {cartItems?.Count ?? 0} produits");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email de notification");
            throw;
        }
    }

    public async Task SendConfirmationToClientAsync(ContactRequest request)
    {
        try
        {
            List<CartItem>? cartItems = null;
            decimal cartTotal = 0;
            
            if (!string.IsNullOrEmpty(request.CartItemsJson))
            {
                try
                {
                    cartItems = JsonSerializer.Deserialize<List<CartItem>>(request.CartItemsJson);
                    if (cartItems != null && cartItems.Any())
                    {
                        cartTotal = cartItems.Sum(item => item.Total);
                    }
                }
                catch { }
            }

            var cartHtml = string.Empty;
            if (cartItems != null && cartItems.Any())
            {
                var cartItemsHtml = string.Join("", cartItems.Select(item => $@"
                    <div style='display: flex; justify-content: space-between; padding: 10px; border-bottom: 1px solid #e5e7eb;'>
                        <div>
                            <strong>{item.ProductName}</strong><br>
                            <span style='color: #6b7280;'>Quantité: {item.Quantity}</span>
                        </div>
                        <div style='text-align: right; font-weight: bold;'>
                            {item.Total:N0} FCFA
                        </div>
                    </div>
                "));

                cartHtml = $@"
                    <div style='background: #f0f9ff; padding: 20px; border-radius: 8px; margin: 20px 0;'>
                        <h3 style='color: #1e3a8a; margin-top: 0;'>🛒 Votre demande de devis pour :</h3>
                        <div style='background: white; border-radius: 5px; overflow: hidden;'>
                            {cartItemsHtml}
                            <div style='background: #fbbf24; padding: 15px; display: flex; justify-content: space-between;'>
                                <strong style='font-size: 1.1em;'>TOTAL ESTIMÉ :</strong>
                                <strong style='font-size: 1.2em;'>{cartTotal:N0} FCFA</strong>
                            </div>
                        </div>
                        <p style='color: #6b7280; font-size: 0.9em; margin-top: 10px;'>
                            💡 Ce montant est indicatif. Un devis détaillé vous sera envoyé sous 24-48h.
                        </p>
                    </div>
                ";
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = "✅ Confirmation de réception - e-Tech Energie+";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <style>
                            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                            .header {{ background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%); 
                                     color: white; padding: 40px; text-align: center; border-radius: 10px 10px 0 0; }}
                            .logo {{ font-size: 48px; margin-bottom: 10px; }}
                            .content {{ background: #f9fafb; padding: 30px; border-radius: 0 0 10px 10px; }}
                            .greeting {{ font-size: 20px; color: #1e3a8a; margin-bottom: 20px; }}
                            .info-box {{ background: #dbeafe; padding: 15px; border-radius: 8px; margin: 20px 0; }}
                            .contact-info {{ background: white; padding: 20px; border-radius: 8px; margin: 20px 0; }}
                            .footer {{ text-align: center; padding: 20px; color: #6b7280; font-size: 14px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <div class='logo'>⚡</div>
                                <h1>e-Tech Energie+</h1>
                                <p>L'expertise technique au service de Thiès</p>
                            </div>
                            <div class='content'>
                                <div class='greeting'>
                                    Bonjour {request.Name} ! 👋
                                </div>
                                
                                <p>Merci d'avoir contacté <strong>e-Tech Energie+</strong>.</p>
                                
                                <div class='info-box'>
                                    <strong>✅ Votre demande a bien été reçue !</strong><br>
                                    Notre équipe commerciale l'examine actuellement et vous contactera dans les plus brefs délais.
                                </div>

                                {cartHtml}
                                
                                <div style='background: white; padding: 20px; border-radius: 8px; margin: 20px 0; border-left: 4px solid #3b82f6;'>
                                    <h3 style='color: #1e3a8a; margin-top: 0;'>📋 Votre message</h3>
                                    <p style='color: #4b5563;'>{request.Message}</p>
                                </div>
                                
                                <div class='contact-info'>
                                    <h3 style='color: #1e3a8a;'>📞 Besoin de nous joindre rapidement ?</h3>
                                    <p><strong>📧 Email:</strong> contact@etechenergie.sn</p>
                                    <p><strong>📱 Téléphone:</strong> +221 77 123 45 67</p>
                                    <p><strong>⏰ Horaires:</strong> Lun-Ven: 8h-18h | Sam: 9h-13h</p>
                                </div>
                                
                                <p style='text-align: center;'>
                                    <strong>⚡ Délai de réponse habituel: 24-48 heures</strong>
                                </p>
                            </div>
                            <div class='footer'>
                                <p>© {DateTime.Now.Year} e-Tech Energie+ | Thiès, Sénégal</p>
                            </div>
                        </div>
                    </body>
                    </html>
                "
            };

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation($"Email de confirmation envoyé à {request.Email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'envoi de l'email de confirmation");
        }
    }
}