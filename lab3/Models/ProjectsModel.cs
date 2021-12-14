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

        /// <summary>Checks if the current user can close a project and closes it.</summary>
        /// <param name="code">Code of the project.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool CloseProject(string code)
        {
            foreach(var project in this.Projects)
            {
                if (project.code == code && project.manager == this.User)
                {
                    project.active = false;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Save projects to the database.</summary>
        public void SaveToDB()
        {
            Entities.ProjectsDBEntity.Save(this.Projects);
        }
    }
}
