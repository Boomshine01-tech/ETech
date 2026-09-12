using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

/// <summary>
/// Une section de la page "Réalisations" (ex: Génie Civil, Électrique, Partenariats).
/// Chaque section reprend la même architecture visuelle (bloc "hero" avec image + bande
/// de miniatures) ; seul son contenu (textes, thème de couleur, images) change.
/// </summary>
public class RealisationSection
{
    public int Id { get; set; }

    /// <summary>Identifiant technique utilisé comme ancre (#slug) et pour le bouton de navigation.</summary>
    [Required(ErrorMessage = "L'identifiant de la section est requis")]
    [StringLength(50)]
    [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Uniquement des lettres minuscules, chiffres et tirets")]
    public string Slug { get; set; } = string.Empty;

    /// <summary>Libellé court affiché dans le menu de navigation (pilules en haut de page).</summary>
    [Required(ErrorMessage = "Le libellé de navigation est requis")]
    [StringLength(60)]
    public string NavLabel { get; set; } = string.Empty;

    /// <summary>Classe d'icône FontAwesome, sans le préfixe "fas" (ex: "fa-hard-hat").</summary>
    [Required(ErrorMessage = "L'icône est requise")]
    [StringLength(40)]
    public string NavIcon { get; set; } = "fa-image";

    /// <summary>Petit texte au-dessus du titre (ex: "Génie Civil & BTP").</summary>
    [Required(ErrorMessage = "Le texte d'introduction est requis")]
    [StringLength(80)]
    public string Eyebrow { get; set; } = string.Empty;

    /// <summary>Première ligne du grand titre (ex: "Bâtir").</summary>
    [Required(ErrorMessage = "Le titre est requis")]
    [StringLength(40)]
    public string TitleMain { get; set; } = string.Empty;

    /// <summary>Deuxième ligne du grand titre, affichée en italique (ex: "l'avenir").</summary>
    [Required(ErrorMessage = "Le complément de titre est requis")]
    [StringLength(40)]
    public string TitleAccent { get; set; } = string.Empty;

    /// <summary>Paragraphe de description de la section.</summary>
    [Required(ErrorMessage = "La description est requise")]
    [StringLength(600)]
    public string Body { get; set; } = string.Empty;

    /// <summary>Petit libellé au-dessus du titre du projet mis en avant (ex: "Projet en cours").</summary>
    [Required]
    [StringLength(60)]
    public string ProjectLabel { get; set; } = "Projet en cours";

    /// <summary>
    /// Thème visuel de la section, reprenant l'un des styles déjà existants sur le site :
    /// "blue" (fond clair, style Génie Civil), "dark" (fond sombre, style Électrique),
    /// "green" (fond clair accent vert, style Partenariats).
    /// </summary>
    [Required]
    [RegularExpression("^(blue|dark|green)$", ErrorMessage = "Thème invalide")]
    public string Theme { get; set; } = "blue";

    /// <summary>Si vrai, l'image est affichée à droite et le texte à gauche (mise en page inversée).</summary>
    public bool ReverseLayout { get; set; } = false;

    /// <summary>Affiche ou non le petit badge flottant (ex: "Partenariat International — ARLA France").</summary>
    public bool ShowBadge { get; set; } = false;

    [StringLength(40)]
    public string? BadgeIcon { get; set; }

    [StringLength(80)]
    public string? BadgeLabel { get; set; }

    [StringLength(80)]
    public string? BadgeHighlight { get; set; }

    /// <summary>Ordre d'affichage des sections sur la page (croissant).</summary>
    public int DisplayOrder { get; set; }

    /// <summary>Permet de masquer temporairement une section sans la supprimer.</summary>
    public bool IsActive { get; set; } = true;

    public ICollection<RealisationImage> Images { get; set; } = new List<RealisationImage>();
}
