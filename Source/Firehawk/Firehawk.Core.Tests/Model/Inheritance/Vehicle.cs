using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// A vehicle
    /// </summary>
    public abstract class Vehicle: Entity
    {
        /// <summary>
        /// Gets or sets the number
        /// </summary>
        public virtual string Number { get; set; }

        /// <summary>
        /// Gets or sets the year
        /// </summary>
        public virtual int Year { get; set; }
    }
}
