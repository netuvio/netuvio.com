using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.Responses;

namespace server.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects")]
public class ProjectsControllerV1(AppDbContext db) : Controller {

    [HttpGet]
    public async Task<IActionResult> GetProjects(
        [FromQuery] string? locale = null,
        [FromQuery] bool? includeBody = false,
        CancellationToken ct = default
    ) {
        var response = await db.ProjectLocalizations
            .AsNoTracking()
            .Where(l => l.Locale == locale)
            .Select(l => new ProjectResponse {
                Id = l.Project.Id,
                Slug = l.Project.Slug,
                Title = l.Title,
                Description = l.Description,
                Body = l.Body,
                IsFeatured = l.Project.IsFeatured,
                ImageUrls = l.Project.ImageUrls,
                Type = l.Project.Type,
                Technologies = l.Project.Technologies,
                StartedAt = l.Project.StartedAt,
                FinishedAt = l.Project.FinishedAt,
                WebsiteUrl = l.Project.WebsiteUrl,
                SourceCodeUrl = l.Project.SourceCodeUrl
            })
            .ToListAsync(ct);

        return new OkObjectResult(response);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetProjectBySlug(
        string slug,
        [FromQuery] string? locale = null,
        CancellationToken ct = default
    ) {
        var response = await db.ProjectLocalizations
            .AsNoTracking()
            .Where(l => l.Project.Slug == slug && l.Locale == locale)
            .Select(l => new ProjectResponse {
                Id = l.Project.Id,
                Slug = l.Project.Slug,
                Title = l.Title,
                Description = l.Description,
                Body = l.Body,
                IsFeatured = l.Project.IsFeatured,
                ImageUrls = l.Project.ImageUrls,
                Type = l.Project.Type,
                Technologies = l.Project.Technologies,
                StartedAt = l.Project.StartedAt,
                FinishedAt = l.Project.FinishedAt,
                WebsiteUrl = l.Project.WebsiteUrl,
                SourceCodeUrl = l.Project.SourceCodeUrl
            })
            .FirstOrDefaultAsync(ct);

        return new OkObjectResult(response);
    }
}
