using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Models
{
    public class UserModel
    {
        public string UsernameError = "";
        public string Name = "";

        public UserModel(string name, HashSet<String> nameList)
        {
            this.UsernameError = CheckName(name, nameList);
            if (!String.IsNullOrEmpty(UsernameError))
            {
                this.Name = name;
            }
        }

        public UserModel(){ }

        public IEnumerable<SelectListItem> Options
        {
            get
            {
                var userList = Entities.UserListDBEntity.Load();
                var selectList = new List<SelectListItem>();
                selectList.AddRange(userList.Select(s => new SelectListItem(s, s)));
                return selectList;
            }
        }

        public static string CheckName(string name, HashSet<String> userList)
        {
            if (name.Length >= 48)
            {
                return "maxlength";
            }
            if (userList.Contains(name))
            {
                return "taken";
            }
            return "";
        }
    }
}
