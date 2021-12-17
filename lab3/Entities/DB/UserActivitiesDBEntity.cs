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

        public static bool Update(DateTime date, string userName, string projectId, string subactivityId, int time, string description)
        {
            using (var db = new StorageContext())
            {
                try
                {
                    UserActivity userActivity = db.UserActivities.AsEnumerable()
                        .Where(ua => (Helper.GetYM(ua.Month) == Helper.GetYM(date)
                            && ua.UserName == userName && ua.ProjectId == projectId && ua.SubactivityId == subactivityId))
                        .First();
                    db.Update(userActivity);
                    userActivity.Time = time;
                    userActivity.Description = description;
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        public static string Insert(DateTime date, string userName, string projectId, string subactivityId, int time, string description)
        {
            subactivityId = subactivityId != null ? subactivityId : "";
            using (var db = new StorageContext())
            {
                UserMonth userMonth;
                HashSet<UserMonth> userMonths = db.UserMonths.AsEnumerable()
                    .Where(um => (um.UserName == userName && Helper.GetYM(um.Month) == Helper.GetYM(date)))
                    .ToHashSet();
                if (userMonths.Count > 0)
                {
                    userMonth = userMonths.First();
                }
                else
                {
                    userMonth = new UserMonth{Month=Helper.GetYM(date), UserName=userName};
                    db.UserMonths.Add(userMonth);
                }
                UserActivity userActivity = new UserActivity{Date=date, ProjectId=projectId, SubactivityId=subactivityId,
                    Time=time, Description=description, UserMonth=userMonth};
                try
                {
                    db.UserActivities.Add(userActivity);
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return "EUNIQUE";
                }
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
