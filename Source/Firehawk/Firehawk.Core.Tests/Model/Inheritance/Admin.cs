using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// An admin user
    /// </summary>
    public class Admin: User
    {
        /// <summary>
        /// Gets or set the email
        /// </summary>
        public virtual string Email { get; set; }
    }
}
