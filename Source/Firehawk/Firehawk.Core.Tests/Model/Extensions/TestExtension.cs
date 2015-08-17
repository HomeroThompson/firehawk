using Fhwk.Core.Utils.ConfigExt;
using NHibernate.Cfg.MappingSchema;
using System;
using System.Collections.Generic;

namespace Fhwk.Core.Tests.Model.Extensions
{
    /// <summary>
    /// An extension for testing purposes
    /// </summary>
    public class TestExtension : FirehawkExtension
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a delegate for the BeforeCompileMappings method
        /// </summary>
        public Action<IList<Type>> BeforeCompileMappingsDelegate { get; set; }

        /// <summary>
        /// Gets or sets a delegate for the AfterCompileMappings method
        /// </summary>
        public Action<IList<Type>, HbmMapping> AfterCompileMappingsDelegate { get; set; }

        /// <summary>
        /// Gets or sets a delegate for the BeforeBuildMappings method
        /// </summary>
        public Action<HbmMapping> BeforeBuildMappingsDelegate { get; set; }

        /// <summary>
        /// Gets or sets a delegate for the AfterBuildMappings method
        /// </summary>
        public Action<HbmMapping> AfterBuildMappingsDelegate { get; set; }

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Allows to execute custom code before the given entity types are compiled into the mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public override void BeforeCompileMappings(IList<Type> entityTypes)
        {
            if (BeforeCompileMappingsDelegate != null)
            {
                BeforeCompileMappingsDelegate(entityTypes);
            }
        }

        /// <summary>
        /// Allows to execute custom code after the entity types were compiled to the given mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public override void AfterCompileMappings(IList<Type> entityTypes, HbmMapping mapping)
        {
            if (AfterCompileMappingsDelegate != null)
            {
                AfterCompileMappingsDelegate(entityTypes, mapping);
            }
        }

        /// <summary>
        /// Allows to execute custom code before the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public override void BeforeBuildMappings(HbmMapping mapping)
        {
            if (BeforeBuildMappingsDelegate != null)
            {
                BeforeBuildMappingsDelegate(mapping);
            }
        }

        /// <summary>
        /// Allows to execute custom code after the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public override void AfterBuildMappings(HbmMapping mapping)
        {
            if (AfterBuildMappingsDelegate != null)
            {
                AfterBuildMappingsDelegate(mapping);
            }
        }

        #endregion
    }
}
