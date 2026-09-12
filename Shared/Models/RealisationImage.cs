using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETechEnergie.Shared.Models;

/// <summary>
/// Une image (chantier/projet) rattachée à une section de la page "Réalisations".
/// </summary>
public class RealisationImage
{
    public int Id { get; set; }

    [Required]
    public int SectionId { get; set; }

    [ForeignKey(nameof(SectionId))]
    public RealisationSection? Section { get; set; }

    [Required(ErrorMessage = "L'image est requise")]
    public string ImageUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le titre est requis")]
    [StringLength(120)]
    public string Titre { get; set; } = string.Empty;

    [StringLength(120)]
    public string Lieu { get; set; } = string.Empty;

    [StringLength(400)]
    public string? Description { get; set; }

    /// <summary>Ordre d'affichage de l'image au sein de sa section (croissant).</summary>
    public int DisplayOrder { get; set; }
}
