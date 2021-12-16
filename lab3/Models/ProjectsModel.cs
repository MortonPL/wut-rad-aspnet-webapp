using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;
using NTR.Helpers;

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
            this.Projects = Entities.ProjectsDBEntity.Select();
        }

        public void CloseProject(string projectid)
        {
            Entities.ProjectsDBEntity.Close(projectid, this.User);
        }
    }
}
