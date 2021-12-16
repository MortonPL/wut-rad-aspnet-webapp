using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using NTR.Entities;
using NTR.Helpers;

namespace NTR.Models
{
    /// <summary>
    /// A model for user activities view.
    /// </summary>
    public class UserActivitiesModel
    {
        public DateTime Date;
        public string User;
        public UserMonth UserMonth;
        public bool IsMonthlyView = false;
        public bool IsInvalid = false;

        public UserActivitiesModel(){
            this.Date = DateTime.Today;
        }

        public string GetMonth()
        {
            return this.Date.ToString("MMMM yyyy");
        }

        public IEnumerable<UserActivity> GetActivities()
        {
            IEnumerable<UserActivity> list = this.IsMonthlyView
                ? this.UserMonth.UserActivities.Where(ua => DateTime.Equals(Helper.GetYM(ua.Date), Helper.GetYM(this.Date)))
                : this.UserMonth.UserActivities.Where(ua => DateTime.Equals(Helper.GetYMD(ua.Date), Helper.GetYMD(this.Date)));
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
            using (var db = new StorageContext())
            {             
                HashSet<UserMonth> usermonths = db.UserMonths
                    .Include(um => um.UserActivities)
                    .ThenInclude(ua => ua.Subactivity)
                    .Where(um => (um.UserName == this.User && DateTime.Equals(um.Month, Helper.GetYM(this.Date))))
                    .ToHashSet();
                this.IsInvalid = usermonths.Count == 0;
                this.UserMonth = usermonths.First();
            }
        }

        /// <summary>Save user activities to the database.</summary>
        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.Date, this.UserMonth);
        }
    }
}
