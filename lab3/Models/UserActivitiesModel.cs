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
    public class ActivityStats
    {
        public string ProjectId { get; set; }
        public int TotalTime {get; set; }
    }

    public class UserActivitiesModel
    {
        public DateTime Date;
        public string User;
        public UserMonth UserMonth = new UserMonth();
        public bool IsMonthlyView = false;
        public bool IsInvalid = true;
        public List<int> totals = new List<int>();

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
            this.UserMonth = Entities.UserActivitiesDBEntity.Select(this.User, this.Date);
            this.IsInvalid = this.UserMonth.UserActivities.Count <= 0;
        }

        public IEnumerable<ActivityStats> GetProjects()
        {
            IEnumerable<ActivityStats> list = new List<ActivityStats>();
            var query = this.UserMonth.UserActivities.GroupBy(ua => ua.ProjectId, ua => ua.Time, (id, time) => new {Key=id, Total=time.Sum()});
            foreach (var q in query)
            {
                list = list.Append(new ActivityStats{ProjectId=q.Key, TotalTime=q.Total});
            }
            return list;
        }
    }
}
