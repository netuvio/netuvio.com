using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace server.Data.Entities;

public enum ProjectType { Website, Graphics }




[Index(nameof(Slug), IsUnique = true)]
public class Project {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] [MaxLength(255)]
    public required string Slug { get; set; }

    public bool IsFeatured { get; set; } = false;

    [MaxLength(1024)]
    public List<string> ImageUrls { get; set; } = [];

    public ProjectType Type { get; set; } = ProjectType.Website;

    public List<string> Technologies { get; set; } = [];

    public DateTimeOffset? StartedAt { get; set; }

    public DateTimeOffset? FinishedAt { get; set; }

    [MaxLength(512)]
    public string? WebsiteUrl { get; set; }

    [MaxLength(512)]
    public string? SourceCodeUrl { get; set; }


    public ICollection<ProjectLocalization> Localizations { get; set; } = new List<ProjectLocalization>();
}
