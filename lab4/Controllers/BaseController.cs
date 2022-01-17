using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
public class BaseController : Controller
{
    private ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
}
