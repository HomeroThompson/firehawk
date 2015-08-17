using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Fhwk.Core.Tests.Model.Extensions;
using Fhwk.Core.Utils.ConfigExt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg.MappingSchema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the extensions feature
    /// </summary>
    [TestClass]
    public class ExtensionsTests : BaseDatabaseTest
    {
        /// <summary>
        /// Tests the generic AddExtension method
        /// </summary>
        [TestMethod]
        public void GenericRegisterExtensionBuildConfigTest()
        {
            var config = Firehawk.Init().Configure();
            config
                .RegisterExtension<GenTestExtension>()
                    .EndConfiguration()
                .BuildMappings(NHConfig);

            Assert.IsNotNull(config);
            Assert.AreEqual(1, config.Extensions.Count);

            var ext = config.Extensions.Single() as GenTestExtension;
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext.BeforeCompileMappingsExecuted);
            Assert.IsTrue(ext.AfterCompileMappingsExecuted);
            Assert.IsTrue(ext.BeforeBuildMappingsExecuted);
            Assert.IsTrue(ext.AfterBuildMappingsExecuted);
        }

        /// <summary>
        /// Tests the generic AddExtension method
        /// </summary>
        [TestMethod]
        public void GenericRegisterExtensionCompileMappingsTest()
        {
            var config = Firehawk.Init().Configure();
            config
                .RegisterExtension<GenTestExtension>()
                    .EndConfiguration()
                .CompileMappings(NHConfig);

            Assert.IsNotNull(config);
            Assert.AreEqual(1, config.Extensions.Count);

            var ext = config.Extensions.Single() as GenTestExtension;
            Assert.IsNotNull(ext);
            Assert.IsTrue(ext.BeforeCompileMappingsExecuted);
            Assert.IsTrue(ext.AfterCompileMappingsExecuted);
            Assert.IsFalse(ext.BeforeBuildMappingsExecuted);
            Assert.IsFalse(ext.AfterBuildMappingsExecuted);
        }

        /// <summary>
        /// Tests the generic AddExtension method
        /// </summary>
        [TestMethod]
        public void GenericRegisterExtensionNoExecutedTest()
        {
            var config = Firehawk.Init().Configure();
            config
                .RegisterExtension<GenTestExtension>()
                .EndConfiguration();

            Assert.IsNotNull(config);
            Assert.AreEqual(1, config.Extensions.Count);

            var ext = config.Extensions.Single() as GenTestExtension;
            Assert.IsNotNull(ext);
            Assert.IsFalse(ext.BeforeCompileMappingsExecuted);
            Assert.IsFalse(ext.AfterCompileMappingsExecuted);
            Assert.IsFalse(ext.BeforeBuildMappingsExecuted);
            Assert.IsFalse(ext.AfterBuildMappingsExecuted);
        }

        /// <summary>
        /// Tests the generic AddExtension method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void GenericRegisterInvalidExtensionTest()
        {
            Firehawk.Init()
                .Configure()
                    .RegisterExtension<GenInvalidTestExtension>()
                .EndConfiguration();
        }

        /// <summary>
        /// Tests the generic AddExtension method
        /// </summary>
        [TestMethod]
        public void GenericRegisterFirehawkExtensionTest()
        {
            var config = Firehawk.Init().Configure();
            config
                .RegisterExtension<FirehawkExtension>()
                    .EndConfiguration()
                .BuildMappings(NHConfig);

            Assert.IsNotNull(config);
            Assert.AreEqual(1, config.Extensions.Count);

            var ext = config.Extensions.Single() as FirehawkExtension;
            Assert.IsNotNull(ext);
        }

        /// <summary>
        /// Tests the AddExtension method
        /// </summary>
        [TestMethod]
        public void RegisterExtensionBuildConfigTest()
        {
            var ext = new TestExtension();
            ext.BeforeCompileMappingsDelegate = (types) =>
            {
                Assert.IsNotNull(types);
                Assert.AreEqual(4, types.Count);
                Assert.IsTrue(types.Contains(typeof(City)));
                Assert.IsTrue(types.Contains(typeof(State)));
                Assert.IsTrue(types.Contains(typeof(ZipCode)));
                Assert.IsTrue(types.Contains(typeof(Customer)));
            };

            ext.AfterCompileMappingsDelegate = (types, mapping) =>
            {
                Assert.IsNotNull(types);
                Assert.AreEqual(4, types.Count);
                Assert.IsTrue(types.Contains(typeof(City)));
                Assert.IsTrue(types.Contains(typeof(State)));
                Assert.IsTrue(types.Contains(typeof(ZipCode)));
                Assert.IsTrue(types.Contains(typeof(Customer)));

                Assert.IsNotNull(mapping);
                Assert.AreEqual(4, mapping.Items.Count());
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(City).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(State).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(ZipCode).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(Customer).Name));
            };

            ext.BeforeBuildMappingsDelegate = (mapping) =>
            {
                Assert.IsNotNull(mapping);
                Assert.AreEqual(4, mapping.Items.Count());
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(City).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(State).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(ZipCode).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(Customer).Name));
            };

            ext.AfterBuildMappingsDelegate = (mapping) =>
            {
                Assert.IsNotNull(mapping);
                Assert.AreEqual(4, mapping.Items.Count());
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(City).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(State).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(ZipCode).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(Customer).Name));
            };

            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .RegisterExtension(ext)
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        /// <summary>
        /// Tests the AddExtension method
        /// </summary>
        [TestMethod]
        public void RegisterExtensionCompileConfigTest()
        {
            var ext = new TestExtension();
            ext.BeforeCompileMappingsDelegate = (types) =>
            {
                Assert.IsNotNull(types);
                Assert.AreEqual(3, types.Count);
                Assert.IsTrue(types.Contains(typeof(City)));
                Assert.IsTrue(types.Contains(typeof(State)));
                Assert.IsTrue(types.Contains(typeof(ZipCode)));
            };

            ext.AfterCompileMappingsDelegate = (types, mapping) =>
            {
                Assert.IsNotNull(types);
                Assert.AreEqual(3, types.Count);
                Assert.IsTrue(types.Contains(typeof(City)));
                Assert.IsTrue(types.Contains(typeof(State)));
                Assert.IsTrue(types.Contains(typeof(ZipCode)));

                Assert.IsNotNull(mapping);
                Assert.AreEqual(3, mapping.Items.Count());
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(City).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(State).Name));
                Assert.IsTrue(mapping.Items.OfType<HbmClass>().Any(c => c.name == typeof(ZipCode).Name));
            };

            ext.BeforeBuildMappingsDelegate = (mapping) =>
            {
                Assert.Fail();
            };

            ext.AfterBuildMappingsDelegate = (mapping) =>
            {
                Assert.Fail();
            };

            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .RegisterExtension(ext)
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .EndConfiguration()
                .CompileMappings(NHConfig);
        }

        /// <summary>
        /// Tests the AddExtension method
        /// </summary>
        [TestMethod]
        public void RegisterExtensionNoExecutedTest()
        {
            var ext = new TestExtension();
            ext.BeforeCompileMappingsDelegate = (types) =>
            {
                Assert.Fail();
            };

            ext.AfterCompileMappingsDelegate = (types, mapping) =>
            {
                Assert.Fail();
            };

            ext.BeforeBuildMappingsDelegate = (mapping) =>
            {
                Assert.Fail();
            };

            ext.AfterBuildMappingsDelegate = (mapping) =>
            {
                Assert.Fail();
            };

            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .RegisterExtension(ext)
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
               .EndConfiguration();
        }
    }
}
