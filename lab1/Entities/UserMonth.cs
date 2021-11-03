using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single user's monthly activity.
    /// </summary>
    public class UserMonth
    {
        public bool frozen { get; set; }
        public UserActivity[] entries { get; set; }
        public ApprovedUserActivity[] accepted { get; set; }

        public bool invalid = false;
        
        public UserMonth(){}

        public UserMonth(bool invalid){
            this.invalid = invalid;
        }

    }
}
