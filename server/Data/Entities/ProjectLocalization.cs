using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Data.Entities;

public class ProjectLocalization {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

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
