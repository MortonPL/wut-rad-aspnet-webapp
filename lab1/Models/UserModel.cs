using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Models
{
    public class UserModel
    {
        public bool IsValidUsername = true;
        public string Name = "";

        public UserModel(bool isValidUsername)
        {
            this.IsValidUsername = false;
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
    }
}
