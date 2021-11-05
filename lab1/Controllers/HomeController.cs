﻿using System;
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
            if(cookie != null)
            {
                model.User = cookie;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(String logout)
        {
            Response.Cookies.Delete("user");
            return View();
        }

        // ****************************** USER ****************************** //
        public IActionResult UserView()
        {
            return UserView(new UserModel());
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

            return Redirect("/Home/");
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

            return Redirect("/Home/");
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
            if(cookie != null)
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
            if(cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            model.Date = date;
            return View(model);
        }

        // ****************************** ACTIVITIES ****************************** //
        public IActionResult ActivitiesView()
        {
            return View(new ActivitiesModel());
        }

        [HttpGet]
        public IActionResult ActivitiesView(ActivitiesModel model)
        {
            model.LoadFromDB();
            return View(model);
        }

        // ****************************** ACTIVITIES CREATOR ****************************** //
        public IActionResult ActivitiesCreatorView()
        {
            ActivitiesCreatorModel model = new ActivitiesCreatorModel();
            var cookie = Request.Cookies["user"];
            if(cookie != null)
            {
                model.User = cookie;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ActivitiesCreatorView(ActivitiesCreatorModel model)
        {
            var cookie = Request.Cookies["user"];
            if(cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult ActivityCreate(string code, string name, string budget, string project)
        {
            ActivitiesCreatorModel model = new ActivitiesCreatorModel();
            var cookie = Request.Cookies["user"];
            if(cookie != null)
            {
                model.User = cookie;
                model.LoadFromDB();
            }
            if (model.AddActivity(code, name, Int32.Parse(budget), project))
            {
                model.SaveToDB();
                return RedirectToAction("ActivitiesView", "Home");
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
