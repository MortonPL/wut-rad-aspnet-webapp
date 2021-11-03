using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using NTR.Entities;

namespace NTR.Models
{
    public class ActivitiesModel
    {
        public HashSet<Activity> Activities;

        public ActivitiesModel(HashSet<Activity> activities)
        {
            this.Activities = activities;
        }

        public ActivitiesModel(){}
    }
}
