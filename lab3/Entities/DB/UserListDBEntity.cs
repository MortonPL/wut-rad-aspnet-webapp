using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A class handling IO of user list database.
    /// </summary>
    public class UserListDBEntity
    {
        /// <summary>
        /// Load the user list from the database.
        /// </summary>
        /// <returns>List of user names.</returns>
        public static HashSet<String> Load()
        {
            var json = System.IO.File.ReadAllText("db/users.json");
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<HashSet<String>>(json);
            }
            catch (System.Text.Json.JsonException)
            {
                return new HashSet<string>();
            }
        }

        /// <summary>
        /// Save the user list to the database.
        /// </summary>
        /// <param name="userList">List of user names.</param>
        public static void Save(HashSet<String> userList)
        {
            var jsonOptions = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userList, jsonOptions);
            System.IO.File.WriteAllBytes("db/users.json", bytes);
        }
    }
}
