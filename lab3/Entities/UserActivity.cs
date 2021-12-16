using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NTR.Entities
{
    /// <summary>
    /// A single user activity.
    /// </summary>
    public class UserActivity
    {
        // PK - UserMonth
        public DateTime Month { get; set; }
        public string Username { get; set; }
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
    }
}
