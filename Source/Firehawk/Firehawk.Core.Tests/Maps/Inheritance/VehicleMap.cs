using Fhwk.Core.Tests.Model.Inheritance;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Maps.Inheritance
{
    /// <summary>
    /// Maps the vehicle entity
    /// </summary>
    public class VehicleMap : ClassMapping<Vehicle>
    {
        /// <summary>
        /// Initializes a new instance of VehicleMap
        /// </summary>
        public VehicleMap()
        {
            
        }
    }
}
