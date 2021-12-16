using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NTR.Entities;

namespace NTR.Models
{
    public class UserModel
    {
        public string UsernameError = "";
        public string User = "";
        public HashSet<User> Users;

        public UserModel()
        {
            this.LoadUsers();
        }

        public UserModel(string user)
        {
            this.LoadUsers();
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
                selectList.AddRange(this.Users.Select(s => new SelectListItem(s.Name, s.Name)));
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
            if (this.Users.Where(u => u.Name == name).Count() > 0)
            {
                return "ETAKEN";
            }
            
            return "";
        }

        public void LoadUsers()
        {
            this.Users = Entities.UsersDBEntity.Select();
        }

        public void AddUser(string user)
        {
            Entities.UsersDBEntity.Insert(user);
        }
    }
}
