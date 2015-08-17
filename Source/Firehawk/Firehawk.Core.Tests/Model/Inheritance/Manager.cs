using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// Represents a manager employee
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Gets or sets the department
        /// </summary>
        public virtual string Department { get; set; }

        /// <summary>
        /// Gets or sets the office number
        /// </summary>
        public virtual int OfficeNumber { get; set; }
    }
}
