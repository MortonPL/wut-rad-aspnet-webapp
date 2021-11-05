using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single user activity.
    /// </summary>
    public class UserActivity
    {
        ///<summary>Date of the registered user activity.</summary>
        public string date { get; set; }

        /// <summary>Code ID of the activity/project that this user contributed to.</summary>
        public string code { get; set; }

        /// <summary>Code ID of the subactivity that this user contributed to.</summary>
        public string subcode { get; set; }

        /// <summary>Amount of time that this user spent on activity, in minutes.</summary>
        public int time { get; set; }

        /// <summary>Description of what the user did.</summary>
        public string description { get; set; }

        public UserActivity(){}

        public UserActivity(string date, string code, string subcode, int time, string description)
        {
            this.date = date;
            this.code = code;
            this.subcode = subcode;
            this.time = time;
            this.description = description;
        }

        /// <summary>Compares own subactivity with provided one.</summary>
        /// <param name="subcode">Subactivity to compare with.</name>
        /// <return>True if equal, false otherwise.</return>
        public bool IsEqualSubactivity(string subcode)
        {
            return (this.subcode == subcode) || (String.IsNullOrEmpty(this.subcode) && String.IsNullOrEmpty(subcode));
        }
    }
}
