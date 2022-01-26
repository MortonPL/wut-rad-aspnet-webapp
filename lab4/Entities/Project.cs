using lab4.Helpers;

namespace lab4.Entities
{
    public class ProjectJson : EntityJson
    {
        public string projectId { get; set; } = "";
        public string name { get; set; } = "";
        public int budget { get; set; } = 0;
        public string managerName { get; set; } = "";
        public bool active { get; set; } = true;
        public ICollection<SubactivityJson>? subactivities { get; set; }
    }

    public class Project : IEntity<ProjectJson>
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

        public ProjectJson toJSON()
        {
            return new ProjectJson
            {
                projectId=ProjectId,
                name=Name,
                budget=Budget,
                managerName=ManagerName,
                active=Active,
                subactivities= Helper.toJSON<Subactivity, SubactivityJson>(this.Subactivities)
            };
        }
    }
}
