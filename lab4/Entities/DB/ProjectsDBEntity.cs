using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using lab4.Helpers;
namespace lab4.Entities
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

        public static Project Select(string projectId)
        {
            using (var db = new StorageContext())
            {
                return db.Projects.Include(p => p.Subactivities).Where(p => p.ProjectId == projectId).First();
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

        public static bool Update(string projectid, string username, string name, int budget, Byte[] timestamp)
        {
            using (var db = new StorageContext())
            {
                try
                {
                    Project project = new Project{
                        ProjectId=projectid, Name=name, Budget=budget, ManagerName=username, Active=true, Timestamp=timestamp};
                    db.Update(project);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
            }
            return true;
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

        public static HashSet<UserMonthT> SelectUserMonths(DateTime date, string projectId)
        {
            using (var db = new StorageContext())
            {
                var ums = db.UserActivities.AsEnumerable()
                    .Where(ua => (ua.ProjectId == projectId && DateTime.Equals(Helper.GetYM(ua.Month), Helper.GetYM(date))))
                    .GroupBy(ua => ua.UserName, ua => ua.Time, (user, time) => new UserMonthT{User=user, Time=time.Sum()})
                    .ToHashSet();
                return ums;
            }
        }
    }
}
