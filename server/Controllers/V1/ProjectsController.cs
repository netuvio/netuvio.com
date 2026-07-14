using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using server.Contracts.V1;
using server.Models.Projects;

namespace server.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projects")]
public class ProjectsController
{
    private readonly List<Project> _projects;

    public ProjectsController()
    {
        var p1 = new Project
        {
            Slug = "project-alpha",
            IsFeatured = true,
            ImageUrl = "https://images.squarespace-cdn.com/content/v1/607f89e638219e13eee71b1e/1684821560422-SD5V37BAG28BURTLIXUQ/michael-sum-LEpfefQf4rU-unsplash.jpg"
        };
        var p2 = new Project
        {
            Slug = "project-beta",
            IsFeatured = true,
            ImageUrl = "https://pawspace.in/wp-content/uploads/2021/09/siamese-cat-1.jpg",
            Type = ProjectType.Graphics
        };

        p1.Localizations = new List<ProjectLocalization>
        {
            new()
            {
                Locale = "en", 
                Title = "Alpha Project", 
                Description = "Short EN description.", 
                Body = "Lorem ipsum dolor sit amet.", 
                ProjectId = p1.Id, 
                Project = p1
            },
            new()
            {
                Locale = "cs", 
                Title = "Projekt Alfa", 
                Description = "Krátký CS popis.", 
                Body = "Lorem ipsum dolor sit amet.", 
                ProjectId = p1.Id, 
                Project = p1
            }
        };

        p2.Localizations = new List<ProjectLocalization>
        {
            new() 
            { 
                Locale = "en", 
                Title = "Beta Project", 
                Description = "Short EN description.", 
                Body = "Sed ut perspiciatis unde omnis.", 
                ProjectId = p2.Id, 
                Project = p2 
            },
            new() 
            { 
                Locale = "cs", 
                Title = "Projekt Beta", 
                Description = "Krátký CS popis.", 
                Body = "Sed ut perspiciatis unde omnis.", 
                ProjectId = p2.Id, 
                Project = p2
            }
        };

        _projects = [p1, p2];
    }
    
    [HttpGet]
    public IActionResult GetProjects([FromQuery] string? locale = null)
    {
        var projects = _projects.Select(p =>
        {
            var localization = locale != null
                ? p.Localizations.FirstOrDefault(l => l.Locale == locale)
                : p.Localizations.FirstOrDefault();

            if (localization == null) 
                return null;

            return new ProjectResponse
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = localization.Title,
                Description = localization.Description,
                Body = localization.Body,
                IsFeatured = p.IsFeatured,
                ImageUrl = p.ImageUrl,
                Type = p.Type
            };
        }).Where(p => p != null).ToArray();

        return new OkObjectResult(projects);
    }
}