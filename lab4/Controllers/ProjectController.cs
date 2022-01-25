using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : BaseController
{
    public ProjectController(ILogger<ProjectController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        var unauth = Enforcer.DemandLogged(this);
        if (unauth == null)
        {
            var response = Entities.ProjectsDBEntity.Select().Select(p => p.toJSON());
            return Ok(response);
        }
        else
        {
            return unauth;
        }
    }
}
