using System.ComponentModel.DataAnnotations;

namespace ETechEnergie.Shared.Models;

public class User
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public string Role { get; set; } = "Admin"; 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
}