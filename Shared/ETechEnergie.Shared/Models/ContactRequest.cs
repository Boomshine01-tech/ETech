using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

public class ContactRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Company { get; set; }
    
    [Required(ErrorMessage = "Le téléphone est requis")]
    [Phone(ErrorMessage = "Format de téléphone invalide")]
    public string Phone { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Le message est requis")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Le message doit contenir entre 10 et 2000 caractères")]
    public string Message { get; set; } = string.Empty;

    public string? CartItemsJson { get; set; }
   
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public bool IsProcessed { get; set; } = false;
}