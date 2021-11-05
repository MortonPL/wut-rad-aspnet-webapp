using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    /// <summary>
    /// A model for activities creator view.
    /// </summary>
    public class ActivitiesCreatorModel: AbstractUserModel
    {
        /// <summary>Name of the user.</summary>
        public string NewCode;

        /// <summary>Name of the user.</summary>
        public string NewName;

        /// <summary>Name of the user.</summary>
        public int NewBudget;

        /// <summary>Name of the user.</summary>
        public string[] NewSubcategories;

        /// <summary>Name of the user.</summary>
        public string User = "";

        /// <summary>List of all activities.</summary>
        public HashSet<Activity> Activities;

        public ActivitiesCreatorModel(){}

        public ActivitiesCreatorModel(string user)
        {
            this.User = user;
        }

        public bool AddActivity(string code, string name, int time, string sub)
        {

            foreach(Activity A in this.Activities)
            {
                if (A.code == code)
                {
                    return false;
                }
            }
            Activities.Add(new Activity(code, User, name, time, sub));
            return true;
        }

        /// <summary>Load activities from the database.</summary>
        public override void LoadFromDB()
        {
            this.Activities = Entities.ActivitiesDBEntity.Load();
        }

        /// <summary>Save activities to the database.</summary>
        public override void SaveToDB()
        {
            Entities.ActivitiesDBEntity.Save(this.Activities);
        }
    }
}
