using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A state enity
    /// </summary>
    public class State : Entity
    {
        /// <summary>
        /// Gets or sets the state name
        /// </summary>
        public virtual string Name { get; set; }
    }
}
