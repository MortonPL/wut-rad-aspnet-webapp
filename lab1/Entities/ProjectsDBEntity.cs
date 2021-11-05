using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A helper class inbetween DB and proper list of projects.
    /// </summary>
    public class ProjectList
    {
        ///<summary>A list of all parsed projects.</summary>
        public HashSet<Project> activities { get; set; }

        public ProjectList(){}

        public ProjectList(HashSet<Project> projects)
        {
            this.activities = projects;
        }
    }

    /// <summary>
    /// A static class handling IO of all projects database.
    /// </summary>
    public class ProjectsDBEntity
    {
        /// <summary>
        /// Load all projects from the database.
        /// </summary>
        /// <returns>
        /// List of all projects.
        /// </returns>
        public static HashSet<Project> Load()
        {
            HashSet<Project> projects;
            var json = System.IO.File.ReadAllText("db/activity.json");
            try
            {
                projects = System.Text.Json.JsonSerializer.Deserialize<ProjectList>(json).activities;
            }
            catch (System.Text.Json.JsonException)
            {
                projects = new HashSet<Project>();
            }
            return projects;
        }

        /// <summary>
        /// Save all projects to the database.
        /// </summary>
        /// <param name="projects"> List of all projects.</param>
        public static void Save(HashSet<Project> projects)
        {
            var jsonOptions = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(new ProjectList(projects), jsonOptions);
            System.IO.File.WriteAllBytes("db/activity.json", bytes);
        }
    }
}
