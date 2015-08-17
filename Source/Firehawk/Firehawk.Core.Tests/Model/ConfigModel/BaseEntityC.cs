using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.ConfigModel
{
    /// <summary>
    /// A base entity
    /// </summary>
    public class BaseEntityC
    {
        /// <summary>
        /// Gets the entity ID
        /// </summary>
        public virtual int ID { get; protected set; }
    }
}
