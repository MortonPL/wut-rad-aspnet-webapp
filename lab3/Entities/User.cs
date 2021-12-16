using System;
using System.Collections.Generic;

namespace NTR.Entities
{
    public class User
    {
        // PK
        public String Name { get; set; }

        // Children
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<UserMonth> UserMonths { get; set; }
    }
}
