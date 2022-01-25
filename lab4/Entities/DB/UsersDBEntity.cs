using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab4.Entities
{
    public class UsersDBEntity
    {
        public static HashSet<string> SelectNames()
        {
            using (var db = new StorageContext())
            {
                return new HashSet<string>(db.Users.Select(u => u.Name).ToHashSet());
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

        public static bool Find(string username)
        {
            using (var db = new StorageContext())
            {
                return db.Users.Any(u => u.Name == username);
            }
        }
    }
}
