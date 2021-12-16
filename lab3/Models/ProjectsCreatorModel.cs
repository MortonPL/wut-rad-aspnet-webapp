using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    /// <summary>
    /// A model for projects creator view.
    /// </summary>
    public class ProjectsCreatorModel
    {
        /// <summary>Name of the user.</summary>
        public string User = "";

        /// <summary>List of all projects.</summary>
        public HashSet<Project> Projects;

        public ProjectsCreatorModel(){}

        public ProjectsCreatorModel(string user)
        {
            this.User = user;
        }

        public bool AddProject(string code, string name, int time, string sub)
        {
            foreach(Project P in this.Projects)
            {
                if (P.ProjectId == code)
                {
                    return false;
                }
            }
            Entities.ProjectsDBEntity.Insert(code, User, name, time, sub);

            return true;
        }

        public void LoadFromDB()
        {
            this.Projects = Entities.ProjectsDBEntity.Select();
        }
    }
}
