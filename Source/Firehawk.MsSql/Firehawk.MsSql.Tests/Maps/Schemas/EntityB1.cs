using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.MsSql.Tests.Model.Schemas
{
    /// <summary>
    /// Defines how the EntityB1Map entity gets mapped to the database
    /// </summary>
    public class EntityB1Map: ClassMapping<EntityB1>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityB1Map"/> class.
        /// </summary>
        public EntityB1Map()
        {
            Schema("SchemaB");
        }
    }
}
