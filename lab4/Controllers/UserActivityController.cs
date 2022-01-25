using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserActivityController : BaseController
{
    public UserActivityController(ILogger<UserController> logger)
        : base(logger)
    {
    }

    [HttpGet]
    [Route("one")]
    public IActionResult GetOne(string user, DateTime month)
    {
        var response = Entities.UserActivitiesDBEntity.Select(user, month).toJSON();
        return Ok(response);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create([FromBody] string user)
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
}
