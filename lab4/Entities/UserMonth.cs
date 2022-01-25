namespace lab4.Entities
{
    public class UserMonthT
    {
        public string? User { get; set; }
        public int? Time {get; set; }
    }

    public class UserMonth
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
        }

        public UserMonth(bool invalid)
        {
            this.UserActivities = new List<UserActivity>();
        }
    }
}
