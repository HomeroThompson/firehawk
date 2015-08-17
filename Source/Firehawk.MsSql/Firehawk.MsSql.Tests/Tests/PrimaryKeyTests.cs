using Fhwk.Core;
using Fhwk.MsSql.Tests.Common.Tests;
using Fhwk.MsSql.Tests.Model;
using Fhwk.MsSql.Tests.Model.Schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Fhwk.MsSql.Tests.Tests
{
    [TestClass]
    public class PrimaryKeyTests : BaseSqlServerTest
    {
        #region Primary Key Names

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionDefaultTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey.StartsWith("PK__City__"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey.StartsWith("PK__State__"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey.StartsWith("PK__ZipCode__"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableNameTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "PK__City");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "PK__State");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "PK__ZipCode");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName_ColumnName
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableName_ColumnNameTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName_ColumnName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "PK__City__ID");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "PK__State__ID");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "PK__ZipCode__ID");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is TableName_ColumnName_PK
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionTableName_ColumnName_PKTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.TableName_ColumnName_PK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "City__ID__PK");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "State__ID__PK");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "ZipCode__ID__PK");
        }


        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is TableName_PK
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionTableName_PKTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.TableName_PK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "City__PK");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "State__PK");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "ZipCode__PK");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionCustomTest()
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
                        .UseCustomConventionForPrimaryKeyNames((t, c) => string.Format("key_{0}_{1}", t.Name, c.Name))
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "key_City_ID");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "key_State_ID");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "key_ZipCode_ID");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is custom but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void PrimaryKeyNamingConventionCustomErrorTest()
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
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.Custom)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName and the convention for constraint names is the default one
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableNameDefaultConstraintNameTest()
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
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Default)
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "PK__City");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "PK__State");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "PK__ZipCode");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName and the convention for constraint names is Lowercase
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableNameLowercaseConstraintNameTest()
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
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase)
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "pk__city");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "pk__state");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "pk__zip_code");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName and the convention for constraint names is Uppercase
        /// </summary>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableNameUppercaseConstraintNameTest()
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
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Uppercase)
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "PK__CITY");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "PK__STATE");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "PK__ZIP_CODE");
        }

        #endregion

        #region Primary Keys and Schemas

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName_ColumnName and the entities belong to different schemas
        /// </summary>
        /// <remarks>
        /// Precondition-> The following schemas must exist on the target database: 'SchemaA', 'SchemaB' and 'SchemaC'
        /// </remarks>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableName_ColumnNameWithSchemaTest()
        {
            var entities = new List<Type>() { typeof(EntityA1), typeof(EntityB1), typeof(EntityC1) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<BaseEntityA>()
                        .AddBaseEntity<BaseEntityB>()
                        .AddBaseEntity<BaseEntityC>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName_ColumnName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var entityA1Table = tables.Single(t => t.Name == "EntityA1");
            Assert.IsTrue(entityA1Table.PrimaryKey == "PK__EntityA1__ID");

            var entityB1Table = tables.Single(t => t.Name == "EntityB1");
            Assert.IsTrue(entityB1Table.PrimaryKey == "PK__EntityB1__ID");

            var entityC1Table = tables.Single(t => t.Name == "EntityC1");
            Assert.IsTrue(entityC1Table.PrimaryKey == "PK__EntityC1__ID");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName_ColumnName and the entities belong to different schemas
        /// </summary>
        /// <remarks>
        /// Precondition-> The following schemas must exist on the target database: 'SchemaA', 'SchemaB' and 'SchemaC'
        /// </remarks>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_TableName_ColumnNameWithMultipleSchemaTest()
        {
            var entities = new List<Type>() {  typeof(City), typeof(State), typeof(ZipCode), typeof(EntityA1), typeof(EntityB1), typeof(EntityC1) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddBaseEntity<BaseEntityA>()
                        .AddBaseEntity<BaseEntityB>()
                        .AddBaseEntity<BaseEntityC>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName_ColumnName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 6);

            var entityA1Table = tables.Single(t => t.Name == "EntityA1");
            Assert.IsTrue(entityA1Table.PrimaryKey == "PK__EntityA1__ID");

            var entityB1Table = tables.Single(t => t.Name == "EntityB1");
            Assert.IsTrue(entityB1Table.PrimaryKey == "PK__EntityB1__ID");

            var entityC1Table = tables.Single(t => t.Name == "EntityC1");
            Assert.IsTrue(entityC1Table.PrimaryKey == "PK__EntityC1__ID");

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.PrimaryKey == "PK__City__ID");

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.PrimaryKey == "PK__State__ID");

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.PrimaryKey == "PK__ZipCode__ID");
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is PK_TableName_ColumnName
        /// </summary>
        /// <remarks>
        /// Precondition-> The following schemas must exist on the target database: 'SchemaA', 'SchemaB' and 'SchemaC'
        /// </remarks>
        [TestMethod]
        public void PrimaryKeyNamingConventionPK_CustomTableNameWithSchemaTest()
        {
            var entities = new List<Type>() { typeof(EntityA1), typeof(EntityB1), typeof(EntityC1) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<BaseEntityA>()
                        .AddBaseEntity<BaseEntityB>()
                        .AddBaseEntity<BaseEntityC>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForPrimaryKeyNames(PrimaryKeyNamingConvention.PK_TableName_ColumnName)
                        .UseCustomConventionForTableNames(t => string.Format("_{0}_", t.Name))
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            Assert.IsTrue(tables.Count == 3);

            var entityA1Table = tables.Single(t => t.Name == "_EntityA1_");
            Assert.IsTrue(entityA1Table.PrimaryKey == "PK__EntityA1__ID");

            var entityB1Table = tables.Single(t => t.Name == "_EntityB1_");
            Assert.IsTrue(entityB1Table.PrimaryKey == "PK__EntityB1__ID");

            var entityC1Table = tables.Single(t => t.Name == "_EntityC1_");
            Assert.IsTrue(entityC1Table.PrimaryKey == "PK__EntityC1__ID");
        }
        
        #endregion
    }
}
