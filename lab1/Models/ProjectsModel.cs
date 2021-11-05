using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    /// <summary>
    /// A model for projects view.
    /// </summary>
    public class ProjectsModel
    {
        /// <summary>List of all projects.</summary>
        public HashSet<Project> Projects;

        /// <summary>Name of the user.</summary>
        public string User = "";

        public ProjectsModel(){}

        /// <summary>Load projects from the database.</summary>
        public void LoadFromDB()
        {
            this.Projects = Entities.ProjectsDBEntity.Load();
        }

        /// <summary>Save projects to the database.</summary>
        public void SaveToDB()
        {
            Entities.ProjectsDBEntity.Save(this.Projects);
        }
    }
}
