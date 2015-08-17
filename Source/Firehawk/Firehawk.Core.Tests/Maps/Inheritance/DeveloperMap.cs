using Fhwk.Core.Tests.Model.Inheritance;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Maps.Inheritance
{
    /// <summary>
    /// Maps the developer entity
    /// </summary>
    public class DeveloperMap : SubclassMapping<Developer>
    {
        /// <summary>
        /// Initializes a new instance of DeveloperMap
        /// </summary>
        public DeveloperMap()
        {
            DiscriminatorValue("Developer");   
        }
    }
}
