using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the database schema config
    /// </summary>
    [TestClass]
    public class SchemaTests : BaseDatabaseTest
    {
        #region Tests

        /// <summary>
        /// Tests the schema convention when the schema name is the assembly name
        /// </summary>
        [TestMethod]
        public void SchemaNamingConventionAssemblyNameTest()
        {
            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForSchemaNames(SchemasNamingConvention.AssemblyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Schema == "tests" && t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Schema == "tests" && t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Schema == "tests" && t.Name == "ZipCode"));
        }

        /// <summary>
        /// Tests the schema convention
        /// </summary>
        [TestMethod]
        public void SchemaNamingConventionDefaultNameTest()
        {
            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForSchemaNames(SchemasNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Schema == null && t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Schema == null && t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Schema == null && t.Name == "ZipCode"));
        }

        /// <summary>
        /// Tests the schema convention
        /// </summary>
        [TestMethod]
        public void SchemaNamingConventionNamespaceNameTest()
        {
            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForSchemaNames(SchemasNamingConvention.NamespaceName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Schema == "model" && t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Schema == "model" && t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Schema == "model" && t.Name == "ZipCode"));
        }

        /// <summary>
        /// Tests the schema convention
        /// </summary>
        [TestMethod]
        public void SchemaNamingConventionCustomNameTest()
        {
            var entities = new List<Type>() { typeof(City), typeof(State), typeof(ZipCode) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseCustomConventionForSchemaNames(t => "entities")
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var schemaExport = new SchemaExport(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Schema == "entities" && t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Schema == "entities" && t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Schema == "entities" && t.Name == "ZipCode"));
        }

        #endregion
    }
}
