using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    public class UserActivitiesModel
    {
        public string ActivityError = "";
        public string Date;

        public UserMonth UserMonth;

        public List<string> Activities;

        public UserActivitiesModel(string date)
        {
            this.ActivityError = CheckDate(date);
            if (String.IsNullOrEmpty(this.ActivityError))
            {
                this.Date = date;
            }
        }

        public UserActivitiesModel(){
            this.Date = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public static string CheckDate(string date)
        {
            return "";
        }
        
        public string GetMonth()
        {
            return this.Date.Remove(Date.Length - 3);
        }
    }
}
