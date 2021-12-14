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
        [Key]
        [MaxLength(16),MinLength(4)]
        public string ProjectID { get; set; }

        /// <summary>Name of the Manager responsible for this project.</summary>
        [Required]
        [MaxLength(32),MinLength(4)]
        public string Manager { get; set; }

        /// <summary>Human-readable name of the project.</summary>
        [Required]
        [MaxLength(64),MinLength(4)]
        public string Name { get; set; }

        /// <summary>Time budget of the project.</summary>
        [Required]
        public int Budget { get; set; }

        /// <summary>Is the project ongoing?</summary>
        [Required]
        public bool Active { get; set; }

        public virtual ICollection<UserActivity> UserActivities { get; set; }

        /// <summary>Array of subactivites (categories of work) for this project.</summary>
        public List<String> Subactivities { get; set; }

        public Project(){}

        public Project(string code, string manager, string name, int budget, string subactivities)
        {
            this.ProjectID = code;
            this.Manager = manager;
            this.Name = name;
            this.Budget = budget;
            this.Active = true;
            this.Subactivities = new List<String>();
            char[] delims = new[] { '\r', '\n' };
            if (subactivities.Length > 0)
            {
                string[] split = subactivities.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length > 0)
                {
                    foreach(string s in split)
                    {
                        this.Subactivities.Add(s);
                    }
                }
            }
        }
    }
}
