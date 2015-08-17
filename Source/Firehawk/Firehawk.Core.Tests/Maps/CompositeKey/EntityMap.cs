using Fhwk.Core.Tests.Model.CompositeKey;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.Core.Tests.Maps.CompositeKey
{
    /// <summary>
    /// Defines how the base entity class gets mapped to the database
    /// </summary>
    public class EntityMap : ClassMapping<Entity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMap"/> class.
        /// </summary>
        public EntityMap()
        {
            ComposedId(m =>
            {
                m.Property(e => e.ID);
                m.Property(e => e.Key);
            });
            Version(e => e.Version, m =>
                {
                    m.Generated(VersionGeneration.Never);
                    m.UnsavedValue(0);
                    m.Insert(true);
                });
        }
    }
}
