using Microsoft.EntityFrameworkCore;

namespace lab4.Entities
{
    public class PopulateDBEntity
    {
        public static void Populate(StorageContext db)
        {
            db.Database.EnsureCreated();

            if (IsDatabaseCreated(db)) return;
            Console.WriteLine("[INFO] Empty database detected, proceeding to populate...");

            var users = new HashSet<User>
            {
                new User("Balbinka"),
                new User("John Fighter"),
                new User("Jacob Hoe"),
                new User("Jacob Birder")
            };
            Add<User>(db, db.Users, users);

            var projects = new HashSet<Project>
            {
                new Project{ProjectId="NTR", Name="Narzędzia Typu RAD", Budget=60, ManagerName="John Fighter", Active=true},
                new Project{ProjectId="KOMPOT", Name="Kompot", Budget=120, ManagerName="Balbinka", Active=true},
                new Project{ProjectId="ARGUS", Name="Stary Argus-123", Budget=20, ManagerName="Jacob Hoe", Active=false}
            };
            Add<Project>(db, db.Projects, projects);

            var subs = new HashSet<Subactivity>
            {
                new Subactivity{SubactivityId="Projekt", ProjectId="NTR"},
                new Subactivity{SubactivityId="Kolokwium", ProjectId="NTR"},
                new Subactivity{SubactivityId="Warzenie kompotu", ProjectId="KOMPOT"},
                new Subactivity{SubactivityId="Rozlewanie kompotu", ProjectId="KOMPOT"},
                new Subactivity{SubactivityId="Smakowanie kompotu", ProjectId="KOMPOT"}
            };
            Add<Subactivity>(db, db.Subactivities, subs);

            var umonths = new HashSet<UserMonth>
            {
                new UserMonth{Month=new DateTime(2022, 1, 25), UserName="Balbinka", Frozen=false},
            };
            Add<UserMonth>(db, db.UserMonths, umonths);

            Console.WriteLine("[INFO] Database population complete.");
        }

        public static bool IsDatabaseCreated(StorageContext db)
        {
            return db.Projects.Any() || db.UserMonths.Any() ||
                   db.UserActivities.Any() || db.ApprovedActivities.Any() ||
                   db.Users.Any() || db.Subactivities.Any();
        }

        public static void Add<T>(StorageContext db, DbSet<T> dbSet, ICollection<T> collection)
        where T : class
        {
            foreach (var e in collection)
                dbSet.Add(e);
            db.SaveChanges();
        }
    }
}
