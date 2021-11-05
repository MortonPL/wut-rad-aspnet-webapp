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
using NTR.Entities;

namespace NTR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ****************************** INDEX ****************************** //
        public IActionResult Index()
        {
            UserModel model = new UserModel();
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }

        // ****************************** USER ****************************** //
        public IActionResult UserView()
        {
            return View(new UserModel());
        }

        [HttpGet]
        public IActionResult UserView(UserModel model)
        {
            return View(model);
        }
        
        [HttpPost]
        public IActionResult UserLogin(String user)
        {
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user, cookieOptions);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UserCreate(String user)
        { 
            UserModel model = new UserModel(user);
            if (!String.IsNullOrEmpty(model.UsernameError))
            {
                return UserView(model);
            }

            model.AddUser(user);
            model.SaveToDB();
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user, cookieOptions);

            return RedirectToAction("Index", "Home");
        }

        // ****************************** USER ACTIVITIES ****************************** //
        public IActionResult UserActivitiesView()
        {
            return View(new UserActivitiesModel());
        }

        [HttpGet]
        public IActionResult UserActivitiesView(UserActivitiesModel model)
        {
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult UserActivitiesView(String date)
        {
            UserActivitiesModel model = new UserActivitiesModel();
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            model.Date = date;

            return View(model);
        }

        // ****************************** PROJECTS ****************************** //
        public IActionResult ProjectsView()
        {
            return View(new ProjectsModel());
        }

        [HttpGet]
        public IActionResult ProjectsView(ProjectsModel model)
        {
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
            }
            model.LoadFromDB();

            return View(model);
        }

        // ****************************** PROJECTS CREATOR ****************************** //
        public IActionResult ProjectsCreatorView()
        {
            ProjectsCreatorModel model = new ProjectsCreatorModel();
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ProjectsCreatorView(ProjectsCreatorModel model)
        {
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ProjectCreate(string code, string name, string budget, string project)
        {
            ProjectsCreatorModel model = new ProjectsCreatorModel();
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            if (model.AddProject(code, name, Int32.Parse(budget), project))
            {
                model.SaveToDB();
                return RedirectToAction("ProjectsView", "Home");
            }

            return View(model);
        }

        // ****************************** USER ACTIVITIES CREATOR ****************************** //
        public IActionResult UserActivitiesCreatorView()
        {
            UserActivitiesCreatorModel model = new UserActivitiesCreatorModel();
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult UserActivitiesCreatorView(UserActivitiesCreatorModel model)
        {
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                model.User = cookie;
            }

            return View(model);
        }

        
        [HttpPost]
        public IActionResult UserActivitiesCreate(string project, string date)
        {
            var cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                UserActivitiesCreatorModel model = new UserActivitiesCreatorModel(cookie, date);
            }

            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(5) };
            Response.Cookies.Append("tempProject", project, cookieOptions);
            Response.Cookies.Append("tempDate", date, cookieOptions);

            return RedirectToAction("UserActivitiesCreator2View", "Home");
        }

        [HttpGet]
        public IActionResult UserActivitiesCreator2View(UserActivitiesCreatorModel model, bool error)
        {
            var cookieUser = Request.Cookies["user"];
            var cookieProject = Request.Cookies["tempProject"];
            var cookieDate = Request.Cookies["tempDate"];
            if (cookieUser != null && cookieProject != null && cookieDate != null)
            {
                model.User = cookieUser;
                model.Date = cookieDate;
                model.TempProject = cookieProject;
                model.Error = error;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UserActivitiesCreate2(string sub, string time, string activity)
        {
            UserActivitiesCreatorModel model;
            var cookieUser = Request.Cookies["user"];
            var cookieProject = Request.Cookies["tempProject"];
            var cookieDate = Request.Cookies["tempDate"];
            if (cookieUser != null && cookieProject != null && cookieDate != null)
            {
                model = new UserActivitiesCreatorModel(cookieUser, cookieDate);
                model.TempProject = cookieProject;
                if (!model.AddUserActivity(cookieDate, cookieProject, sub, Int32.Parse(time), activity))
                {
                    return RedirectToAction("UserActivitiesCreator2View", "Home", new {error=true});
                }
                model.SaveToDB();
                Response.Cookies.Delete("tempProject");
                Response.Cookies.Delete("tempDate");
            }
            return RedirectToAction("UserActivitiesView", "Home");
        }

        // ****************************** OTHER ****************************** //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
