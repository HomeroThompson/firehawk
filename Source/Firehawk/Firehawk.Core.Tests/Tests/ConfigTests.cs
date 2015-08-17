using Fhwk.Core.Naming;
using Fhwk.Core.Tests.Common.Data;
using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model.ConfigModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the configuration options
    /// </summary>
    [TestClass]
    public class ConfigTests : BaseDatabaseTest
    {
        #region Firehawk Config

        /// <summary>
        /// Tests the minimal settings config
        /// </summary>
        [TestMethod]
        public void MinSettingsConfigTest()
        {
            Firehawk.Init()
                .Configure()
                    .ConfigureEntities()
                        .AddBaseEntity<BaseEntityA>()
                        .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("MyProject.Domain"))
                    .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
                    .EndConfig()
                .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        #endregion

        #region Entity Definitions Config

        /// <summary>
        /// Tests the AddBaseEntity method
        /// </summary>
        [TestMethod]
        public void AddBaseEntityATest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));

        }

        /// <summary>
        /// Tests the AddBaseEntity method
        /// </summary>
        [TestMethod]
        public void AddBaseEntityBTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityB>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));

        }

        /// <summary>
        /// Tests the AddBaseEntity method
        /// </summary>
        [TestMethod]
        public void AddBaseEntityNoBaseEntityTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityB>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityC>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        /// <summary>
        /// Tests the AddBaseEntity method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void AddBaseEntityNoMappingTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityB>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityC>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        /// <summary>
        /// Tests the AddBaseEntity method
        /// </summary>
        [TestMethod]
        public void AddBaseAllEntitiesEntityTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .AddBaseEntity<BaseEntityB>()
                       .AddBaseEntity<BaseEntityC>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityB>(m => m.Id(e => e.ID))
                   .AddCustomMapping<BaseEntityC>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityB1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityB2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityB3"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityC1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityC2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityC3"));

        }

        #endregion

        #region Source Assemblies Config

        /// <summary>
        /// Tests the SeachForEntitiesOnThisAssembly method
        /// </summary>
        [TestMethod]
        public void SeachForEntitiesOnThisAssemblyTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));

        }

        /// <summary>
        /// Tests the SeachForEntitiesOnThisAssembly method when there are no entities
        /// </summary>
        [TestMethod]
        public void SeachForEntitiesOnThisAssemblyNoEntitiesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(typeof(Int32).Assembly)
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        /// <summary>
        /// Tests the SeachForEntitiesOnTheseAssemblies method
        /// </summary>
        [TestMethod]
        public void SeachForEntitiesOnTheseAssembliesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));

        }

        /// <summary>
        /// Tests the SeachForEntitiesOnTheseAssemblies method when there is no assembly that matches the filter
        /// </summary>
        [TestMethod]
        public void SeachForEntitiesOnTheseAssembliesNoEntitiesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("ZZZZZ"))
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        /// <summary>
        /// Tests the ExcludeEntitiesFromTheseAssemblies method 
        /// </summary>
        [TestMethod]
        public void ExcludeEntitiesFromTheseAssembliesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .ExcludeEntitiesFromTheseAssemblies(a => a.FullName.StartsWith("Fire"))
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        /// <summary>
        /// Tests the ExcludeEntitiesFromThisAssembly method 
        /// </summary>
        [TestMethod]
        public void ExcludeEntitiesFromThisAssemblyTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .ExcludeEntitiesFromThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                   .AddCustomMapping<BaseEntityA>(m => m.Id(e => e.ID))
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 0);
        }

        #endregion

        #region Mapping Types Config

        /// <summary>
        /// Tests the SeachForMappingsOnThisAssembly method
        /// </summary>
        [TestMethod]
        public void SeachForMappingsOnThisAssemblyTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));
        }

        /// <summary>
        /// Tests the AddMappingFilter method
        /// </summary>
        [TestMethod]
        public void AddMappingFilterTest()
        {
            var mappings = Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                       .AddMappingFilter(m => m.Name.EndsWith("Map"))
                       .EndConfig()
                   .EndConfiguration()
                .CompileMappings(NHConfig);

            Assert.IsNotNull(mappings);
            Assert.IsTrue(mappings.Items.Length == 3);
        }

        /// <summary>
        /// Tests the AddMappingFilter method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void AddMappingFilterNoItemsTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                       .AddMappingFilter(m => !m.Name.EndsWith("Map"))
                       .EndConfig()
                   .EndConfiguration()
                .CompileMappings(NHConfig);
        }

        /// <summary>
        /// Tests the SeachForMappingsOnThisAssembly method when there are no entities
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void SeachForMappingsOnThisAssemblyNoMappingsTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnThisAssembly(typeof(Int32).Assembly)
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        /// <summary>
        /// Tests the SeachForMappingsOnTheseAssemblies method
        /// </summary>
        [TestMethod]
        public void SeachForMappingsOnTheseAssembliesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);

            IList<Table> tables = GetTables();

            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA2"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA3"));

        }

        /// <summary>
        /// Tests the SeachForMappingsOnTheseAssemblies method when there is no assembly that matches the filter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void SeachForMappingsOnTheseAssembliesNoMappingsTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnTheseAssemblies(a => a.FullName.StartsWith("ZZZZZ"))
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        /// <summary>
        /// Tests the ExcludeMappingsFromTheseAssemblies method 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ExcludeMappingsFromTheseAssembliesTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .ExcludeMappingsFromTheseAssemblies(a => a.FullName.StartsWith("Fire"))
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        /// <summary>
        /// Tests the ExcludeMappingsFromThisAssembly method 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ExcludeMappingsFromThisAssemblyTest()
        {
            Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnTheseAssemblies(a => a.FullName.StartsWith("Firehawk"))
                       .ExcludeMappingsFromThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                .BuildMappings(NHConfig);
        }

        #endregion

        #region Compile Mappings

        /// <summary>
        /// Tests the CompileMappings method
        /// </summary>
        [TestMethod]
        public void CompileMappingsTest()
        {
            var hbmMappings =
               Firehawk.Init()
               .Configure()
                   .ConfigureEntities()
                       .AddBaseEntity<BaseEntityA>()
                       .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                    .ConfigureMappings()
                       .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                       .EndConfig()
                   .EndConfiguration()
                .CompileMappings(NHConfig);

            Assert.IsNotNull(hbmMappings);
            Assert.IsTrue(hbmMappings.RootClasses.Length == 3);
        }

        #endregion

        #region Model Mapper

        /// <summary>
        /// Tests the GetModelMapper method
        /// </summary>
        [TestMethod]
        public void GetModelMapperTest()
        {
            Firehawk.Init()
            .Configure()
                .ConfigureEntities()
                    .AddBaseEntity<BaseEntityA>()
                    .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                 .ConfigureMappings()
                    .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                .EndConfiguration()
                .GetModelMapper(m => { Assert.IsNotNull(m); })
                .BuildMappings(NHConfig);
        }

        #endregion
    }
}
