using Fhwk.Core.Tests.Model.Inheritance;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Maps.Inheritance
{
    /// <summary>
    /// Maps the user entity
    /// </summary>
    public class UserMap : ClassMapping<User>
    {
        /// <summary>
        /// Initializes a new instance of UserMap
        /// </summary>
        public UserMap()
        {
            Discriminator(x =>
            {
                x.Type(NHibernate.NHibernateUtil.String);
                x.Column("discriminator");
            });
        }
    }
}
