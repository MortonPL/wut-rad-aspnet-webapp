using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NTR.Entities
{
    /// <summary>
    /// A class handling IO of user list database.
    /// </summary>
    public class UserListDBEntity
    {
        public static HashSet<User> Load()
        {
            using (var db = new StorageContext())
            {
                return new HashSet<User>(db.Users.Select(u => u).ToHashSet());
            }
        }

        public static void Save(HashSet<User> users)
        {
            // insert
        }
    }
}
