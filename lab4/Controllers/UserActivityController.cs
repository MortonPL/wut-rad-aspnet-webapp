using Microsoft.AspNetCore.Mvc;

using lab4.Entities;

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
    public IActionResult GetOne(string user, string month)
    {
        var parsed = month.Split('-').Select((string s) => int.Parse(s)).ToList();
        var date = new DateTime(parsed[0], parsed[1], parsed[2]);
        var response = Entities.UserActivitiesDBEntity.Select(user, date).toJSON();
        return Ok(response);
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create([FromBody] UserActivityJson uaj)
    {
        var unauth = Enforcer.DemandLogged(this);
        if (unauth == null)
        {
            
            return Ok();
        }
        else
        {
            return unauth;
        }
    }

    [HttpPatch]
    [Route("edit")]
    public IActionResult Edit([FromBody] UserActivityJson uaj)
    {
        var unauth = Enforcer.DemandLogged(this);
        if (unauth == null)
        {
            Entities.UserActivitiesDBEntity.Update(uaj.date, uaj.pid, uaj.userName, uaj.projectId, uaj.subactivityId, uaj.time, uaj.description);
            return Ok();
        }
        else
        {
            return unauth;
        }
    }

    [HttpDelete]
    [Route("delete")]
    public IActionResult Delete([FromBody] UserActivityJson uaj)
    {
        var unauth = Enforcer.DemandLogged(this);
        if (unauth == null)
        {
            Entities.UserActivitiesDBEntity.Delete(uaj.userName, uaj.projectId, uaj.date, uaj.subactivityId);
            return Ok();
        }
        else
        {
            return unauth;
        }
    }
}
