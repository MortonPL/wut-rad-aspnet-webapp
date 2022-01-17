using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : Controller
{
    public BaseController()
    {
    }

    [HttpGet]
    public IEnumerable<int> Get()
    {
        return Enumerable.Range(1, 5).ToArray();
    }
}
