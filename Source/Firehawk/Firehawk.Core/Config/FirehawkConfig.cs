using Fhwk.Core.Resources;
using Fhwk.Core.Utils.ConfigExt;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace Fhwk.Core.Config
{
    /// <summary>
    /// Allows to set the Firehawk configuration in a fluent way
    /// </summary>
    public class FirehawkConfig
    {
        #region Members

        private Firehawk config;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FirehawkConfig"/> class.
        /// </summary>
        internal FirehawkConfig(Firehawk config)
        {
            this.config = config;
            this.EntityDefinitionsConfig = new EntitiesConfig(this);
            this.MappingsConfig = new MappingsConfig(this);
            this.NamingConventionsConfig = new NamingConventionsConfig(this);
            this.Extensions = new List<FirehawkExtension>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the entity definitions configuration
        /// </summary>
        internal EntitiesConfig EntityDefinitionsConfig { get; private set; }

        /// <summary>
        /// Gets the mappings configuration
        /// </summary>
        internal MappingsConfig MappingsConfig { get; private set; }

        /// <summary>
        /// Gets the naming conventions configurations
        /// </summary>
        internal NamingConventionsConfig NamingConventionsConfig { get; private set; }

        /// <summary>
        /// Gets the list of registered extensions
        /// </summary>
        internal IList<FirehawkExtension> Extensions { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allows to configure the entity definitions in a fluent way
        /// </summary>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig ConfigureEntities()
        {
            return EntityDefinitionsConfig;
        }

        /// <summary>
        /// Allows to configure the custom mappings definitions in a fluent way
        /// </summary>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig ConfigureMappings()
        {
            return MappingsConfig;
        }

        /// <summary>
        /// Allows to configure the naming conventions used when mapping the entity names to database table names
        /// </summary>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig ConfigureNamingConventions()
        {
            return NamingConventionsConfig;
        }

        /// <summary>
        /// Allows to register an extension that executes custom code on different extension points
        /// </summary>
        /// <param name="extension">An instance of the extension</param>
        public FirehawkConfig RegisterExtension(FirehawkExtension extension)
        {
            if (!this.Extensions.Contains(extension))
            {
                this.Extensions.Add(extension);
            }

            return this;
        }

        /// <summary>
        /// Allows to register an extension that executes custom code on different extension points
        /// </summary>
        /// <typeparam name="TExtension">The extension type. This type must extend <see cref="FirehawkExtension"/> type</typeparam>
        public FirehawkConfig RegisterExtension<TExtension>() where TExtension : FirehawkExtension
        {
            try
            {
                TExtension ext = Activator.CreateInstance<TExtension>();
                RegisterExtension(ext);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(string.Format(ExceptionMessages.UnableToCreateExtensionInstance, typeof(TExtension).Name), ex);
            }

            return this;
        }

        /// <summary>
        /// Ends the current configuration
        /// </summary>
        /// <returns>An instance of Firehawk</returns>
        public Firehawk EndConfiguration()
        {
            return config;
        }

        #endregion
    }
}
