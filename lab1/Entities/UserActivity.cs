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
        public string date { get; set; }
        public string code { get; set; }
        public string subcode { get; set; }
        public int time { get; set; }
        public string description { get; set; }

        public UserActivity(){}

    }
}
