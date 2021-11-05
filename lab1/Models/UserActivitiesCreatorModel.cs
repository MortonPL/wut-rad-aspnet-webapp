using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

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
        public string Date;

        public bool Error;

        /// <summary>Temporary stored project code.</summary>
        public string TempProject;

        /// <summary>User's monthly report object.</summary>
        public UserMonth UserMonth;

        /// <summary>List of saved projects.</summary>
        public HashSet<Project> ProjectsList;

        public UserActivitiesCreatorModel()
        {
            this.LoadFromDB();
        }

        public UserActivitiesCreatorModel(string user, string date)
        {
            this.User = user;
            this.Date = date;
            LoadExtrasFromDB("UserMonth");
        }

        /// <summary>Extract the saved date.</summary>
        /// <returns>Saved date as string in yyyy-MM format.</returns>
        public string GetMonth()
        {
            return this.Date.Remove(Date.Length - 3);
        }

        /// <summary>Add new user activty to the saved month.</summary>
        /// <param name="date">Date of the activity in yyyy-MM-dd format.</param>
        /// <param name="code">Code ID of the activity.</param>
        /// <param name="subcode">Subcode of the activity.</param>
        /// <param name="time">Time spent on the activity.</param>
        /// <param name="description">Short description of the activity.</param>
        /// <returns>True for success, false for failure.</returns>
        public bool AddUserActivity(string date, string code, string subcode, int time, string description)
        {
            foreach(UserActivity UA in this.UserMonth.entries)
            {
                if (UA.code == code && UA.date == date && UA.subcode == subcode)
                {
                    return false;
                }
            }
            this.UserMonth.entries.Add(new UserActivity(date, code, subcode, time, description));

            return true;
        }

        /// <summary>Generates a select list out of projects list.</summary>
        /// <returns>Enumerable of select list items containing user names.</returns>
        public IEnumerable<SelectListItem> CreateProjectSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                var pl = this.ProjectsList.Where(a => a.active);
                selectList.AddRange(pl.Select(a => new SelectListItem(a.code, a.code)));
                return selectList;
            }
        }

        /// <summary>Generates a select list out of activity list.</summary>
        /// <returns>Enumerable of select list items containing user names.</returns>
        public IEnumerable<SelectListItem> CreateSubactivitySelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                var pl = this.ProjectsList.Where(p => p.code == this.TempProject);
                if (pl.Count() > 0)
                {
                    selectList.AddRange(pl.First().subactivities.Select(a => new SelectListItem(a.code, a.code)));
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
            this.ProjectsList = Entities.ProjectsDBEntity.Load();
        }

        /// <summary>Load extra data from the database.</summary>
        /// <param name="type">Extra data load. "UserMonth" loads current user month.</param>
        public void LoadExtrasFromDB(string type)
        {
            if (type == "UserMonth")
            {
                this.UserMonth = Entities.UserActivitiesDBEntity.Load(this.User, this.GetMonth());
            }
        }

        /// <summary>Save user activities to the database.</summary>
        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.GetMonth(), this.UserMonth);
        }
    }
}
