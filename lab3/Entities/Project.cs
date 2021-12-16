using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    /// <summary>
    /// A single project.
    /// </summary>
    public class Project
    {
        // PK
        public string ProjectId { get; set; }

        public string Name { get; set; }
        public int Budget { get; set; }
        public string ManagerName { get; set; }
        public bool Active { get; set; }

        // Parents
        public User Manager { get; set; }

        // Children
        public virtual ICollection<ApprovedActivity> ApprovedActivities { get; set; }
        public virtual ICollection<Subactivity> Subactivities { get; set; }

        public Project(){}

        public Project(string projectId, string managerName, string name, int budget)
        {
            this.ProjectId = projectId;
            this.ManagerName = managerName;
            this.Name = name;
            this.Budget = budget;
            this.Active = true;
        }
    }
}
