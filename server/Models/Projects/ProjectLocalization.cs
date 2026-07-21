using System.ComponentModel.DataAnnotations;

namespace server.Models.Projects;

public class ProjectLocalization
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(10)]
    public required string Locale { get; set; }
    
    [Required, MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }
    
    [Required, MaxLength(10000)]
    public required string Body { get; set; }
    
    [Required]
    public required Guid ProjectId { get; set; }
    
    public required Project Project { get; set; }
}