using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Services;

public interface IEmailService
{
    Task SendContactNotificationAsync(ContactRequest request);
    Task SendConfirmationToClientAsync(ContactRequest request);
}