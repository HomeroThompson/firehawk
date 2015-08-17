using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// A chieff employee
    /// </summary>
    public class Chief: Employee
    {
        /// <summary>
        /// Gets or sets the phone
        /// </summary>
        public virtual string Phone { get; set; }
    }
}
