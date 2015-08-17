using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// Represents a zip code
    /// </summary>
    public class ZipCode: Entity
    {
        /// <summary>
        /// The zip postal code
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// The associated city
        /// </summary>
        public virtual City City { get; set; }
    }
}
