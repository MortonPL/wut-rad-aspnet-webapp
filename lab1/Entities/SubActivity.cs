using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NTR.Entities
{
    /// <summary>
    /// A single subactivity - project category.
    /// </summary>
    public class SubActivity
    {
        /// <summary>Code ID of the activity.</summary>
        public string code { get; set; }

        public SubActivity(){}

    }
}
