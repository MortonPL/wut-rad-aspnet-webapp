using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using lab4.Entities;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    public UserController(ILogger<UserController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    [Route("me")]
    public IActionResult AmILogged()
    {
        
        var cookie = Request.Cookies["sessionUser"];
        if (string.IsNullOrEmpty(cookie)) {
            return Unauthorized();
        } else {
            var response = new {name=cookie};
            return Ok(response);
        }
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        var response = Entities.UsersDBEntity.SelectNames();
        return Ok(response);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(string user)
    {
        var response = Entities.UsersDBEntity.Find(user);
        if (response) {
            var cookieOptions = new CookieOptions{ HttpOnly = false, Secure = false, MaxAge = TimeSpan.FromMinutes(60) };
            Response.Cookies.Append("sessionUser", user, cookieOptions);
            return Ok(response);
        } else {
            return NotFound(response);
        }
    }

    [HttpPost]
    [Route("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("sessionUser");
        return Ok();
    }

    [HttpPut]
    [Route("create")]
    public IActionResult Create(string user)
    {
        Entities.UsersDBEntity.Insert(user);
        return Ok(user);
    }
}
