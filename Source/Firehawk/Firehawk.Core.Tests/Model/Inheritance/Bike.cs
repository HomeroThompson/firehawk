using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Inheritance
{
    /// <summary>
    /// A bike
    /// </summary>
    public class Bike: Vehicle
    {
        /// <summary>
        /// Gets or sets the motr type
        /// </summary>
        public virtual string Type { get; set; }
    }
}
