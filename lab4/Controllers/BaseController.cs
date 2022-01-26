using Microsoft.AspNetCore.Mvc;

using lab4.Entities;

namespace lab4.Controllers;

public class Enforcer
{
    static public IActionResult? DemandLogged(ControllerBase cbase)
    {
        var user = cbase.Request.Cookies["sessionUser"];
        if (!UsersDBEntity.Find(user) || user == null) {
            return cbase.Unauthorized();
        } else {
            var cookieOptions = new CookieOptions{ HttpOnly = false, Secure = false, MaxAge = TimeSpan.FromMinutes(60) };
            cbase.Response.Cookies.Append("sessionUser", user, cookieOptions);
            return null;
        }
    }
}


[ApiController]
public class BaseController : ControllerBase
{
    private ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
}
