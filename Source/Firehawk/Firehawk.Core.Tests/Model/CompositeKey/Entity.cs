
namespace Fhwk.Core.Tests.Model.CompositeKey
{
    /// <summary>
    /// A base entity class with a composite key of <long,string>
    /// </summary>
    public class Entity : BaseEntity<short>
    {
        /// <summary>
        /// Gets the entity key
        /// </summary>
        public string Key { get; protected set; }
    }
}
