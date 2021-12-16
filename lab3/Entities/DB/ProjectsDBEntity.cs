using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var db = new StorageContext())
            {
                return new HashSet<Project>(db.Projects.Include(p => p.Subactivities).Select(p => p).ToHashSet());
            };
        }

        public static void Create(string projectid, string username, string name, int budget, string subactivities)
        {
            Project project = new Project(projectid, username, name, budget);
            using (var db = new StorageContext())
            {
                db.Projects.Add(project);

                char[] delims = new[] { '\r', '\n' };
                if (subactivities.Length > 0)
                {
                    string[] split = subactivities.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length > 0)
                    {
                        foreach(string s in split)
                        {
                            db.Subactivities.Add(new Subactivity{SubactivityId=s, ProjectId=projectid, Project=project});
                        }
                    }
                }
                db.SaveChanges();
            };
        }
    }
}
