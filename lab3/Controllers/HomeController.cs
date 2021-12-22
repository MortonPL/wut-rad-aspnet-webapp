using System;
using System.Globalization;
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
using NTR.Helpers;

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
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("CurrentUser");
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
            if (user != null)
            {
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
                Response.Cookies.Append("CurrentUser", user, cookieOptions);
            }

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
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("CurrentUser", user, cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        // ****************************** USER ACTIVITIES ****************************** //
        public IActionResult UserActivitiesView()
        {
            UserActivitiesModel model = new UserActivitiesModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult UserActivitiesView(UserActivitiesModel model, string month)
        {
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieDate = Request.Cookies["ActivitiesViewDate"];
            if (!String.IsNullOrEmpty(month))
            {
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false };
                Response.Cookies.Append("ActivitiesViewMonthly", month, cookieOptions);
                model.IsMonthlyView = Convert.ToBoolean(month);
            }
            var cookieMonthly = Request.Cookies["ActivitiesViewMonthly"];
            if (cookieUser != null)
            {
                model.User = cookieUser;
                if (cookieDate != null)
                {
                    model.Date = DateTime.Parse(cookieDate, new CultureInfo("pl-pl"));
                }
                if ((cookieMonthly != null) && String.IsNullOrEmpty(month))
                {
                    model.IsMonthlyView = Convert.ToBoolean(cookieMonthly);
                }
                model.LoadFromDB();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult UserActivitiesView(String date)
        {
            UserActivitiesModel model = new UserActivitiesModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieMonthly = Request.Cookies["ActivitiesViewMonthly"];
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("ActivitiesViewDate", date, cookieOptions);
            if (cookieUser != null)
            {
                model.User = cookieUser;
                model.LoadFromDB();
            }
            model.Date = DateTime.Parse(date, new CultureInfo("pl-pl"));;
            if (cookieMonthly != null)
            {
                model.IsMonthlyView = Convert.ToBoolean(cookieMonthly);
            }

            return View(model);
        }

        public IActionResult UserActivityDelete(string code, string date, string subcode)
        {
            UserActivitiesModel model = new UserActivitiesModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
                model.DeleteUserActivity(code, date, subcode);
            }

            return RedirectToAction("UserActivitiesView", "Home");
        }

        public IActionResult UserActivitiesLock(string date)
        {
            UserActivitiesModel model = new UserActivitiesModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.Date = DateTime.Parse(date, new CultureInfo("pl-pl"));;
                model.LoadFromDB();
                model.LockUserActivity();
            }

            return RedirectToAction("UserActivitiesView", "Home");
        }

        public IActionResult UserActivitiesEditorView(string code, string date, string subcode, string error, int time, string desc, string pid)
        {
            UserActivitiesCreatorModel model = new UserActivitiesCreatorModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            if (cookieUser != null)
            {
                model = new UserActivitiesCreatorModel(cookieUser, date, code, subcode);
                model.TempProject = code;
                model.TempSubactivity = subcode;
                model.Error = error;
                model.Time = time;
                model.Description = desc;
                if (!String.IsNullOrEmpty(pid)) model.Pid = Int32.Parse(pid);
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(5) };
                Response.Cookies.Append("tempProject", code, cookieOptions);
                Response.Cookies.Append("tempDate", date, cookieOptions);
                Response.Cookies.Append("tempSubactivity", subcode, cookieOptions);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult UserActivitiesEdit(string TempProject, string TempSubactivity, string time, string activity, string Timestamp, string pid)
        {
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieProject = Request.Cookies["tempProject"];
            var cookieDate = Request.Cookies["tempDate"];
            var cookieSubactivity = Request.Cookies["tempSubactivity"];
            if (cookieUser != null)
            {
                UserActivitiesCreatorModel model = new UserActivitiesCreatorModel(cookieUser, cookieDate);
                model.Timestamp = Convert.FromBase64String(Timestamp);
                if (!model.EditUserActivity(cookieDate, cookieProject, Uri.UnescapeDataString(cookieSubactivity), Int32.Parse(time), activity, Int32.Parse(pid)))
                {
                    return RedirectToAction("UserActivitiesEditorView", "Home", new {
                        code = TempProject, date=cookieDate, subcode=TempSubactivity, error="ECONC", time=time, desc=activity, pid=pid});
                }
            }

            return RedirectToAction("UserActivitiesView", "Home");
        }

        // ****************************** USER ACTIVITIES MONTHLY ****************************** //
        public IActionResult UserActivitiesMonthlyView()
        {
            UserActivitiesModel model = new UserActivitiesModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult UserActivitiesMonthlyView(UserActivitiesModel model)
        {
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieDate = Request.Cookies["ActivitiesMonthlyViewDate"];
            if (cookieUser != null)
            {
                model.User = cookieUser;
                if (cookieDate != null)
                {
                    model.Date = DateTime.Parse(cookieDate, new CultureInfo("pl-pl"));
                }
                model.LoadFromDB();
            }

            return View(model);
        }        

        [HttpPost]
        public IActionResult UserActivitiesMonthlyView(String date)
        {
            UserActivitiesModel model = new UserActivitiesModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("ActivitiesMonthlyViewDate", date, cookieOptions);
            if (cookieUser != null)
            {
                model.User = cookieUser;
                model.LoadFromDB();
            }
            model.Date = DateTime.Parse(date, new CultureInfo("pl-pl"));;

            return View(model);
        }

        // ****************************** USER ACTIVITIES CREATOR ****************************** //
        public IActionResult UserActivitiesCreatorView()
        {
            UserActivitiesCreatorModel model = new UserActivitiesCreatorModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult UserActivitiesCreatorView(UserActivitiesCreatorModel model, string error)
        {
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.Error = error;
            }

            return View(model);
        }

        
        [HttpPost]
        public IActionResult UserActivitiesCreate(string project, string date)
        {
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                UserActivitiesCreatorModel model = new UserActivitiesCreatorModel(cookie, date);
                if (!String.IsNullOrEmpty(model.Error))
                {
                    return RedirectToAction("UserActivitiesCreatorView", "Home", new {error=model.Error});
                }
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(5) };
                Response.Cookies.Append("tempProject", project, cookieOptions);
                Response.Cookies.Append("tempDate", date, cookieOptions);
            }
            return RedirectToAction("UserActivitiesCreator2View", "Home");
        }

        [HttpGet]
        public IActionResult UserActivitiesCreator2View(UserActivitiesCreatorModel model, string error)
        {
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieProject = Request.Cookies["tempProject"];
            var cookieDate = Request.Cookies["tempDate"];
            if (cookieUser != null && cookieProject != null && cookieDate != null)
            {
                model.User = cookieUser;
                model.Date = DateTime.Parse(cookieDate, new CultureInfo("pl-pl"));;
                model.TempProject = cookieProject;
                model.Error = error;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UserActivitiesCreate2(string sub, string time, string activity)
        {
            UserActivitiesCreatorModel model;
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieProject = Request.Cookies["tempProject"];
            var cookieDate = Request.Cookies["tempDate"];
            if (cookieUser != null && cookieProject != null && cookieDate != null)
            {
                model = new UserActivitiesCreatorModel(cookieUser, cookieDate);
                model.TempProject = cookieProject;
                model.AddUserActivity(cookieDate, cookieProject, sub, Int32.Parse(time), activity);
                if (!String.IsNullOrEmpty(model.Error))
                {
                    return RedirectToAction("UserActivitiesCreator2View", "Home", new {error=model.Error});
                }
                Response.Cookies.Delete("tempProject");
                Response.Cookies.Delete("tempDate");
            }
            return RedirectToAction("UserActivitiesView", "Home");
        }

        // ****************************** PROJECTS ****************************** //
        public IActionResult ProjectsView()
        {
            return View(new ProjectsModel());
        }

        [HttpGet]
        public IActionResult ProjectsView(ProjectsModel model)
        {
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
            }
            model.LoadFromDB();

            return View(model);
        }

        public IActionResult ProjectClose(string code)
        {
            ProjectsModel model = new ProjectsModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
                model.CloseProject(code);
            }

            return RedirectToAction("ProjectsView", "Home");
        }

        // ****************************** PROJECTS CREATOR ****************************** //
        public IActionResult ProjectsCreatorView()
        {
            ProjectsCreatorModel model = new ProjectsCreatorModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ProjectsCreatorView(ProjectsCreatorModel model)
        {
            var cookie = Request.Cookies["CurrentUser"];
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
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            if (model.AddProject(code, name, Int32.Parse(budget), project))
            {
                return RedirectToAction("ProjectsView", "Home");
            }

            return View(model);
        }

        public IActionResult ProjectsEditorView(string projectId, string error, string name, int budget)
        {
            ProjectsCreatorModel model = new ProjectsCreatorModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            if (cookieUser != null)
            {
                model = new ProjectsCreatorModel(cookieUser, projectId);
                model.Error = error;
                model.Name = name;
                model.Budget = budget;
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(5) };
                Response.Cookies.Append("tempProject", model.tempProject, cookieOptions);
                Response.Cookies.Append("tempSubs", model.tempSubs, cookieOptions);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ProjectsEditor(string budget, string name, string Timestamp)
        {
            ProjectsCreatorModel model;
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieProject = Request.Cookies["tempProject"];
            if (cookieUser != null)
            {
                model = new ProjectsCreatorModel(cookieUser);
                model.Timestamp = Convert.FromBase64String(Timestamp);
                if (!model.EditProject(cookieProject, name, Int32.Parse(budget)))
                {
                    return RedirectToAction("ProjectsEditorView", "Home", new {
                        projectId=cookieProject, error="ECONC", name=name, budget=budget});
                }
            }

            return RedirectToAction("ProjectsView", "Home");
        }

        // ****************************** PROJECT INSPECTOR ****************************** //
        [HttpGet]
        public IActionResult ProjectInspectorView(string projectId, string dummy)
        {
            ProjectInspectorModel model = new ProjectInspectorModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieDate = Request.Cookies["ProjectInspectorDate"];
            if (cookieUser != null)
            {
                model.User = cookieUser;
                model.ProjectId = projectId;
                if (cookieDate != null)
                {
                    model.Date = DateTime.Parse(cookieDate, new CultureInfo("pl-pl"));
                }
                model.LoadFromDB();
            }

            return View(model);
        }     

        [HttpPost]
        public IActionResult ProjectInspectorView(string date)
        {
            ProjectInspectorModel model = new ProjectInspectorModel();
            var cookieUser = Request.Cookies["CurrentUser"];
            var cookieOptions = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("ProjectInspectorDate", date, cookieOptions);
            if (cookieUser != null)
            {
                model.User = cookieUser;
                model.Date = DateTime.Parse(date, new CultureInfo("pl-pl"));
                model.LoadFromDB();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ProjectInspectorApprovalView(string user, string date, string time)
        {
            ProjectInspectorModel model = new ProjectInspectorModel();
            var cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                model.User = cookie;
                model.Date = DateTime.Parse(date, new CultureInfo("pl-pl"));
                model.LoadFromDB();
            }

            return View(model);
        }
        // ****************************** OTHER ****************************** //
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
