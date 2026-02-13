namespace ETechEnergie.Server.Configuration;

    public class EmailSettings
    {
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string AdminEmail { get; set; } = string.Empty;
    }

    public class BrevoSettings
    {
        public string ApiKey { get; set; } = string.Empty;
    }

