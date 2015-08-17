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
    /// Maps the car entity
    /// </summary>
    public class CarMap : UnionSubclassMapping<Car>
    {
        /// <summary>
        /// Initializes a new instance of CarMap
        /// </summary>
        public CarMap()
        {
        }
    }
}
