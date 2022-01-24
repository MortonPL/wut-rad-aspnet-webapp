using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using lab4.Entities;

namespace lab4.Entities
{
    public class UserMonthT
    {
        public string User { get; set; }
        public int Time {get; set; }
    }
}

namespace lab4.Models
{
    public class ProjectInspectorModel
    {
        public string User = "";
        public DateTime Date;
        public string ProjectId;
        public HashSet<UserMonthT> UserMonths = new HashSet<UserMonthT>();

        public ProjectInspectorModel()
        {
            this.Date = DateTime.Today;
        }

        public string GetMonth()
        {
            return this.Date.ToString("MMMM yyyy");
        }

        public void LoadFromDB()
        {
            this.UserMonths = Entities.ProjectsDBEntity.SelectUserMonths(this.Date, this.ProjectId);
        }

        public void LoadForEdit()
        {
        }
    }
}
