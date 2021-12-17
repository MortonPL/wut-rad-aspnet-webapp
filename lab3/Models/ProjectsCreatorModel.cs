using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    public class ProjectsCreatorModel
    {
        public string User = "";
        public HashSet<Project> Projects;

        public ProjectsCreatorModel(){}

        public ProjectsCreatorModel(string user)
        {
            this.User = user;
        }

        public bool AddProject(string projectId, string name, int budget, string subactivities)
        {
            foreach(Project P in this.Projects)
            {
                if (P.ProjectId == projectId)
                {
                    return false;
                }
            }
            Entities.ProjectsDBEntity.Insert(projectId, User, name, budget, subactivities);
            return true;
        }

        public void LoadFromDB()
        {
            this.Projects = Entities.ProjectsDBEntity.Select();
        }
    }
}
