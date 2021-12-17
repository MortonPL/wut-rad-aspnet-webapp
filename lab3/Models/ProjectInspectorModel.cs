using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    public class ProjectInspectorModel
    {
        public HashSet<String> Users;
        public string User = "";

        public ProjectInspectorModel(){}

        public void LoadFromDB()
        {
            Entities.ProjectsDBEntity.Select();
        }
    }
}
