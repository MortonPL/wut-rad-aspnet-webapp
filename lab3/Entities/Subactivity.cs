using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    /// <summary>
    /// A single subactivity.
    /// </summary>
    public class Subactivity
    {
        // PK
        public string SubactivityId { get; set; }
        public string ProjectId { get; set; }
        
        // Parents
        public virtual Project Project { get; set; }

        // Children
        public virtual ICollection<UserActivity> UserActivities { get; set; }

        public bool IsEqualSubactivity(string other)
        {
            return (this.SubactivityId == other) || (String.IsNullOrEmpty(this.SubactivityId) && String.IsNullOrEmpty(other));
        }
    }
}
