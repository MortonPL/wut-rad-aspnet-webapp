using Microsoft.EntityFrameworkCore;

namespace lab4.Entities
{
    public class UsersDBEntity
    {
        public static HashSet<User> Select()
        {
            using (var db = new StorageContext())
            {
                return new HashSet<User>(db.Users.ToHashSet());
            }
        }

        public static void Insert(string username)
        {
            using (var db = new StorageContext())
            {
                User user = new User(username);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static bool Find(string? username)
        {
            if (string.IsNullOrEmpty(username))
                return false;
            using (var db = new StorageContext())
            {
                return db.Users.Any(u => u.Name == username);
            }
        }
    }
}
