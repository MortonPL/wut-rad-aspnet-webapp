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

        public IEnumerable<SelectListItem> CreateUserSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                selectList.AddRange(this.Users.Select(s => new SelectListItem(s.Name, s.Name)));
                return selectList;
            }
        }

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
