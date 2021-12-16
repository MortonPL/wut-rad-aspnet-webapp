using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            }
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
