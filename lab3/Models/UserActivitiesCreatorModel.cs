using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;
using NTR.Helpers;

namespace NTR.Models
{
    /// <summary>
    /// A model for user activities creator view.
    /// </summary>
    public class UserActivitiesCreatorModel
    {
        /// <summary>Name of the user.</summary>
        public string User = "";

        /// <summary>Date entered by the user.</summary>
        public DateTime Date;

        /// <summary>Error type when creation fails.</summary>
        public string Error;

        /// <summary>Temporary stored project code.</summary>
        public string TempProject;

        /// <summary>Temporary stored project code.</summary>
        public string TempSubactivity;

        /// <summary>User's monthly report object.</summary>
        public UserMonth UserMonth = new UserMonth();

        /// <summary>List of saved projects.</summary>
        public HashSet<Project> ProjectsList;

        public UserActivitiesCreatorModel()
        {
            this.LoadFromDB();
        }

        public UserActivitiesCreatorModel(string user, string date)
        {
            this.User = user;
            this.Date = DateTime.Parse(date, new CultureInfo("en-US"));
            LoadExtrasFromDB("UserMonth");
        }

        /// <summary>Add new user activity to the saved month.</summary>
        /// <param name="date">Date of the activity in yyyy-MM-dd format.</param>
        /// <param name="code">Code ID of the activity.</param>
        /// <param name="subcode">Subcode of the activity.</param>
        /// <param name="time">Time spent on the activity.</param>
        /// <param name="description">Short description of the activity.</param>
        public void AddUserActivity(string date, string code, string subcode, int time, string description)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("en-US"));
            foreach(UserActivity UA in this.UserMonth.UserActivities)
            {
                if ((UA.ProjectId == code && DateTime.Equals(UA.Date, parsedDate)) && UA.IsEqualSubactivity(subcode))
                {
                    this.Error = "EUNIQUE";
                    return;
                }
            }
            this.UserMonth.UserActivities.Add(new UserActivity(parsedDate, code, subcode, time, description));
        }

        /// <summary>Edit existing user activity.</summary>
        /// <param name="date">Date of the activity in yyyy-MM-dd format.</param>
        /// <param name="project">Code ID of the activity.</param>
        /// <param name="subcode">Subcode of the activity.</param>
        /// <param name="time">Time spent on the activity.</param>
        /// <param name="description">Short description of the activity.</param>
        /// <returns>True for success, false for failure.</returns>
        public bool EditUserActivity(string date, string project, string subcode, int time, string description)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("en-US"));
            foreach(UserActivity UA in this.UserMonth.UserActivities)
            {
                if (UA.ProjectId == project && DateTime.Equals(UA.Date, parsedDate) && UA.IsEqualSubactivity(subcode))
                {
                    UA.Time = time;
                    UA.Description = description;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Generates a select list out of projects list.</summary>
        /// <returns>Enumerable of select list items containing user names.</returns>
        public IEnumerable<SelectListItem> CreateProjectSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                var pl = this.ProjectsList.Where(p => p.Active);
                selectList.AddRange(pl.Select(p => new SelectListItem(p.ProjectId, p.ProjectId)));
                return selectList;
            }
        }

        /// <summary>Generates a select list out of activity list.</summary>
        /// <returns>Enumerable of select list items containing subactivity names.</returns>
        public IEnumerable<SelectListItem> CreateSubactivitySelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                var pl = this.ProjectsList.Where(p => p.ProjectId == this.TempProject);
                if (pl.Count() > 0)
                {
                    //selectList.AddRange(pl.First().Subactivities.Select(sa => new SelectListItem(sa, sa)));
                }
                else
                {
                    selectList.Add(new SelectListItem("", ""));
                }
                return selectList;
            }
        }

        /// <summary>Load projects from the database.</summary>
        public void LoadFromDB()
        {
            this.ProjectsList = Entities.ProjectsDBEntity.Select();
        }

        /// <summary>Load extra data from the database.</summary>
        /// <param name="type">Extra data load. "UserMonth" loads current user month.</param>
        public void LoadExtrasFromDB(string type)
        {
            if (type == "UserMonth")
            {
                this.UserMonth = Entities.UserActivitiesDBEntity.Load(this.User, Helper.GetYM(this.Date));
            }
        }

        /// <summary>Save user activities to the database.</summary>
        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, Helper.GetYM(this.Date), this.UserMonth);
        }
    }
}
