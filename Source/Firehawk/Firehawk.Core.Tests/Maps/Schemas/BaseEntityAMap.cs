using Fhwk.Core.Tests.Model.Schemas;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.Core.Tests.Maps.Schemas
{
    /// <summary>
    /// Defines how the BaseEntityA entity gets mapped to the database
    /// </summary>
    public class BaseEntityAMap: ClassMapping<BaseEntityA>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityAMap"/> class.
        /// </summary>
        public BaseEntityAMap()
        {
            Schema("SchemaA");
            Id(e => e.ID, m => m.Generator(Generators.Identity));
            Version(e => e.Version, m =>
            {
                m.Generated(VersionGeneration.Never);
                m.UnsavedValue(0);
                m.Insert(true);
            });
        }
    }
}
