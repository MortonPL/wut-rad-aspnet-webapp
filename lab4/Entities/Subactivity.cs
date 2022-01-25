namespace lab4.Entities
{
    public class SubactivityJson : EntityJson
    {
        public string subactivityId { get; set; } = "";
        public string projectId { get; set; } = "";
    }

    public class Subactivity : IEntity<SubactivityJson>
    {
        // PK
        public string SubactivityId { get; set; } = "";
        public string ProjectId { get; set; } = "";
        
        // Parents
        public virtual Project? Project { get; set; }

        // Children
        public virtual ICollection<UserActivity>? UserActivities { get; set; }

        public bool IsEqualSubactivity(string other)
        {
            return (this.SubactivityId == other) || (String.IsNullOrEmpty(this.SubactivityId) && String.IsNullOrEmpty(other));
        }

        public SubactivityJson toJSON()
        {
            return new SubactivityJson{subactivityId=SubactivityId, projectId=ProjectId};
        }
    }
}
