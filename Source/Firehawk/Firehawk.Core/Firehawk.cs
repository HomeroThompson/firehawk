using Fhwk.Core.Config;
using Fhwk.Core.Mapping;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System;

namespace Fhwk.Core
{
    /// <summary>
    /// The Firehawk entry point
    /// </summary>
    public class Firehawk
    {
        #region Members

        private FirehawkConfig rootConfig;
        private ConventionModelMapper modelMapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Firehawk"/> class
        /// </summary>
        internal Firehawk()
        {
            this.rootConfig = new FirehawkConfig(this);
            this.modelMapper = new ConventionModelMapper();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the Firehawk configuration
        /// </summary>
        /// <returns>An instance of FirehawkConfig</returns>
        public static Firehawk Init()
        {
            return new Firehawk();
        }

        /// <summary>
        /// Starts the Firehawk configuration
        /// </summary>
        /// <returns>An instance of FirehawkConfig</returns>
        public FirehawkConfig Configure()
        {
            return rootConfig;
        }

        /// <summary>
        /// Allows to indicate that a given type is used as root entity type
        /// </summary>
        /// <typeparam name="TRootEntity">The Type of the root entity</typeparam>
        /// <param name="entityMap">The entity class mapper</param>
        /// <returns>An instance of FirehawkConfig</returns>
        public Firehawk AddCustomMapping<TRootEntity>(Action<IClassMapper<TRootEntity>> entityMap) where TRootEntity : class
        {
            this.modelMapper.Class(entityMap);

            return this;
        }

        /// <summary>
        /// Provides access to the underlying ModelMapper
        /// </summary>
        /// <param name="action">An action with the model mapper as input param</param>
        /// <returns></returns>
        public Firehawk GetModelMapper(Action<ModelMapper> action)
        {
            action(this.modelMapper);

            return this;
        }

        /// <summary>
        /// Builds the current configuration by using an instance of NH configuration
        /// </summary>
        /// <param name="config">The NH Configuration</param>
        public void BuildMappings(Configuration config)
        {
            MappingEngine engine = new MappingEngine(this.rootConfig, config, this.modelMapper);
            engine.BuildConfiguration();
        }

        /// <summary>
        /// Compiles the mappings without adding them to the configuration
        /// </summary>
        /// <param name="config">The NH Configuration</param>
        public HbmMapping CompileMappings(Configuration config)
        {
            MappingEngine engine = new MappingEngine(this.rootConfig, config, this.modelMapper);
            return engine.CompileMappings();
        }

        #endregion


    }

}
