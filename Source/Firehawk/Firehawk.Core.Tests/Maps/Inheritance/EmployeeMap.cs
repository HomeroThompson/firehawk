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
    /// Maps the employee entity
    /// </summary>
    public class EmployeeMap : ClassMapping<Employee>
    {
        /// <summary>
        /// Initializes a new instance of EmployeeMap
        /// </summary>
        public EmployeeMap()
        {
        }
    }
}
