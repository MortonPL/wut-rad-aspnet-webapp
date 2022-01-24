using System;
using System.Collections.Generic;

namespace lab4.Entities
{
    public class ApprovedActivity
    {
        // PK
        public DateTime Month { get; set; }
        public string UserName { get; set; }
        public string ProjectId { get; set; }

        public int Time { get; set; }
        
        // Parents
        public virtual Project Project { get; set; }
        public virtual UserMonth UserMonth { get; set; }
    }
}
