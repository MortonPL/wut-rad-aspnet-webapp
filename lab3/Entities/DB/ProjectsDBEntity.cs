using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NTR.Entities
{
    /// <summary>
    /// A static class handling IO of all projects database.
    /// </summary>
    public class ProjectsDBEntity
    {
        public static HashSet<Project> Load()
        {
            HashSet<Project> projects = new HashSet<Project>();
            using (var db = new StorageContext()) {
                projects = db.Projects.Include(p => p.Subactivities).Select(p => p).ToHashSet();
            };
            //Debugger.Log(projects.Select(p => p.ProjectId.Subactivities));
            return projects;
        }

        /// <summary>
        /// Save all projects to the database.
        /// </summary>
        /// <param name="projects"> List of all projects.</param>
        public static void Save(HashSet<Project> projects)
        {
        }
    }
}
