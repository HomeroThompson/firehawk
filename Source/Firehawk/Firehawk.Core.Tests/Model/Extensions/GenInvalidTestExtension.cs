using Fhwk.Core.Utils.ConfigExt;
using NHibernate.Cfg.MappingSchema;
using System;
using System.Collections.Generic;

namespace Fhwk.Core.Tests.Model.Extensions
{
    /// <summary>
    /// An extension for testing purposes
    /// </summary>
    public class GenInvalidTestExtension : FirehawkExtension
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenInvalidTestExtension"/> class.
        /// </summary>
        /// <param name="propA">An int value</param>
        /// <param name="propB">A string value</param>
        public GenInvalidTestExtension(int propA, string propB)
        { 

        }

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Allows to execute custom code before the given entity types are compiled into the mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public override void BeforeCompileMappings(IList<Type> entityTypes)
        {
        }

        /// <summary>
        /// Allows to execute custom code after the entity types were compiled to the given mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public override void AfterCompileMappings(IList<Type> entityTypes, HbmMapping mapping)
        {
        }

        /// <summary>
        /// Allows to execute custom code before the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public override void BeforeBuildMappings(HbmMapping mapping)
        {
        }

        /// <summary>
        /// Allows to execute custom code after the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public override void AfterBuildMappings(HbmMapping mapping)
        {
        }

        #endregion
    }
}
