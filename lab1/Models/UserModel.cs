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
        public string Name = "";

        public HashSet<String> NameList;

        public UserModel(string name, HashSet<String> nameList)
        {
            this.UsernameError = CheckName(name, nameList);
            if (String.IsNullOrEmpty(this.UsernameError))
            {
                this.Name = name;
                this.NameList = nameList;
            }
        }

        public UserModel(){ }

        public IEnumerable<SelectListItem> GetUserList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                selectList.AddRange(this.NameList.Select(s => new SelectListItem(s, s)));
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
