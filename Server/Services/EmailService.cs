using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ETechEnergie.Server.Configuration;
using ETechEnergie.Shared.Models;
using System.Text.Json;
using System.Threading.Tasks;  
using System.Linq; 
using System.Collections.Generic;

using Task = System.Threading.Tasks.Task;

namespace ETechEnergie.Server.Services
{
    public interface IEmailService
    {
        // Emails pour les contacts simples (sans panier)
        Task SendContactNotificationAsync(ContactRequest request);
        Task SendContactConfirmationAsync(ContactRequest request);
        
        // Emails pour les commandes (avec panier)
        Task SendOrderNotificationAsync(OrderRequest request);
        Task SendOrderConfirmationAsync(OrderRequest request);
    }

    public class EmailService : IEmailService
    {
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings,
                            IOptions<BrevoSettings> brevoSettings,
                            ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;

            // Configuration de l'API Brevo
            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Clear();
            sib_api_v3_sdk.Client.Configuration.Default.ApiKey.Add("api-key", brevoSettings.Value.ApiKey);
            _logger.LogInformation("Brevo API Key utilisée: {ApiKey}", brevoSettings.Value.ApiKey);

            _apiInstance = new TransactionalEmailsApi();
        }

        #region Emails de Contact Simple (sans panier)

        public async Task SendContactNotificationAsync(ContactRequest request)
        {
            try
            {
                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(
                        _emailSettings.SenderName, 
                        _emailSettings.SenderEmail
                    ),
                    To = new List<SendSmtpEmailTo>
                    {
                        new SendSmtpEmailTo(_emailSettings.AdminEmail)
                    },
                    Subject = $"Nouveau message de contact - {request.Name}",
                    HtmlContent = BuildContactNotificationHtml(request)
                };

                var result = await Task.Run(() => _apiInstance.SendTransacEmail(sendSmtpEmail));
                
                _logger.LogInformation(
                    "Email de contact envoyé avec succès. MessageId: {MessageId}", 
                    result.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Erreur lors de l'envoi de l'email de contact pour {Email}", 
                    request.Email);
                throw;
            }
        }

        public async Task SendContactConfirmationAsync(ContactRequest request)
        {
            try
            {
                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(
                        _emailSettings.SenderName, 
                        _emailSettings.SenderEmail
                    ),
                    To = new List<SendSmtpEmailTo>
                    {
                        new SendSmtpEmailTo(request.Email, $"{request.Name}")
                    },
                    Subject = "Confirmation de réception de votre message - e-Tech Energie+",
                    HtmlContent = BuildContactConfirmationHtml(request)
                };

                var result = await Task.Run(() => _apiInstance.SendTransacEmail(sendSmtpEmail));
                
                _logger.LogInformation(
                    "Email de confirmation de contact envoyé à {Email}. MessageId: {MessageId}", 
                    request.Email, 
                    result.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Erreur lors de l'envoi de l'email de confirmation de contact à {Email}", 
                    request.Email);
                throw;
            }
        }

        #endregion

        #region Emails de Commande (avec panier)

        public async Task SendOrderNotificationAsync(OrderRequest request)
        {
            try
            {
                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(
                        _emailSettings.SenderName, 
                        _emailSettings.SenderEmail
                    ),
                    To = new List<SendSmtpEmailTo>
                    {
                        new SendSmtpEmailTo(_emailSettings.AdminEmail)
                    },
                    Subject = $"🛒 Nouvelle Commande - {request.Prenom} {request.Nom}",
                    HtmlContent = BuildOrderNotificationHtml(request)
                };

                var result = await Task.Run(() => _apiInstance.SendTransacEmail(sendSmtpEmail));
                
                _logger.LogInformation(
                    "Email de notification de commande envoyé avec succès. MessageId: {MessageId}", 
                    result.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Erreur lors de l'envoi de l'email de notification de commande pour {Email}", 
                    request.Email);
                throw;
            }
        }

        public async Task SendOrderConfirmationAsync(OrderRequest request)
        {
            try
            {
                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(
                        _emailSettings.SenderName, 
                        _emailSettings.SenderEmail
                    ),
                    To = new List<SendSmtpEmailTo>
                    {
                        new SendSmtpEmailTo(request.Email, $"{request.Prenom} {request.Nom}")
                    },
                    Subject = "✅ Confirmation de votre commande - e-Tech Energie+",
                    HtmlContent = BuildOrderConfirmationHtml(request)
                };

                var result = await Task.Run(() => _apiInstance.SendTransacEmail(sendSmtpEmail));
                
                _logger.LogInformation(
                    "Email de confirmation de commande envoyé à {Email}. MessageId: {MessageId}", 
                    request.Email, 
                    result.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Erreur lors de l'envoi de l'email de confirmation de commande à {Email}", 
                    request.Email);
                throw;
            }
        }

        #endregion

        #region Templates HTML pour Contact Simple

        private string BuildContactNotificationHtml(ContactRequest request)
        {
            return $@"
<!DOCTYPE html>
<html lang='fr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            background: linear-gradient(135deg, #2563eb 0%, #1d4ed8 100%);
            color: white;
            padding: 30px 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
            font-weight: 600;
        }}
        .content {{
            padding: 30px;
        }}
        .field {{
            margin-bottom: 20px;
            border-bottom: 1px solid #e5e7eb;
            padding-bottom: 15px;
        }}
        .field:last-child {{
            border-bottom: none;
        }}
        .label {{
            display: block;
            font-weight: 600;
            color: #1f2937;
            margin-bottom: 5px;
            font-size: 14px;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }}
        .value {{
            color: #4b5563;
            font-size: 16px;
        }}
        .message-box {{
            background-color: #f9fafb;
            padding: 15px;
            border-radius: 5px;
            border-left: 4px solid #2563eb;
            white-space: pre-wrap;
            word-wrap: break-word;
        }}
        .footer {{
            background-color: #f9fafb;
            padding: 20px;
            text-align: center;
            color: #6b7280;
            font-size: 12px;
        }}
        .badge {{
            display: inline-block;
            background-color: #10b981;
            color: white;
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            margin-top: 10px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>📧 Nouveau Message de Contact</h1>
            <span class='badge'>Message Simple</span>
        </div>
        <div class='content'>
            <div class='field'>
                <span class='label'>👤 Nom Complet</span>
                <span class='value'>{request.Name}</span>
            </div>
            <div class='field'>
                <span class='label'>✉️ Email</span>
                <span class='value'>{request.Email}</span>
            </div>
            <div class='field'>
                <span class='label'>📱 Téléphone</span>
                <span class='value'>{request.Phone}</span>
            </div>
            <div class='field'>
                <span class='label'>🏢 Entreprise</span>
                <span class='value'>{request.Company}</span>
            </div>
            <div class='field'>
                <span class='label'>💬 Message</span>
                <div class='message-box'>{request.Message}</div>
            </div>
            <div class='field'>
                <span class='label'>🕐 Date de Réception</span>
                <span class='value'>{DateTime.Now:dddd dd MMMM yyyy à HH:mm:ss}</span>
            </div>
        </div>
        <div class='footer'>
            <p>Cet email a été envoyé automatiquement par le système de contact e-Tech Energie+</p>
        </div>
    </div>
</body>
</html>";
        }

        private string BuildContactConfirmationHtml(ContactRequest request)
        {
            return $@"
<!DOCTYPE html>
<html lang='fr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            background: linear-gradient(135deg, #2563eb 0%, #1d4ed8 100%);
            color: white;
            padding: 40px 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 700;
        }}
        .header .emoji {{
            font-size: 48px;
            margin-bottom: 10px;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            margin-bottom: 20px;
            color: #1f2937;
        }}
        .message {{
            margin: 25px 0;
            line-height: 1.8;
        }}
        .recap {{
            background-color: #f9fafb;
            padding: 20px;
            border-radius: 8px;
            margin: 25px 0;
            border-left: 4px solid #2563eb;
        }}
        .recap-title {{
            font-weight: 700;
            color: #1f2937;
            margin-bottom: 15px;
            font-size: 16px;
        }}
        .recap-item {{
            margin: 10px 0;
        }}
        .recap-label {{
            font-weight: 600;
            color: #4b5563;
        }}
        .recap-value {{
            color: #6b7280;
            display: block;
            margin-top: 5px;
            padding-left: 10px;
            border-left: 2px solid #e5e7eb;
        }}
        .signature {{
            margin-top: 30px;
            font-style: italic;
        }}
        .team-name {{
            font-weight: 700;
            color: #2563eb;
        }}
        .footer {{
            background-color: #1f2937;
            color: #9ca3af;
            padding: 20px;
            text-align: center;
            font-size: 12px;
        }}
        .footer a {{
            color: #60a5fa;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='emoji'>⚡</div>
            <h1>e-Tech Energie+</h1>
        </div>
        <div class='content'>
            <p class='greeting'>Bonjour <strong>{request.Name}</strong>,</p>
            
            <div class='message'>
                <p>Nous avons bien reçu votre message et vous en remercions sincèrement.</p>
                <p>Notre équipe analyse actuellement votre demande et vous répondra dans les plus brefs délais, généralement sous <strong>24 à 48 heures</strong>.</p>
            </div>
            
            <div class='recap'>
                <div class='recap-title'>📋 Récapitulatif de votre demande</div>
                <div class='recap-item'>
                    <span class='recap-label'>Votre message :</span>
                    <span class='recap-value' style='white-space: pre-wrap;'>{request.Message}</span>
                </div>
                <div class='recap-item'>
                    <span class='recap-label'>Date d'envoi :</span>
                    <span class='recap-value'>{DateTime.Now:dddd dd MMMM yyyy à HH:mm}</span>
                </div>
            </div>
            
            <div class='message'>
                <p>Si vous avez des questions supplémentaires d'ici là, n'hésitez pas à nous recontacter.</p>
            </div>
            
            <div class='signature'>
                <p>Cordialement,</p>
                <p class='team-name'>L'équipe e-Tech Energie+</p>
            </div>
        </div>
        <div class='footer'>
            <p>Cet email a été envoyé automatiquement, merci de ne pas y répondre directement.</p>
            <p>Pour toute question : <a href='mailto:{_emailSettings.AdminEmail}'>{_emailSettings.AdminEmail}</a></p>
        </div>
    </div>
</body>
</html>";
        }

        #endregion

        #region Templates HTML pour Commandes (avec panier)

        private string BuildOrderNotificationHtml(OrderRequest request)
        {
            // Désérialisation du panier 
            List<CartItem>? cartItems = null; 
            decimal cartTotal = 0; 
            
            if (!string.IsNullOrEmpty(request.CartItemsJson)) 
            { 
                try 
                {
                    cartItems = JsonSerializer.Deserialize<List<CartItem>>(request.CartItemsJson); 
                    if (cartItems != null && cartItems.Any()) 
                       cartTotal = cartItems.Sum(item => item.Total);
                } 
                catch (Exception ex) 
                { 
                    _logger.LogWarning("Erreur désérialisation panier: {Message}", ex.Message);
                } 
            } 
            
            // Bloc HTML du panier 
            var cartHtml = string.Empty; 
            if (cartItems != null && cartItems.Any()) 
            { 
                var cartItemsHtml = string.Join("", cartItems.Select(item => $@" 
                    <tr> 
                        <td style='padding:10px;border-bottom:1px solid #e5e7eb;'>{item.ProductName}</td> 
                        <td style='padding:10px;text-align:center;border-bottom:1px solid #e5e7eb;'>{item.Quantity}</td> 
                        <td style='padding:10px;text-align:right;border-bottom:1px solid #e5e7eb;'>{item.Price:N0} FCFA</td> 
                        <td style='padding:10px;text-align:right;font-weight:bold;border-bottom:1px solid #e5e7eb;'>{item.Total:N0} FCFA</td> 
                    </tr> 
                ")); 
                
                cartHtml = $@"
                    <div style='margin-top:30px;background:#f0f9ff;padding:20px;border-radius:8px;border-left:4px solid #3b82f6;'> 
                        <h3 style='color:#1e3a8a;margin-top:0;'>🛒 Produits Commandés</h3>
                        <table style='width:100%;border-collapse:collapse;background:white;border-radius:5px;overflow:hidden;'> 
                            <thead> 
                                <tr style='background:#3b82f6;color:white;'> 
                                    <th style='padding:12px;text-align:left;'>Produit</th> 
                                    <th style='padding:12px;text-align:center;'>Qté</th> 
                                    <th style='padding:12px;text-align:right;'>Prix Unit.</th> 
                                    <th style='padding:12px;text-align:right;'>Total</th> 
                                </tr> 
                            </thead> 
                            <tbody>{cartItemsHtml}</tbody> 
                            <tfoot> 
                                <tr style='background:#fbbf24;'> 
                                    <td colspan='3' style='padding:15px;text-align:right;font-weight:bold;font-size:1.1em;'>TOTAL COMMANDE :</td> 
                                    <td style='padding:15px;text-align:right;font-weight:bold;font-size:1.2em;'>{cartTotal:N0} FCFA</td> 
                                </tr> 
                            </tfoot> 
                        </table> 
                    </div>";
            }

            return $@"
<!DOCTYPE html>
<html lang='fr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            color: white;
            padding: 30px 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
            font-weight: 600;
        }}
        .content {{
            padding: 30px;
        }}
        .field {{
            margin-bottom: 20px;
            border-bottom: 1px solid #e5e7eb;
            padding-bottom: 15px;
        }}
        .field:last-child {{
            border-bottom: none;
        }}
        .label {{
            display: block;
            font-weight: 600;
            color: #1f2937;
            margin-bottom: 5px;
            font-size: 14px;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }}
        .value {{
            color: #4b5563;
            font-size: 16px;
        }}
        .footer {{
            background-color: #f9fafb;
            padding: 20px;
            text-align: center;
            color: #6b7280;
            font-size: 12px;
        }}
        .badge {{
            display: inline-block;
            background-color: #fbbf24;
            color: #1f2937;
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            margin-top: 10px;
        }}
        .urgent {{
            background-color: #fef3c7;
            border-left: 4px solid #fbbf24;
            padding: 15px;
            margin: 20px 0;
            border-radius: 5px;
        }}
        .urgent strong {{
            color: #d97706;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🛒 Nouvelle Commande Reçue</h1>
            <span class='badge'>COMMANDE</span>
        </div>
        <div class='content'>
            <div class='urgent'>
                <strong>⚡ Action requise :</strong> Un client a passé une commande. Veuillez le contacter rapidement pour finaliser la transaction.
            </div>
            
            <div class='field'>
                <span class='label'>👤 Prénom</span>
                <span class='value'>{request.Prenom}</span>
            </div>
            <div class='field'>
                <span class='label'>👤 Nom</span>
                <span class='value'>{request.Nom}</span>
            </div>
            <div class='field'>
                <span class='label'>✉️ Email</span>
                <span class='value'>{request.Email}</span>
            </div>
            <div class='field'>
                <span class='label'>📱 Téléphone</span>
                <span class='value'>{request.Phone}</span>
            </div>
            <div class='field'>
                <span class='label'>🕐 Date de la Commande</span>
                <span class='value'>{DateTime.Now:dddd dd MMMM yyyy à HH:mm:ss}</span>
            </div>
            {cartHtml}
        </div>
        <div class='footer'>
            <p>Cet email a été envoyé automatiquement par le système de commande e-Tech Energie+</p>
        </div>
    </div>
</body>
</html>";
        }

        private string BuildOrderConfirmationHtml(OrderRequest request)
        {
            List<CartItem>? cartItems = null; 
            decimal cartTotal = 0;
            
            if (!string.IsNullOrEmpty(request.CartItemsJson)) 
            { 
                try 
                {
                    cartItems = JsonSerializer.Deserialize<List<CartItem>>(request.CartItemsJson); 
                    if (cartItems != null && cartItems.Any()) 
                       cartTotal = cartItems.Sum(item => item.Total);
                } 
                catch (Exception ex) 
                { 
                    _logger.LogWarning("Erreur désérialisation panier (confirmation): {Message}", ex.Message);
                } 
            }
            
            var cartHtml = string.Empty;
            if (cartItems != null && cartItems.Any())
            {
                var cartItemsHtml = string.Join("", cartItems.Select(item => $@"
                    <div style='display:flex;justify-content:space-between;padding:10px;border-bottom:1px solid #e5e7eb;'>
                        <div>
                            <strong>{item.ProductName}</strong><br>
                            <span style='color:#6b7280;'>Quantité: {item.Quantity}</span>
                        </div>
                        <div style='text-align:right;font-weight:bold;'>{item.Total:N0} FCFA</div>
                    </div>
                "));

                cartHtml = $@"
                    <div style='background:#f0f9ff;padding:20px;border-radius:8px;margin:20px 0;'>
                        <h3 style='color:#1e3a8a;margin-top:0;'>🛒 Votre Commande</h3>
                        <div style='background:white;border-radius:5px;overflow:hidden;'>
                            {cartItemsHtml}
                            <div style='background:#10b981;color:white;padding:15px;display:flex;justify-content:space-between;'>
                                <strong style='font-size:1.1em;'>TOTAL :</strong>
                                <strong style='font-size:1.2em;'>{cartTotal:N0} FCFA</strong>
                            </div>
                        </div>
                    </div>";
            }

            return $@"
<!DOCTYPE html>
<html lang='fr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }}
        .header {{
            background: linear-gradient(135deg, #10b981 0%, #059669 100%);
            color: white;
            padding: 40px 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 700;
        }}
        .header .emoji {{
            font-size: 48px;
            margin-bottom: 10px;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            margin-bottom: 20px;
            color: #1f2937;
        }}
        .message {{
            margin: 25px 0;
            line-height: 1.8;
        }}
        .success-box {{
            background-color: #d1fae5;
            padding: 20px;
            border-radius: 8px;
            margin: 25px 0;
            border-left: 4px solid #10b981;
            text-align: center;
        }}
        .success-box h2 {{
            color: #065f46;
            margin: 0 0 10px 0;
            font-size: 20px;
        }}
        .success-box p {{
            color: #047857;
            margin: 0;
        }}
        .info-box {{
            background-color: #dbeafe;
            padding: 15px;
            border-radius: 8px;
            margin: 20px 0;
            border-left: 4px solid #3b82f6;
        }}
        .info-box p {{
            margin: 5px 0;
            color: #1e40af;
        }}
        .signature {{
            margin-top: 30px;
            font-style: italic;
        }}
        .team-name {{
            font-weight: 700;
            color: #10b981;
        }}
        .footer {{
            background-color: #1f2937;
            color: #9ca3af;
            padding: 20px;
            text-align: center;
            font-size: 12px;
        }}
        .footer a {{
            color: #60a5fa;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='emoji'>✅</div>
            <h1>Commande Confirmée !</h1>
        </div>
        <div class='content'>
            <p class='greeting'>Bonjour <strong>{request.Prenom} {request.Nom}</strong>,</p>
            
            <div class='success-box'>
                <h2>🎉 Votre commande a été enregistrée avec succès !</h2>
                <p>Référence : CMD-{DateTime.Now:yyyyMMddHHmmss}</p>
            </div>
            
            <div class='message'>
                <p>Merci pour votre commande ! Nous sommes ravis de vous compter parmi nos clients.</p>
                <p>Notre équipe va traiter votre demande et vous contacter très prochainement pour :</p>
                <ul>
                    <li>Confirmer les détails de votre commande</li>
                    <li>Discuter des modalités de livraison</li>
                    <li>Finaliser les conditions de paiement</li>
                </ul>
            </div>
            
            {cartHtml}
            
            <div class='info-box'>
                <p><strong>📞 Besoin d'aide ?</strong></p>
                <p>Vous pouvez nous contacter à tout moment :</p>
                <p>Email : {_emailSettings.AdminEmail}</p>
                <p>Nous vous répondrons sous 24-48 heures maximum.</p>
            </div>
            
            <div class='signature'>
                <p>Merci de votre confiance,</p>
                <p class='team-name'>L'équipe e-Tech Energie+</p>
            </div>
        </div>
        <div class='footer'>
            <p>Cet email a été envoyé automatiquement suite à votre commande.</p>
            <p>Pour toute question : <a href='mailto:{_emailSettings.AdminEmail}'>{_emailSettings.AdminEmail}</a></p>
        </div>
    </div>
</body>
</html>";
        }

        #endregion
    }
}
