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
    /// Maps the bike entity
    /// </summary>
    public class BikeMap : UnionSubclassMapping<Bike>
    {
        /// <summary>
        /// Initializes a new instance of BikeMap
        /// </summary>
        public BikeMap()
        {
        }
    }
}
