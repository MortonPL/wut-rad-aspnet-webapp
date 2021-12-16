using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using NTR.Helpers;

namespace NTR.Entities
{
    /// <summary>
    /// A class handling IO of each user's monthly activities database.
    /// </summary>
    public class UserActivitiesDBEntity
    {
        public static UserMonth Load(string name, DateTime date)
        {
            using (var db = new StorageContext())
            {
                HashSet<UserMonth> usermonth = db.UserMonths
                    .Include(um => um.UserActivities)
                    .Where(um => (um.UserName == name && DateTime.Equals(um.Month, Helper.GetYM(date))))
                    .ToHashSet();

                return usermonth.First();
            }
        }

        public static void Save(string name, DateTime date, UserMonth usermonth)
        {
        }
    }
}
