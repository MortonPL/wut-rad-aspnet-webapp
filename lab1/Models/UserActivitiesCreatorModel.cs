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

        /// <summary>User's monthly report object.</summary>
        public UserMonth UserMonth;

        /// <summary>List of saved activities.</summary>
        public HashSet<Activity> ActivityList;

        public UserActivitiesCreatorModel()
        {
            this.LoadFromDB("");
        }

        public UserActivitiesCreatorModel(string user, string date)
        {
            this.User = user;
            this.Date = date;
            this.LoadFromDB("user");
        }

        /// <summary>Extract the saved date.</summary>
        /// <returns>Saved date as string in yyyy-MM format</returns>
        public string GetMonth()
        {
            return this.Date.Remove(Date.Length - 3);
        }

        public bool AddUserActivity(string date, string code, string subcode, int time, string description)
        {

            foreach(UserActivity UA in this.UserMonth.entries)
            {
                if (UA.code == code)
                {
                    return false;
                }
            }
            this.UserMonth.entries.Add(new UserActivity(date, code, subcode, time, description));
            return true;
        }

        /// <summary>Generates a select list out of activity list.</summary>
        /// <returns>Enumerable of select list items containing user names.</returns>
        public IEnumerable<SelectListItem> CreateActivitySelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                //var ac = this.ActivityList.Where(a => !a.active);
                //selectList.AddRange(ac.Select(a => new SelectListItem(a.code, a.code)));
                selectList.AddRange(this.ActivityList.Select(a => new SelectListItem(a.code, a.code)));
                return selectList;
            }
        }

        /// <summary>Load user activities from the database.</summary>
        public void LoadFromDB(string mode)
        {
            if (mode == "user")
            {
                this.UserMonth = Entities.UserActivitiesDBEntity.Load(this.User, this.GetMonth());
            }
            this.ActivityList = Entities.ActivitiesDBEntity.Load();
        }

        /// <summary>Save user activities to the database.</summary>
        public void SaveToDB(string mode)
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.GetMonth(), this.UserMonth);
        }
    }
}
