using Microsoft.AspNetCore.Mvc;
using lab4.Entities;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    public UsersController(ILogger<UsersController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        var response = Entities.UsersDBEntity.Select();
        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(string user)
    {
        var response = Entities.UsersDBEntity.Find(user);
        return Ok(response);
    }

    [HttpPut]
    [Route("create")]
    public IActionResult Create(string user)
    {
        Entities.UsersDBEntity.Insert(user);
        return Ok(user);
    }
}
