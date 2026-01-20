using System.ComponentModel.DataAnnotations;

public class Service
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La description est requise")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    public string IconClass { get; set; } = string.Empty;
    
    [StringLength(2000)]
    public string DetailedDescription { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}