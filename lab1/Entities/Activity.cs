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
        /// <summary>Code ID of the activity.</summary>
        public string code { get; set; }

        /// <summary>Name of the manager responsible for this activity.</summary>
        public string manager { get; set; }

        /// <summary>Human-readable name of the activity.</summary>
        public string name { get; set; }

        /// <summary>Time budget of the activity.</summary>
        public int budget { get; set; }

        /// <summary>Is the activity ongoing?</summary>
        public bool active { get; set; }

        /// <summary>Array of subactivites (categories of work) for this activity.</summary>
        public List<SubActivity> subactivities { get; set; }

        public Activity(){}

        public Activity(string code, string manager, string name, int budget, string subactivities)
        {
            this.code = code;
            this.manager = manager;
            this.name = name;
            this.budget = budget;
            this.active = true;
            this.subactivities = new List<SubActivity>();
            char[] delims = new[] { '\r', '\n' };
            if (subactivities.Length > 0)
            {
                string[] split = subactivities.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0)
                {
                    foreach(string s in split)
                    {
                        this.subactivities.Add(new SubActivity(s));
                    }
                }
            }
        }
    }
}
