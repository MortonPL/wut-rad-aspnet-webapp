using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;
using NTR.Helpers;

namespace NTR.Models
{
    public class UserActivitiesCreatorModel
    {
        public string User = "";
        public DateTime Date;
        public string Error;
        public string TempProject;
        public string TempSubactivity;
        public UserMonth UserMonth = new UserMonth();
        public HashSet<Project> ProjectsList;

        public UserActivitiesCreatorModel()
        {
            this.LoadFromDB();
        }

        public UserActivitiesCreatorModel(string user, string date)
        {
            this.User = user;
            this.Date = DateTime.Parse(date, new CultureInfo("en-US"));
            LoadMoreFromDB();
        }

        public void AddUserActivity(string date, string projectId, string subactivityId, int time, string description)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("en-US"));
            this.Error = Entities.UserActivitiesDBEntity.Insert(parsedDate, this.User, projectId, subactivityId, time, description);
            return;

        }

        public bool EditUserActivity(string date, string projectId, string subactivity, int time, string description)
        {
            DateTime parsedDate = DateTime.Parse(date, new CultureInfo("en-US"));
            foreach(UserActivity UA in this.UserMonth.UserActivities)
            {
                //if (UA.ProjectId == projectId && DateTime.Equals(UA.Date, parsedDate) && UA.IsEqualSubactivity(subactivity))
                {
                    UA.Time = time;
                    UA.Description = description;
                    return true;
                }
            }
            return false;
        }

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

        public IEnumerable<SelectListItem> CreateSubactivitySelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                var pl = this.ProjectsList.Where(p => p.ProjectId == this.TempProject);
                if (pl.Count() > 0)
                {
                    selectList.AddRange(pl.First().Subactivities.Select(sa => new SelectListItem(sa.SubactivityId, sa.SubactivityId)));
                }
                else
                {
                    selectList.Add(new SelectListItem("", ""));
                }
                return selectList;
            }
        }

        public void LoadFromDB()
        {
            this.ProjectsList = Entities.ProjectsDBEntity.Select();
        }

        public void LoadMoreFromDB()
        {
            this.UserMonth = Entities.UserActivitiesDBEntity.Select(this.User, this.Date);
        }

        public void SaveToDB()
        {
            Entities.UserActivitiesDBEntity.Save(this.User, this.Date, this.UserMonth);
        }
    }
}
