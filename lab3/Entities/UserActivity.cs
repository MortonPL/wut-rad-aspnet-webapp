using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    public class UserActivity
    {
        // PK - UserMonth
        public DateTime Month { get; set; }
        public string UserName { get; set; }
        // PK - Rest
        public string ProjectId { get; set; }
        public string SubactivityId { get; set; }
        public DateTime Date { get; set; }

        public int Time { get; set; }
        public string Description { get; set; }

        // Parents
        public virtual UserMonth UserMonth { get; set; }
        public virtual Subactivity Subactivity { get; set; }

        public UserActivity(){}

        public UserActivity(DateTime date, string projectId, string subactivityId, int time, string description)
        {
            this.Date = date;
            this.Month = new DateTime(date.Year, date.Month, 1);
            this.ProjectId = projectId;
            this.SubactivityId = subactivityId;
            this.Time = time;
            this.Description = description;
        }
    }
}
