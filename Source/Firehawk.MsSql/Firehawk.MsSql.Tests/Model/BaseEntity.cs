using System;

namespace Fhwk.MsSql.Tests.Model
{
    /// <summary>
    /// A base class for entities
    /// </summary>
    /// <typeparam name="TID">The type of the entity ID field</typeparam>
    public class BaseEntity<TID>
    {
        /// <summary>
        /// Gets the entity ID
        /// </summary>
        public virtual TID ID { get; protected set; }

        /// <summary>
        /// Gets the entity version
        /// </summary>
        public virtual int Version { get; protected set; }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            BaseEntity<TID> other = obj as BaseEntity<TID>;
            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Equals the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>true of the specified object equals this object; otherwise false</returns>
        public virtual bool Equals(BaseEntity<TID> obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (IsTransient(this) || IsTransient(obj) || !BaseEntity<TID>.Equals(this.ID, obj.ID))
            {
                return false;
            }

            Type otherType = obj.GetUnproxiedType();
            Type thisType = this.GetUnproxiedType();

            return otherType.IsAssignableFrom(thisType) || thisType.IsAssignableFrom(otherType);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            if (IsTransient(this))
            {
                return base.GetHashCode();
            }
            else
            {
                return this.ID.GetHashCode();
            }
        }

        /// <summary>
        /// Determines whether the specified entity is transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is transient; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTransient(BaseEntity<TID> entity)
        {
            return entity != null && BaseEntity<TID>.Equals(entity.ID, default(TID));
        }

        /// <summary>
        /// Gets the type of the base class (non-proxy class).
        /// </summary>
        /// <returns><see cref="System.Type"/></returns>
        private Type GetUnproxiedType()
        {
            return GetType();
        }
    }
}
