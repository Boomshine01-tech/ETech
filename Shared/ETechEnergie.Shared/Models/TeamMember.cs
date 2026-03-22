namespace ETechEnergie.Shared.Models;

public class TeamMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = "/images/team/default.jpg";
    public string? LinkedIn { get; set; }
    public int DisplayOrder { get; set; }
}