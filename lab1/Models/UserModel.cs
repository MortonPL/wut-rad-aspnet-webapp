using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTR.Entities;

namespace NTR.Models
{
    public class UserModel
    {
        /// <summary>User name error. Empty if no error.</summary>
        public string UsernameError = "";

        /// <summary>Name of the user.</summary>
        public string User = "";

        /// <summary>List of saved users.</summary>
        public HashSet<String> UserList;

        public UserModel()
        {
            this.LoadFromDB();
        }

        public UserModel(string user)
        {
            this.LoadFromDB();
            this.UsernameError = this.CheckName(user);
            if (String.IsNullOrEmpty(this.UsernameError))
            {
                this.User = user;
            }
        }

        /// <summary>Generates a select list out of user list.</summary>
        /// <returns>Enumerable of select list items containing user names.</returns>
        public IEnumerable<SelectListItem> CreateUserSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                selectList.AddRange(this.UserList.Select(s => new SelectListItem(s, s)));
                return selectList;
            }
        }

        /// <summary>Check if the nate is not empty and has less than 48 characters.</summary>
        /// <param name="name">Name to be validated.</param>
        /// <returns>Error code if any.</returns>
        public string CheckName(string name)
        {
            if (name.Length >= 48)
            {
                return "EMAX";
            }
            if (this.UserList.Contains(name))
            {
                return "ETAKEN";
            }
            return "";
        }

        /// <summary>Add the user to the model's list.</summary>
        /// <param name="user">User name to be added.</param>
        public void AddUser(string user)
        {
            this.UserList.Add(user);
        }

        /// <summary>Load list of users from the database.</summary>
        public void LoadFromDB()
        {
            this.UserList = Entities.UserListDBEntity.Load();
        }

        /// <summary>Save list of users to the database.</summary>
        public void SaveToDB()
        {
            Entities.UserListDBEntity.Save(this.UserList);
        }
    }
}
