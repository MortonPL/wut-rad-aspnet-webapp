using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    /// <summary>
    /// A model for user activities view.
    /// </summary>
    public class UserActivitiesModel
    {
        /// <summary>Date entered by the user.</summary>
        public DateTime Date;

        /// <summary>Logged user's name.</summary>
        public string User;

        /// <summary>User's monthly report object.</summary>
        public UserMonth UserMonth;

        /// <summary>Is the activity view per day or per month?</summary>
        public bool IsMonthlyView = false;

        public UserActivitiesModel(){
            this.Date = DateTime.Today;
        }
        
        /// <summary>Extract the saved date.</summary>
        /// <returns>Saved date as string in yyyy-MM format</returns>
        public DateTime GetMonth()
        {
            return new DateTime(this.Date.Year, this.Date.Month, 1);
        }

        /// <summary>Get set date's activties for saved user.</summary>
        /// <returns>Filtered list of user activities.</returns>
        public IEnumerable<UserActivity> GetActivities()
        {
            IEnumerable<UserActivity> list = this.IsMonthlyView
                ? this.UserMonth.UserActivities.Where(ua => DateTime.Equals(new DateTime(ua.Date.Year, ua.Date.Month, 1), this.GetMonth()))
                : this.UserMonth.UserActivities.Where(ua => DateTime.Equals(ua.Date, this.Date));
            return list.OrderBy(ua => ua.Date).ToList();
        }

        /// <summary>Checks if the current user can delete the UA and deletes it.</summary>
        /// <param name="code">Code of the project.</param>
        /// <param name="date">Date of the project.</param>
        /// <param name="subcode">Subcode of the project.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DeleteUserActivity(string code, string date, string subcode)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("en-US"));
            foreach(UserActivity UA in this.UserMonth.UserActivities)
            {
                if (UA.ProjectId == code && DateTime.Equals(UA.Date, parsedDate) && UA.IsEqualSubactivity(subcode))
                {
                    this.UserMonth.UserActivities.Remove(UA);
                    return true;
                }
            }
            return false;
        }

        /// <summary>Freezes the month.</summary>
        public void LockUserActivity()
        {
            this.UserMonth.Frozen = true;
        }

        /// <summary>Load user activities from the database.</summary>
        public void LoadFromDB()
        {
            this.UserMonth = Entities.UserActivitiesDBEntity.Load(this.User, this.GetMonth().ToString("YYYY-MM"));
        }

        /// <summary>Save user activities to the database.</summary>
        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.GetMonth().ToString("YYYY-MM"), this.UserMonth);
        }
    }
}
