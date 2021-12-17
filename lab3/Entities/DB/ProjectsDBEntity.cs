using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using NTR.Helpers;
namespace NTR.Entities
{
    public class ProjectsDBEntity
    {
        public static HashSet<Project> Select()
        {
            using (var db = new StorageContext())
            {
                return new HashSet<Project>(db.Projects.Include(p => p.Subactivities).Select(p => p).ToHashSet());
            };
        }

        public static void Insert(string projectid, string username, string name, int budget, string subactivities)
        {
            subactivities = subactivities != null ? subactivities : "";
            Project project = new Project(projectid, username, name, budget);
            using (var db = new StorageContext())
            {
                db.Projects.Add(project);

                char[] delims = new[] { '\r', '\n' };
                if (subactivities.Length > 0)
                {
                    List<string> split = subactivities.Split(delims, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach(string s in split.Prepend(""))
                    {
                        db.Subactivities.Add(new Subactivity{SubactivityId=s, ProjectId=projectid, Project=project});
                    }
                }
                db.SaveChanges();
            };
        }

        public static void Close(string projectid, string manager)
        {
            using (var db = new StorageContext())
            {
                Project project = db.Projects.Where(p => p.ProjectId == projectid && p.ManagerName == manager).Select(p => p).First();
                project.Active = false;
                db.SaveChanges();
            };
        }

        public static int SpecialBudget(Project project)
        {
            using (var db = new StorageContext())
            {
                var uas = db.UserActivities
                    .GroupBy(ua => ua.ProjectId, ua => ua.Time, (id, time) => new {Id=id, Total=time.Sum()})
                    .Where(q => q.Id == project.ProjectId);
                if (uas.Count() > 0)
                {
                return uas.Select(q => q.Total).First();
                }
                return 0;
            }
        }
    }
}
