using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single user's monthly activity summary.
    /// </summary>
    public class UserMonth
    {
        ///<summary>Month and year of the compiled user activity.</summary>
        [Key]
        [Column(Order=1)]
        public string Date { get; set; }

        /// <summary>Name of the user.</summary>
        [Key]
        [MaxLength(32),MinLength(4)]
        public string User { get; set; }

        /// <summary>Is this month complete?</summary>
        [Required]
        public bool Frozen { get; set; }

        /// <summary>Array of activity entries.</summary>
        public virtual ICollection<UserActivity> UserActivities { get; set; }

        /// <summary>Has this entry been correctly read?</summary>
        [NotMapped]
        public bool Invalid = false;
        
        public UserMonth(){
            this.Entries = new List<UserActivity>();
            this.Accepted = new List<ApprovedUserActivity>();
        }

        public UserMonth(bool invalid){
            this.Invalid = invalid;
            this.Entries = new List<UserActivity>();
            this.Accepted = new List<ApprovedUserActivity>();
        }
    }
}
