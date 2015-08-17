using Fhwk.Core.Tests.Model.Schemas;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.Core.Tests.Maps.Schemas
{
    /// <summary>
    /// Defines how the BaseEntityC entity gets mapped to the database
    /// </summary>
    public class BaseEntityCMap: ClassMapping<BaseEntityC>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityCMap"/> class.
        /// </summary>
        public BaseEntityCMap()
        {
            Schema("SchemaC");
            Id(e => e.ID, m => m.Generator(Generators.Identity));
        }
    }
}
