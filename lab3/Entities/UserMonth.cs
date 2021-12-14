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
        ///<summary>Month and year of the compiled user activity.</summary>
        public string Date { get; set; }

        /// <summary>Name of the user.</summary>
        public string User { get; set; }

        /// <summary>Is this month complete?</summary>
        public bool Frozen { get; set; }

        /// <summary>Array of activity entries.</summary>
        public virtual ICollection<UserActivity> UserActivities { get; set; }

        /// <summary>Has this entry been correctly read?</summary>
        public bool Invalid = false;
        
        public UserMonth(){
        }

        public UserMonth(bool invalid){
            this.Invalid = invalid;
        }
    }
}
