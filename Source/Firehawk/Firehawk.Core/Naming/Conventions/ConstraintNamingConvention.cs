using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core
{
     /// <summary>
    /// Defines the different naming conventions used when generating constrint names
    /// </summary>
    public enum ConstraintNamingConvention
    {
        /// <summary>
        /// Default naming convention
        /// </summary>
        Default,

        /// <summary>
        /// Uppercase underscore separated naming convention
        /// </summary>
        Uppercase,

        /// <summary>
        /// Lowercase underscore separated naming convention
        /// </summary>
        Lowercase
    }
}
