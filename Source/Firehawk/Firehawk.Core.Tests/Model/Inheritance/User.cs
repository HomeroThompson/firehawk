using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// Represents an employee
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public virtual string UserName { get; set; }

    }
}
