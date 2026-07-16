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
            ImageUrls = new List<string>
            {
                "https://images.squarespace-cdn.com/content/v1/607f89e638219e13eee71b1e/1684821560422-SD5V37BAG28BURTLIXUQ/michael-sum-LEpfefQf4rU-unsplash.jpg"
            }
        };
        var p2 = new Project
        {
            Slug = "project-beta",
            IsFeatured = true,
            ImageUrls = new List<string>
            {
                "https://pawspace.in/wp-content/uploads/2021/09/siamese-cat-1.jpg"
            },
            Type = ProjectType.Graphics
        };

        p1.Localizations = new List<ProjectLocalization>
        {
            new()
            {
                Locale = "en", 
                Title = "Alpha Project", 
                Body = "Lorem ipsum dolor sit amet.", 
                ProjectId = p1.Id, 
                Project = p1
            },
            new()
            {
                Locale = "cs", 
                Title = "Projekt Alfa", 
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
                Body = "[Click Me](https://netuvio.com) Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris consectetur mauris sed purus molestie, non congue nunc ultricies. In non consequat arcu. Suspendisse nec tristique ante. Vestibulum cursus ante lacinia turpis consequat fringilla. Fusce congue nisl eu mauris suscipit interdum. Suspendisse ac odio leo. Nulla euismod lorem eget urna rhoncus tincidunt. Ut id justo nunc. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc a aliquet eros.", 
                ProjectId = p2.Id, 
                Project = p2 
            },
            new() 
            { 
                Locale = "cs", 
                Title = "Projekt Beta", 
                Description = "Krátký CS popis.", 
                Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris consectetur mauris sed purus molestie, non congue nunc ultricies. In non consequat arcu. Suspendisse nec tristique ante. Vestibulum cursus ante lacinia turpis consequat fringilla. Fusce congue nisl eu mauris suscipit interdum. Suspendisse ac odio leo. Nulla euismod lorem eget urna rhoncus tincidunt. Ut id justo nunc. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc a aliquet eros.", 
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
                ImageUrls = p.ImageUrls,
                Type = p.Type
            };
        }).Where(p => p != null).ToArray();

        return new OkObjectResult(projects);
    }
    
    [HttpGet("{slug}")]
    public IActionResult GetProjectBySlug(string slug, [FromQuery] string? locale = null)
    {
        var project = _projects.FirstOrDefault(p => p.Slug == slug);
        if (project == null)
            return new NotFoundResult();

        var localization = locale != null
            ? project.Localizations.FirstOrDefault(l => l.Locale == locale)
            : project.Localizations.FirstOrDefault();

        if (localization == null)
            return new NotFoundResult();

        var response = new ProjectResponse
        {
            Id = project.Id,
            Slug = project.Slug,
            Title = localization.Title,
            Description = localization.Description,
            Body = localization.Body,
            IsFeatured = project.IsFeatured,
            ImageUrls = project.ImageUrls,
            Type = project.Type
        };

        return new OkObjectResult(response);
    }
}