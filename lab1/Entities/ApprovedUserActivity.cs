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
        public string code { get; set; }
        public int time { get; set; }
        
        public ApprovedUserActivity(){}

    }
}
