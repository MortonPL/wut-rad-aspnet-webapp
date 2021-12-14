using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single project - project.
    /// </summary>
    public class Project
    {
        /// <summary>Code ID of the project.</summary>
        public string code { get; set; }

        /// <summary>Name of the manager responsible for this project.</summary>
        public string manager { get; set; }

        /// <summary>Human-readable name of the project.</summary>
        public string name { get; set; }

        /// <summary>Time budget of the project.</summary>
        public int budget { get; set; }

        /// <summary>Is the project ongoing?</summary>
        public bool active { get; set; }

        /// <summary>Array of subactivites (categories of work) for this project.</summary>
        public List<SubActivity> subactivities { get; set; }

        public Project(){}

        public Project(string code, string manager, string name, int budget, string subactivities)
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
