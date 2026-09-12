using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom de la catégorie est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne doit pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
