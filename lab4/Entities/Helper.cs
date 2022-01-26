using lab4.Entities;

namespace lab4.Helpers
{
    public static class Helper
    {
        public static DateTime GetYM(this DateTime date) => new DateTime(date.Year, date.Month, 1, 1, 1, 1);

        public static DateTime GetYMD(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 1, 1, 1);

        public static DateTime GetYMDHM(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 1);

        public static bool EqualsYM(this DateTime date, DateTime other) => DateTime.Equals(date.GetYM(), other.GetYM());

        public static bool EqualsYMD(this DateTime date, DateTime other) => DateTime.Equals(date.GetYMD(), other.GetYMD());

        public static bool EqualsYMDHM(this DateTime date, DateTime other) => DateTime.Equals(date.GetYMDHM(), other.GetYMDHM());

        public static List<TJson> toJSON<T, TJson>(ICollection<T>? collection)
        where T: IEntity<TJson>
        where TJson: EntityJson
        {
            return collection != null ? collection.Select(e => e.toJSON()).ToList() : new List<TJson>();
        }
    }
}
