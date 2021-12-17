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
                HashSet<UserMonth> usermonths = db.UserMonths
                    .Include(um => um.UserActivities)
                    .ThenInclude(ua => ua.Subactivity).AsEnumerable()
                    .Where(um => (um.UserName == name && DateTime.Equals(Helper.GetYM(um.Month), Helper.GetYM(date))))
                    .ToHashSet();
                if (usermonths.Count > 0)
                {
                    return usermonths.First();
                }
                return new UserMonth(true);
            }
        }

        public static void Save(string name, DateTime date, UserMonth userMonth)
        {
        }

        public static string Insert(DateTime date, string userName, string projectId, string subactivityId, int time, string description)
        {
            subactivityId = subactivityId != null ? subactivityId : "";
            using (var db = new StorageContext())
            {
                UserMonth userMonth;
                HashSet<UserMonth> userMonths = db.UserMonths.Where(um => (um.UserName == userName && um.Month == date)).ToHashSet();
                if (userMonths.Count > 0)
                {
                    userMonth = userMonths.First();
                }
                else
                {
                    userMonth = new UserMonth{Month=date, UserName=userName};
                    db.UserMonths.Add(userMonth);
                }
                UserActivity userActivity = new UserActivity{Date=date, ProjectId=projectId, SubactivityId=subactivityId,
                    Time=time, Description=description, UserMonth=userMonth};
                try
                {
                    db.UserActivities.Add(userActivity);
                }
                catch (DbUpdateException)
                {
                    return "EUNIQUE";
                }
                db.SaveChanges();
                return "";
            }
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
