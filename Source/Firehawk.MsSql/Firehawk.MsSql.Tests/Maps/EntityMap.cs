using Fhwk.MsSql.Tests.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.MsSql.Tests.Maps
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
