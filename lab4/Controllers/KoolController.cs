using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KoolController : BaseController
{
    public KoolController(ILogger<KoolController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Enumerable.Range(1, 5).ToArray());
    }
}
