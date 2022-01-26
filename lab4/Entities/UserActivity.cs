namespace lab4.Entities
{
    public class UserActivityJson : EntityJson
    {
        public DateTime month { get; set; } = new DateTime();
        public string userName { get; set; } = "";
        public int pid {get; set; } = 0;
        public string projectId { get; set; } = "";
        public DateTime date { get; set; } = new DateTime();
        public string subactivityId {get; set; } = "";
        public int time { get; set; } = 0;
        public string? description { get; set; }

    }
    public class UserActivity : IEntity<UserActivityJson>
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

        public UserActivityJson toJSON()
        {
            return new UserActivityJson
            {
                month=Month,
                userName=UserName,
                pid=Pid,
                projectId=ProjectId,
                date=Date,
                subactivityId=SubactivityId,
                time=Time,
                description=Description
            };
        }
    }
}
