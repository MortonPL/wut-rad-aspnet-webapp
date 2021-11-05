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
    public class ProjectInspectorModel
    {
        /// <summary>List of all users.</summary>
        public HashSet<String> Users;

        /// <summary>Name of the user.</summary>
        public string User = "";

        public ProjectInspectorModel(){}

        /// <summary>Load projects from the database.</summary>
        public void LoadFromDB()
        {
            //Entities.ProjectsDBEntity.Load();
        }

        /// <summary>Save projects to the database.</summary>
        public void SaveToDB()
        {
            //Entities.ProjectsDBEntity.Save();
        }
    }
}
