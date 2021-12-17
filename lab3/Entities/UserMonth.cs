using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    public class UserMonth
    {
        // PK
        public DateTime Month { get; set; }
        public string UserName { get; set; }

        public bool Frozen { get; set; }

        // Parents
        public User User { get; set; }

        // Children
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<ApprovedActivity> ApprovedActivities { get; set; }
        
        public UserMonth(){}

        public UserMonth(bool invalid){}
    }
}
