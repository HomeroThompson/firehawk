using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.Elements
{
    /// <summary>
    /// Represents an entity with property elements
    /// </summary>
    public class Element : Entity
    {
        /// <summary>
        /// A list of strings
        /// </summary>
        public virtual IList<string> ListOfString { get; set; }

        /// <summary>
        /// A list of integer
        /// </summary>
        public virtual IList<int> ListOfInt { get; set; }

        /// <summary>
        /// A list of guids
        /// </summary>
        public virtual IList<Guid> ListOfGuid { get; set; }

        /// <summary>
        /// A dictionary of long -> string
        /// </summary>
        public virtual IDictionary<long, string> DictionaryLongString { get; set; }
    }
}
