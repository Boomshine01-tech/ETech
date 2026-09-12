using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

/// <summary>
/// Bande d'annonce affichée sur toutes les pages du site public.
/// Il n'existe qu'une seule annonce configurable (Id = 1) que l'admin peut activer/désactiver.
/// </summary>
public class Announcement
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le message est requis")]
    [StringLength(300, ErrorMessage = "Le message ne doit pas dépasser 300 caractères")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Lien optionnel (ex: vers une page promo, formation, produit...).
    /// </summary>
    [StringLength(500)]
    public string? LinkUrl { get; set; }

    /// <summary>
    /// Texte du bouton/lien optionnel affiché dans la bande (ex: "En savoir plus").
    /// </summary>
    [StringLength(50)]
    public string? LinkText { get; set; }

    public bool IsActive { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
