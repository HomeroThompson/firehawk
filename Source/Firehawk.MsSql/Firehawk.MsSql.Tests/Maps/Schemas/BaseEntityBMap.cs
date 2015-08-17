using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Fhwk.MsSql.Tests.Model.Schemas
{
    /// <summary>
    /// Defines how the BaseEntityBMap entity gets mapped to the database
    /// </summary>
    public class BaseEntityBMap : ClassMapping<BaseEntityB>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntityBMap"/> class.
        /// </summary>
        public BaseEntityBMap()
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
