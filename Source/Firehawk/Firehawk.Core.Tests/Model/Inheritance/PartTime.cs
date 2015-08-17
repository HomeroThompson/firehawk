using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// A developer user
    /// </summary>
    public class Developer : User
    {
        /// <summary>
        /// Gets or sets the project
        /// </summary>
        public virtual string Project { get; set; }
    }
}
