using System.Reflection;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class IndexController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult Index()
    {
        var version =
            Assembly.GetEntryAssembly()?
                .GetName()
                .Version?
                .ToString();
        
        return Ok($"Deploy server API v1.0 running on version {version}, server time is {DateTime.Now}.");
    }
}