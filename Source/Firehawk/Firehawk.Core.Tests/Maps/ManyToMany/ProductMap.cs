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
    /// Maps the product entity
    /// </summary>
    public class ProductMap : ClassMapping<Product>
    {
        /// <summary>
        /// Initializes a new instance of ProductMap
        /// </summary>
        public ProductMap()
        {
            Bag(x => x.Categories, m => m.Cascade(Cascade.None), m => m.ManyToMany());
        }
    }
}
