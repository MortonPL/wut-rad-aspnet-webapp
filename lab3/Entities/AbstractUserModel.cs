using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// Base abstract DB model.
    /// </summary>
    public abstract class AbstractUserModel
    {
        /// <summary>Logged user's name.</summary>
        public string User;

        /// <summary>Load user activities from the database.</summary>
        public abstract void LoadFromDB();

        /// <summary>Save user activities to the database.</summary>
        public abstract void SaveToDB();
    }
}
