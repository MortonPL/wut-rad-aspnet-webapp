using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    /// <summary>
    /// A model for activities view.
    /// </summary>
    public class ActivitiesModel
    {
        /// <summary>List of all activities.</summary>
        public HashSet<Activity> Activities;

        public ActivitiesModel(){}

        /// <summary>Load activities from the database.</summary>
        public void LoadFromDB()
        {
            this.Activities = Entities.ActivitiesDBEntity.Load();
        }

        /// <summary>Save activities to the database.</summary>
        public void SaveToDB()
        {
            Entities.ActivitiesDBEntity.Save(this.Activities);
        }
    }
}
