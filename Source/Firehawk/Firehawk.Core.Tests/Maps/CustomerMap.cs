using Fhwk.Core.Tests.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Maps
{
    /// <summary>
    /// Defines how the Customer entity gets mapped to the database
    /// </summary>
    public class CustomerMap : ClassMapping<Customer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMap"/> class.
        /// </summary>
        public CustomerMap()
        {
           
        }
    }
}
