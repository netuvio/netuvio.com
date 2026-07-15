using server.Models.Projects;

namespace server.Contracts.V1;

public class ProjectResponse
{
    public required Guid Id { get; set; }
    public required string Slug { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Body { get; set; }
    public required bool IsFeatured { get; set; } = false;
    public required List<string> ImageUrls { get; set; } = [];
    public required ProjectType Type { get; set; } = ProjectType.Website;
}