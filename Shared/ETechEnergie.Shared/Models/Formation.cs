namespace ETechEnergie.Shared.Models;

public class Formation
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Prix { get; set; }

    /// <summary>
    /// Image/affiche de la formation. Optionnelle : peut être nulle ou vide si aucune image n'a été fournie.
    /// </summary>
    public string? ImageUrl { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public string Horaires { get; set; } = string.Empty; // ex: "Tous les dimanches de 09h à 13h"
    public string Statut { get; set; } = "A venir"; // "En cours", "Terminée", "A venir"
    public string Lieu { get; set; } = string.Empty;
    public string Partenaires { get; set; } = string.Empty; // séparés par des virgules
    public bool InscriptionOuverte { get; set; } = true;
    public int CapaciteMax { get; set; } = 20;
    public int PlacesRestantes { get; set; } = 20;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<Inscription> Inscriptions { get; set; } = new();
}

public enum FormationStatut
{
    Avenir,
    EnCours,
    Terminee
}
