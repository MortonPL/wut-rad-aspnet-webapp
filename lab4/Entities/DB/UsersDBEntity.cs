using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab4.Entities
{
    public class UsersDBEntity
    {
        public static HashSet<User> Select()
        {
            using (var db = new StorageContext())
            {
                return new HashSet<User>(db.Users.Select(u => u).ToHashSet());
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
