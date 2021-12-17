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
    public class UserActivitiesModel
    {
        public DateTime Date;
        public string User;
        public UserMonth UserMonth = new UserMonth();
        public bool IsMonthlyView = false;
        public bool IsInvalid = true;

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
                ? this.UserMonth.UserActivities.Where(ua => Helper.EqualsYM(ua.Date, this.Date))
                : this.UserMonth.UserActivities.Where(ua => Helper.EqualsYMD(ua.Date, this.Date));
            return list.OrderBy(ua => ua.Date).ToList();
        }

        public void DeleteUserActivity(string projectId, string date, string subactivityId)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("pl-pl"));
            Entities.UserActivitiesDBEntity.Delete(UserMonth.UserName, projectId, parsedDate, subactivityId);
        }

        public void LockUserActivity()
        {
            if (this.UserMonth.UserActivities != null)
            {
                Entities.UserActivitiesDBEntity.Lock(UserMonth);
            }
        }

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
                if (!this.IsInvalid)
                {
                    this.UserMonth = usermonths.First();
                }
            }
        }

        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.Date, this.UserMonth);
        }
    }
}
