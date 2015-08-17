using Fhwk.Core.Config;
using Fhwk.MsSql.Naming;
using System;
using System.Reflection;

namespace Fhwk.Core
{
    /// <summary>
    /// Provides extensions methods to the <see cref="NamingConventionsConfig"/> class
    /// </summary>
    public static class NamingConventionsConfigExtensions
    {
        /// <summary>
        /// Indicates that a given convention must be used when creating the primary key names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public static NamingConventionsConfig UseConventionForPrimaryKeyNames(this NamingConventionsConfig config, PrimaryKeyNamingConvention convention)
        {
            if (convention != PrimaryKeyNamingConvention.Default)
            {
                var pkExt = new PrimaryKeyExtension()
                {
                    PrimaryKeyNamingConvention = convention
                };

                config.EndConfig().RegisterExtension(pkExt);
            }

            return config;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when creating the primary key names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the pk entity type and the pk column property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public static NamingConventionsConfig UseCustomConventionForPrimaryKeyNames(this NamingConventionsConfig config, Func<Type, MemberInfo, string> customConvention)
        {
            var pkExt = new PrimaryKeyExtension()
            {
                PrimaryKeyNamingConvention = PrimaryKeyNamingConvention.Custom,
                PrimaryKeyCustomNamingConvention = customConvention
            };

            config.EndConfig().RegisterExtension(pkExt);

            return config;
        }
    }
}
