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
    /// Maps the admin entity
    /// </summary>
    public class AdminMap : SubclassMapping<Admin>
    {
        /// <summary>
        /// Initializes a new instance of AdminMap
        /// </summary>
        public AdminMap()
        {
            DiscriminatorValue("Admin");   
        }
    }
}
