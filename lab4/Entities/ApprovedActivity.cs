using System;
using System.Collections.Generic;

namespace lab4.Entities
{
    public class ApprovedActivityStatsJson : EntityJson
    {
        public string projectId { get; set; } = "";
        public int time { get; set; }
    }

    public class ApprovedActivityJson : EntityJson
    {
        public DateTime month { get; set; } = new DateTime();
        public string userName { get; set; } = "";
        public string projectId { get; set; } = "";
        public int time { get; set; }
    }

    public class ApprovedActivity : IEntity<ApprovedActivityJson>
    {
        // PK
        public DateTime Month { get; set; } = new DateTime();
        public string UserName { get; set; } = "";
        public string ProjectId { get; set; } = "";

        public int Time { get; set; }
        
        // Parents
        public virtual Project? Project { get; set; }
        public virtual UserMonth? UserMonth { get; set; }

        public ApprovedActivityJson toJSON()
        {
            return new ApprovedActivityJson
            {
                month=Month,
                userName=UserName,
                projectId=ProjectId,
                time=Time
            };
        }
    }
}
