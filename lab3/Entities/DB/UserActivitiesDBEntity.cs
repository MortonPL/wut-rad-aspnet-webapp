using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using NTR.Helpers;

namespace NTR.Entities
{
    public class UserActivitiesDBEntity
    {
        public static UserMonth Select(string name, DateTime date)
        {
            using (var db = new StorageContext())
            {
                HashSet<UserMonth> usermonth = db.UserMonths
                    .Include(um => um.UserActivities)
                    .Where(um => (um.UserName == name && Helper.EqualsYM(um.Month, date)))
                    .ToHashSet();

                return usermonth.First();
            }
        }

        public static void Save(string name, DateTime date, UserMonth usermonth)
        {
        }

        public static void Lock(UserMonth userMonth)
        {
            using (var db = new StorageContext())
            {
                db.Update(userMonth);
                userMonth.Frozen = true;
                db.SaveChanges();
            }
        }

        public static void Delete(string user, string projectid, DateTime date, string subcode)
        {
            using (var db = new StorageContext())
            {
                UserActivity userActivity = db.UserActivities
                    .Include(ua => ua.Subactivity).AsEnumerable()
                    .Where(ua => (ua.ProjectId == projectid && ua.Subactivity.IsEqualSubactivity(subcode) &&
                        ua.Date.EqualsYM(date)))
                    .First();
                db.UserActivities.Remove(userActivity);
                db.SaveChanges();
            }
        }
    }
}
