using Microsoft.AspNetCore.Mvc;
using lab4.Entities;

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
        var response = Entities.UsersDBEntity.Select();
        return Ok(response);
    }

    [HttpGet]
    [Route("new")]
    public IActionResult CreateUser()
    {
        Random random = new Random();
        const int length = 4;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string user = new String(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        Entities.UsersDBEntity.Insert(user);
        return Ok(user);
    }
}
