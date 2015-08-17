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
    /// Maps the chief entity
    /// </summary>
    public class ChiefMap : JoinedSubclassMapping<Chief>
    {
        /// <summary>
        /// Initializes a new instance of EmployeeMap
        /// </summary>
        public ChiefMap()
        {
            
        }
    }
}
