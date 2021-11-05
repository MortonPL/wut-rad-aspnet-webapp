using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A helper class inbetween DB and proper list of activities.
    /// </summary>
    public class ActivityList
    {
        ///<summary>A list of all parsed activities.</summary>
        public HashSet<Activity> activities {get; set; }

        public ActivityList(){}

        public ActivityList(HashSet<Activity> activities)
        {
            this.activities = activities;
        }
    }


    /// <summary>
    /// A static class handling IO of all activities database.
    /// </summary>
    public class ActivitiesDBEntity
    {
        /// <summary>
        /// Load all activities from the database.
        /// </summary>
        /// <returns>
        /// List of all activities.
        /// </returns>
        public static HashSet<Activity> Load()
        {
            HashSet<Activity> activities;
            var json = System.IO.File.ReadAllText("db/activity.json");
            try
            {
                var activityList = System.Text.Json.JsonSerializer.Deserialize<ActivityList>(json);
                activities = activityList.activities;
            }
            catch (System.Text.Json.JsonException)
            {
                activities = new HashSet<Activity>();
            }
            return activities;
        }

        /// <summary>
        /// Save all activities to the database.
        /// </summary>
        /// <param name="activities"> List of all activities.</param>
        public static void Save(HashSet<Activity> activities)
        {
            var jsonOptions = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(new ActivityList(activities), jsonOptions);
            System.IO.File.WriteAllBytes("db/activity.json", bytes);
        }
    }
}
