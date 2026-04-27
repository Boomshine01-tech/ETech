namespace ETechEnergie.Shared.Models;

public class Inscription
{
    public int Id { get; set; }
    public int FormationId { get; set; }
    public Formation Formation { get; set; } = null!;
    public string Nom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public DateTime DateInscription { get; set; } = DateTime.UtcNow;
    public string Statut { get; set; } = "Confirmée"; // "Confirmée", "En attente", "Annulée"
    public string? Message { get; set; }
}
