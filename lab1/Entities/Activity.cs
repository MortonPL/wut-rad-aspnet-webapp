using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single activity - project.
    /// </summary>
    public class Activity
    {
        public string code { get; set; }
        public string manager { get; set; }
        public string name { get; set; }
        public int budget { get; set; }
        public bool active { get; set; }

        public SubActivity[] subactivities { get; set; }

        public Activity(){}

    }
}
