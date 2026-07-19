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
                "https://pawspace.in/wp-content/uploads/2021/09/siamese-cat-1.jpg",
                "https://i.guim.co.uk/img/media/327aa3f0c3b8e40ab03b4ae80319064e401c6fbc/377_133_3542_2834/master/3542.jpg?width=700&quality=85&auto=format&fit=max&s=6358df6dcb66158b5227b0c4444902e7",
                "https://bestfriends.org/sites/default/files/styles/image_small_mobile/public/image/CantaloupeNora1277MW_.jpg?itok=JrkGNp5b",
                "https://upload.wikimedia.org/wikipedia/commons/2/27/Samiha%27s_Siberian_cat_Moon_Pie.jpg",
                "https://static1.squarespace.com/static/66ec3b49803ab81bf84f89e4/66ec3b69bf61a73d71744826/66eca14ee7ad767297ce31d4/1734296092801/?format=1500w"
            },
            Type = ProjectType.Graphics,
            Technologies = ["Next.js", "React", "Nuxt"]
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
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec magna mi, lobortis eget lobortis non, rhoncus ut purus. Curabitur sit amet nisi quis nunc imperdiet tincidunt vel nec orci. Morbi congue, leo consectetur efficitur egestas, libero lectus volutpat justo, ut pulvinar lorem mauris sed eros. Suspendisse elementum dui eget urna pharetra, et fringilla nisl vehicula. Vivamus egestas sed massa quis luctus. Vestibulum blandit sapien lorem, eu semper libero aliquet eu. Aliquam erat volutpat.", 
                Body = @"
# Title text

## 1. Introduction

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin elementum, tellus ac tristique dictum, nisl lorem porta erat, sit amet accumsan tellus erat nec lorem. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam nec ex at massa hendrerit molestie. Ut tristique elementum nisl, non hendrerit lorem porttitor eu.

* **Dolor sit amet:** Consectetur adipiscing elit.
* **Proin elementum:** Tellus ac tristique dictum.
* **Aliquam nec ex:** At massa hendrerit molestie.

## 2. Classical Latin Passage (Cicero)

The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from ""de Finibus Bonorum et Malorum"" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.

### 2.1 Section 1.10.32
> ""Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.""

### 2.2 Section 1.10.33
> ""At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.""

## 3. Structural Elements and Typography

Below is a demonstration of various Markdown formatting styles using standard placeholder text.

### 3.1 Text Styles
* *Italicized text* represents soft emphasis.
* **Bold text** represents strong emphasis.
* ***Bold and italicized text*** represents maximum emphasis.
* ~~Strikethrough text~~ represents deleted or outdated details.
* `Monospace text` represents inline code or literal inputs.

### 3.2 Tabular Data

| Metric ID | Target Parameter | Status | Value (Est.) | Performance Trend |
| :--- | :--- | :---: | :---: | :--- |
| **SYS-001** | Lorem Ipsum Limit | Active | 98.4% | Escalating upward |
| **SYS-002** | Dolor Sit Delay | Pending | 12.0 ms | Holding steady |
| **SYS-003** | Consectetur Load | Failed | N/A | Declining rapidly |
| **SYS-004** | Adipiscing Rate | Active | 450 kbps | Fluctuating slightly |

---

## 4. Code Blocks & Implementation

Here is an example of how one might programmatically generate placeholder paragraphs in Python:

```python
import random

def generate_lorem_ipsum(paragraphs_count=3):
    words = [
        ""lorem"", ""ipsum"", ""dolor"", ""sit"", ""amet"", ""consectetur"", ""adipiscing"", 
        ""elit"", ""sed"", ""do"", ""eiusmod"", ""tempor"", ""incididunt"", ""ut"", ""labore""
    ]
    generated_text = []
    for _ in range(paragraphs_count):
        paragraph = "" "".join(random.choices(words, k=50)).capitalize() + "".""
        generated_text.append(paragraph)
    return ""\\n\\n"".join(generated_text)

# Example output
print(generate_lorem_ipsum(1))
```

Press `test` to continue
", 
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
                Type = p.Type,
                Technologies = p.Technologies
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
            Type = project.Type,
            Technologies = project.Technologies
        };

        return new OkObjectResult(response);
    }
}