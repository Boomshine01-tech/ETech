namespace ETechEnergie.Client.Services;

public class NotificationService
{
    public event Action<string, NotificationType>? OnNotification;

    public void ShowSuccess(string message)
    {
        OnNotification?.Invoke(message, NotificationType.Success);
    }

    public void ShowError(string message)
    {
        OnNotification?.Invoke(message, NotificationType.Error);
    }

}

public enum NotificationType
{
    Success,
    Error
}
