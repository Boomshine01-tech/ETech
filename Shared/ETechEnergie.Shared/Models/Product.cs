using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETechEnergie.Shared.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La description est requise")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; } = "/images/products/default.jpg";
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une catégorie")]
    public int CategoryId { get; set; }
    
    public Category? Category { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Le stock ne peut pas être négatif")]
    public int Stock { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Prix promotionnel optionnel. Si renseigné (et inférieur à Price), une remise est proposée
    /// sur ce produit tant que DiscountEndDate n'est pas dépassée (ou indéfiniment si non renseignée).
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix promotionnel doit être supérieur à 0")]
    public decimal? DiscountPrice { get; set; }

    /// <summary>
    /// Date/heure (UTC) de fin de la remise. Passé ce délai, la remise n'est plus appliquée
    /// automatiquement, même si DiscountPrice reste renseigné en base.
    /// </summary>
    public DateTime? DiscountEndDate { get; set; }

    /// <summary>
    /// Indique si une remise est actuellement active sur ce produit (prix promo valide et non expiré).
    /// </summary>
    [NotMapped]
    public bool HasActiveDiscount =>
        DiscountPrice.HasValue &&
        DiscountPrice.Value > 0 &&
        DiscountPrice.Value < Price &&
        (!DiscountEndDate.HasValue || DiscountEndDate.Value > DateTime.UtcNow);

    /// <summary>
    /// Prix réellement applicable : prix promo si une remise est active, sinon prix normal.
    /// </summary>
    [NotMapped]
    public decimal EffectivePrice => HasActiveDiscount ? DiscountPrice!.Value : Price;

    /// <summary>
    /// Pourcentage de réduction arrondi (ex: 20 pour -20%), null si pas de remise active.
    /// </summary>
    [NotMapped]
    public int? DiscountPercentage =>
        HasActiveDiscount ? (int)Math.Round((1 - (DiscountPrice!.Value / Price)) * 100) : null;
}
public class PagedResult<T>
{

    public List<T> Items { get; set; } = new();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    public int FirstItemIndex => (Page - 1) * PageSize + 1;

    public int LastItemIndex => Math.Min(Page * PageSize, TotalItems);
}
