using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NTR.Entities
{
    /// <summary>
    /// A single user's monthly activity summary.
    /// </summary>
    public class UserMonth
    {
        // PK
        public DateTime Month { get; set; }
        public string Username { get; set; }

        public bool Frozen { get; set; }
        public bool Invalid { get; set; }

        // Parents
        public User User { get; set; }

        // Children
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<ApprovedActivity> ApprovedActivities { get; set; }
        
        public UserMonth(){}

        public UserMonth(bool invalid)
        {
            this.Invalid = invalid;
        }
    }
}
