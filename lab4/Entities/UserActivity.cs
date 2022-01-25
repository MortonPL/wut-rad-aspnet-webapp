namespace lab4.Entities
{
    public class UserActivity
    {
        // PK - UserMonth
        public DateTime Month { get; set; } = new DateTime();
        public string UserName { get; set; } = "";
        // PK - Rest
        public int Pid {get; set; } = 0;
        public string ProjectId { get; set; } = "";

        public DateTime Date { get; set; } = new DateTime();
        public string SubactivityId {get; set; } = "";
        public int Time { get; set; } = 0;
        public string? Description { get; set; }

        // Parents
        public virtual UserMonth? UserMonth { get; set; }
        public virtual Subactivity? Subactivity { get; set; }
        public virtual Project? Project { get; set; }
    }
}
