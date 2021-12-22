using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    public class UserActivity
    {
        // PK - UserMonth
        public DateTime Month { get; set; }
        public string UserName { get; set; }
        // PK - Rest
        public int Pid {get; set; }
        public string ProjectId { get; set; }

        public DateTime Date { get; set; }
        public string SubactivityId {get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public Byte[] Timestamp { get; set; }

        // Parents
        public virtual UserMonth UserMonth { get; set; }
        public virtual Subactivity Subactivity { get; set; }
        public virtual Project Project { get; set; }

        public UserActivity(){}
    }
}
