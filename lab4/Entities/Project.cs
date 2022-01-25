namespace lab4.Entities
{
    public class Project
    {
        // PK
        public string ProjectId { get; set; } = "";

        public string Name { get; set; } = "";
        public int Budget { get; set; } = 0;
        public string ManagerName { get; set; } = "";
        public bool Active { get; set; } = true;

        // Parents
        public User? Manager { get; set; }

        // Children
        public virtual ICollection<ApprovedActivity>? ApprovedActivities { get; set; }
        public virtual ICollection<Subactivity>? Subactivities { get; set; }
        public virtual ICollection<UserActivity>? UserActivities { get; set; }
    }
}
