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
        public string tempProject;
        public string tempSubs;
        public Project Project;

        public ProjectsCreatorModel(){}

        public ProjectsCreatorModel(string user)
        {
            this.User = user;
        }

        public ProjectsCreatorModel(string user, string projectid)
        {
            this.User = user;
            this.tempProject = projectid;
            LoadFromDB(projectid);
            this.tempSubs = String.Concat(this.Project.Subactivities.SelectMany(s => s.SubactivityId.Append('\n')));
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
            Entities.ProjectsDBEntity.Insert(projectId, this.User, name, budget, subactivities);
            return true;
        }

        public bool EditProject(string projectId, string name, int budget)
        {
            return Entities.ProjectsDBEntity.Update(projectId, this.User, name, budget);
        }

        public void LoadFromDB()
        {
            this.Projects = Entities.ProjectsDBEntity.Select();
        }

        public void LoadFromDB(string projectid)
        {
            this.Project = Entities.ProjectsDBEntity.Select(projectid);
        }
    }
}
