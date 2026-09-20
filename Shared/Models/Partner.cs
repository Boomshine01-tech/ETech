using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

/// <summary>
/// Un partenaire de l'entreprise, affiché dans la section "Nos partenaires"
/// de la page d'accueil, juste en dessous des témoignages ("Ils nous font confiance").
/// </summary>
public class Partner
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom du partenaire est requis")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>URL du logo (hébergé sur Supabase Storage, dossier "partenaires").</summary>
    [Required(ErrorMessage = "Le logo est requis")]
    public string LogoUrl { get; set; } = string.Empty;

    /// <summary>Courte description ou nature du partenariat (optionnel, ex: "Fournisseur officiel de panneaux solaires").</summary>
    [StringLength(300)]
    public string? Description { get; set; }

    /// <summary>Lien vers le site du partenaire (optionnel). Si renseigné, le logo devient cliquable sur la page publique.</summary>
    [StringLength(300)]
    [Url(ErrorMessage = "L'URL du site n'est pas valide")]
    public string? WebsiteUrl { get; set; }

    /// <summary>Ordre d'affichage sur la page d'accueil (croissant).</summary>
    public int DisplayOrder { get; set; }

    /// <summary>Permet de masquer temporairement un partenaire sans le supprimer.</summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
