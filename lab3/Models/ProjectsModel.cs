using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;
using NTR.Helpers;

namespace NTR.Models
{
    public class ProjectsModel
    {
        public HashSet<Project> Projects;
        public string User = "";

        public ProjectsModel(){}

        public void LoadFromDB()
        {
            this.Projects = Entities.ProjectsDBEntity.Select();
        }

        public void CloseProject(string projectid)
        {
            Entities.ProjectsDBEntity.Close(projectid, this.User);
        }

        public int CalcBudget(Project project)
        {
            return project.Budget - Entities.ProjectsDBEntity.SpecialBudget(project);
        }
    }
}
