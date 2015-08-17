using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// Represents a city entity
    /// </summary>
    public class City : Entity
    {
        /// <summary>
        /// Gets or sets the city name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the postal code
        /// </summary>
        public virtual ZipCode ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public virtual State State { get; set; }
    }
}
