using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NTR.Models;

namespace NTR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserModel model = new UserModel();
            var cookie = Request.Cookies["user"];
            if(cookie != null)
            {
                model.Name = cookie;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(String logout)
        {
            Response.Cookies.Delete("user");
            return View();
        }

        public IActionResult UserView()
        {
            return View(new UserModel());
        }

        [HttpGet]
        public IActionResult UserView(bool? isValidUsername)
        {
            if (isValidUsername.HasValue) {
                return View(new UserModel(isValidUsername.Value));
            } else {
                return View(new UserModel());
            }
        }
        
        [HttpPost]
        public IActionResult UserView(String user, String type)
        {
            if (type == "Create user")
            {
                var userList = Entities.UserListDBEntity.Load();
                if (!userList.Add(user)) {
                    return Redirect("UserView?isValidUsername=false");
                }
                Entities.UserListDBEntity.Save(userList);
            }
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user, cookieOptions);

            return Redirect("/Home/");
        }

        public IActionResult Activities()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
