using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single user activity - with approval from the manager.
    /// </summary>
    public class ApprovedUserActivity
    {
        /// <summary>Code ID of the activity.</summary>
        public string code { get; set; }

        /// <summary>Time budget of the activity, approved by the manager.</summary>
        public int time { get; set; }
        
        public ApprovedUserActivity(){}

    }
}
