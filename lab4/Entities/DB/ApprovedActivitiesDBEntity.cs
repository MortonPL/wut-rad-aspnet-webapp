namespace lab4.Entities
{
    public class ApprovedActivitiesDBEntity
    {
        public static List<ApprovedActivityStatsJson> SelectStats(string name)
        {
            using (var db = new StorageContext())
            {
                List<ApprovedActivityStatsJson> activities = db.ApprovedActivities
                    .Where(aa => (aa.UserName == name))
                    .GroupBy(aa => aa.ProjectId, aa => aa.Time, (id, time) => new ApprovedActivityStatsJson{projectId=id, time=time.Sum()})
                    .ToList();
                if (activities.Count > 0)
                    return activities;
                else
                    return new List<ApprovedActivityStatsJson>();
            }
        }
    }
}
