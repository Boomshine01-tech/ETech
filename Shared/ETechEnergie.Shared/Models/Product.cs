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
    public decimal? DiscountPrice { get; set; }
    public DateTime? DiscountEndDate { get; set; }
    [NotMapped]
    public bool IsOnDiscount => DiscountPrice.HasValue && 
                                DiscountEndDate.HasValue && 
                                DiscountEndDate.Value > DateTime.UtcNow;

    [NotMapped]
    public decimal ActivePrice => IsOnDiscount ? DiscountPrice!.Value : Price;

    public bool IsOnOrder { get; set; }
    
    public string ImageUrl { get; set; } = "/images/products/default.jpg";
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une catégorie")]
    public int CategoryId { get; set; }
    
    public Category? Category { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Le stock ne peut pas être négatif")]
    public int Stock { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
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
