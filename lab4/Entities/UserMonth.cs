using lab4.Helpers;

namespace lab4.Entities
{
    public class UserMonthJson : EntityJson
    {
        public DateTime month { get; set; } = new DateTime();
        public string userName { get; set; } = "";
        public bool frozen { get; set; } = false;
        public ICollection<UserActivityJson>? userActivities { get; set; }
        public ICollection<ApprovedActivityJson>? approvedActivities { get; set; }
    }

    public class UserMonthT
    {
        public string? User { get; set; }
        public int? Time {get; set; }
    }

    public class UserMonth : IEntity<UserMonthJson>
    {
        // PK
        public DateTime Month { get; set; } = new DateTime();
        public string UserName { get; set; } = "";

        public bool Frozen { get; set; } = false;

        // Parents
        public User? User { get; set; }

        // Children
        public virtual ICollection<UserActivity>? UserActivities { get; set; }
        public virtual ICollection<ApprovedActivity>? ApprovedActivities { get; set; }

        public UserMonth()
        {
            this.UserActivities = new List<UserActivity>();
            this.ApprovedActivities = new List<ApprovedActivity>();
        }

        public UserMonthJson toJSON()
        {
            return new UserMonthJson
            {
                month=Month,
                userName=UserName,
                frozen=Frozen,
                userActivities= Helper.toJSON<UserActivity, UserActivityJson>(this.UserActivities),
                approvedActivities= Helper.toJSON<ApprovedActivity, ApprovedActivityJson>(this.ApprovedActivities),
            };
        }
    }
}
