using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace server.Models.Projects;

public enum ProjectType
{
    Website,
    Graphics
}

[Index(nameof(Slug), IsUnique = true)]
public class Project
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    
    [Required, MaxLength(255)]
    public required string Slug { get; set; }
    
    public bool IsFeatured { get; set; } = false;
    
    public string? ImageUrl { get; set; }
    
    public ProjectType Type { get; set; } = ProjectType.Website;
    
    
    public ICollection<ProjectLocalization> Localizations { get; set; } = new List<ProjectLocalization>();
}