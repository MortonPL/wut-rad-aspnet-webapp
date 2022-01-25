using Microsoft.EntityFrameworkCore;

namespace lab4.Entities
{
    public class PopulateDBEntity
    {
        private int _pid = 0;
        protected int pid {
            get {_pid += 1; return _pid;}
            set {if (value > _pid) _pid = value;}
        }

        public void Populate(StorageContext db)
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

            var projects = new List<Project>
            {
                new Project{ProjectId="NTR", Name="Narzędzia Typu RAD", Budget=60, ManagerName="John Fighter", Active=true},
                new Project{ProjectId="KOMPOT", Name="Kompot", Budget=120, ManagerName="Balbinka", Active=true},
                new Project{ProjectId="ARGUS", Name="Stary Argus-123", Budget=20, ManagerName="Jacob Hoe", Active=false}
            };
            Add<Project>(db, db.Projects, projects);

            var subs = new List<Subactivity>
            {
                new Subactivity{SubactivityId="Projekt", ProjectId="NTR"},
                new Subactivity{SubactivityId="Kolokwium", ProjectId="NTR"},
                new Subactivity{SubactivityId="Warzenie kompotu", ProjectId="KOMPOT"},
                new Subactivity{SubactivityId="Rozlewanie kompotu", ProjectId="KOMPOT"},
                new Subactivity{SubactivityId="Smakowanie kompotu", ProjectId="KOMPOT"}
            };
            Add<Subactivity>(db, db.Subactivities, subs);

            var umonths = new List<UserMonth>
            {
                new UserMonth{Month=new DateTime(2022, 1, 25), UserName="Balbinka", Frozen=false},
                new UserMonth{Month=new DateTime(2022, 1, 24), UserName="John Fighter", Frozen=false},
            };
            Add<UserMonth>(db, db.UserMonths, umonths);

            var uas = new List<UserActivity>
            {
                new UserActivity{Month=umonths[0].Month, UserName=umonths[0].UserName, Pid=pid, ProjectId="KOMPOT",
                Date=new DateTime(2022, 1, 25, 20, 30, 1), SubactivityId=subs[2].SubactivityId, Time=10, Description="Mieszanie"},
                new UserActivity{Month=umonths[0].Month, UserName=umonths[0].UserName, Pid=pid, ProjectId="KOMPOT",
                Date=new DateTime(2022, 1, 25, 20, 40, 1), SubactivityId=subs[2].SubactivityId, Time=15, Description="Dodawanie cukru"},
                new UserActivity{Month=umonths[0].Month, UserName=umonths[0].UserName, Pid=pid, ProjectId="KOMPOT",
                Date=new DateTime(2022, 1, 25, 21, 00, 1), SubactivityId=subs[3].SubactivityId, Time=20, Description="Do słoików siup!"},

                new UserActivity{Month=umonths[1].Month, UserName=umonths[1].UserName, Pid=pid, ProjectId="NTR",
                Date=new DateTime(2022, 1, 24, 18, 15, 1), SubactivityId=subs[0].SubactivityId, Time=50, Description="Klepanie projektu"},
                new UserActivity{Month=umonths[1].Month, UserName=umonths[1].UserName, Pid=pid, ProjectId="NTR",
                Date=new DateTime(2022, 1, 25, 23, 23, 1), SubactivityId=subs[0].SubactivityId, Time=36, Description="Klepanie projektu"},
            };
            Add<UserActivity>(db, db.UserActivities, uas);

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
