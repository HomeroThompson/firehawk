using Fhwk.Core.Tests.Model.ManyToMany;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Maps.ManyToMany
{
    /// <summary>
    /// Maps the category entity
    /// </summary>
    public class CategoryMap : ClassMapping<Category>
    {
        /// <summary>
        /// Initializes a new instance of CategoryMap
        /// </summary>
        public CategoryMap()
        {
            Bag(x => x.Products, m => m.Cascade(Cascade.None), m => m.ManyToMany());
        }
    }
}
