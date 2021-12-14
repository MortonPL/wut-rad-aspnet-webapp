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
        ///<summary>Date of the registered user activity.</summary>
        public string Date { get; set; }

        ///<summary>Date of the approval.</summary>
        public string ApprovalDate { get; set; }

        /// <summary>Code ID of the activity/project that this user contributed to.</summary>
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        /// <summary>Subactivity that this user contributed to.</summary>
        public string Subactivity { get; set; }

        /// <summary>Amount of time that this user spent on activity, in minutes.</summary>
        public int Time { get; set; }

        /// <summary>Approved amount of time spent.</summary>
        public int ApprovedTime { get; set; }

        /// <summary>Description of what the user did.</summary>
        public string Description { get; set; }

        /// <summary>Code ID of the user month.</summary>
        public string UserMonthId { get; set; }
        public virtual UserMonth UserMonth { get; set; }

        public UserActivity(){}

        public UserActivity(string date, string code, string subactivity, int time, string description)
        {
            this.Date = date;
            this.ProjectId = code;
            this.Subactivity = subactivity;
            this.Time = time;
            this.Description = description;
        }

        /// <summary>Compares own subactivity with provided one.</summary>
        /// <param name="subactivity">Subactivity to compare with.</name>
        /// <return>True if equal, false otherwise.</return>
        public bool IsEqualSubactivity(string subactivity)
        {
            return (this.Subactivity == subactivity) || (String.IsNullOrEmpty(this.Subactivity) && String.IsNullOrEmpty(subactivity));
        }
    }
}
