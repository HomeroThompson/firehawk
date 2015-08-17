using Fhwk.Core.Naming;
using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ManyToManyModel = Fhwk.Core.Tests.Model.ManyToMany;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the foreign key names config
    /// </summary>
    [TestClass]
    public class ForeignKeyTests : BaseDatabaseTest
    {
        #region Many To One

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionDefaultTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.All(c => c.StartsWith("FK")));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFK_FKTable_PKTableTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__City__State"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__City__ZipCode"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "FK__ZipCode__City"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable_PKColumn
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFK_FKTable_PKTable_PKColumnTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__City__State__State"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__City__ZipCode__ZipCode"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "FK__ZipCode__City__City"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_FK
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFKTable_PKTable_FKTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "City__State__FK"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "City__ZipCode__FK"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "ZipCode__City__FK"));
        }


        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_PKColumn_FK
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFKTable_PKTable_PKColumn_FKTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "City__State__State__FK"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "City__ZipCode__ZipCode__FK"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "ZipCode__City__City__FK"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionCustomTest()
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
                        .UseCustomConventionForForeignKeyNames((f, p, c, i) => string.Format("foreign_{0}_{1}_{2}", f.Name, p.Name, c.Name))
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "foreign_City_State_State"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "foreign_City_ZipCode_ZipCode"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "foreign_ZipCode_City_City"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is the default one
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFK_TableNameDefaultConstraintNameTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c.StartsWith("FK")));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Lowercase
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFK_TableNameLowercaseConstraintNameTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "fk__city__state"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "fk__city__zip_code"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "fk__zip_code__city"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Uppercase
        /// </summary>
        [TestMethod]
        public void ManyToOneForeignKeyNamingConventionFK_TableNameUppercaseConstraintNameTest()
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
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.ForeignKeys.Count == 2);
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__CITY__STATE__STATE"));
            Assert.IsTrue(cityTable.ForeignKeys.Any(c => c == "FK__CITY__ZIP_CODE__ZIP_CODE"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.ForeignKeys.Count == 0);

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.ForeignKeys.Count == 1);
            Assert.IsTrue(zipCodeTable.ForeignKeys.Any(c => c == "FK__ZIP_CODE__CITY__CITY"));
        }


        #endregion

        #region One To Many

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionDefaultTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(c => c.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFK_FKTable_PKTableTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "FK__CustomerTelephone__Customer"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable_PKColumn
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFK_FKTable_PKTable_PKColumnTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "FK__CustomerTelephone__Customer__ID"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_FK
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFKTable_PKTable_FKTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "CustomerTelephone__Customer__FK"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_PKColumn_FK
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFKTable_PKTable_PKColumn_FKTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();


            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "CustomerTelephone__Customer__ID__FK"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionCustomTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseCustomConventionForForeignKeyNames((f, p, c, i) => string.Format("foreign_{0}_{1}_{2}", f.Name, p.Name, c.Name, i.Name))
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "foreign_Telephone_Customer_Telephones"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is the default one
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFK_TableNameDefaultConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Default)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();


            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Lowercase
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFK_TableNameLowercaseConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();


            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "fk__customer_telephone__customer"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Uppercase
        /// </summary>
        [TestMethod]
        public void OneToManyForeignKeyNamingConventionFK_TableNameUppercaseConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(Customer), typeof(City), typeof(State), typeof(ZipCode) };

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
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Uppercase)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();


            Assert.IsTrue(tables.Count == 5);

            var customerTelephoneTable = tables.SingleOrDefault(t => t.Name == "CustomerTelephone");
            Assert.IsNotNull(customerTelephoneTable);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.Count == 1);
            Assert.IsTrue(customerTelephoneTable.ForeignKeys.All(f => f == "FK__CUSTOMER_TELEPHONE__CUSTOMER__ID"));
        }

        #endregion

        #region Many To Many

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionDefaultTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.All(c => c.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFK_FKTable_PKTableTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CategoryProduct__Category"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CategoryProduct__Product"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_FKTable_PKTable_PKColumn
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFK_FKTable_PKTable_PKColumnTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CategoryProduct__Category__ID"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CategoryProduct__Product__ID"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_FK
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFKTable_PKTable_FKTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "CategoryProduct__Category__FK"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "CategoryProduct__Product__FK"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FKTable_PKTable_PKColumn_FK
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFKTable_PKTable_PKColumn_FKTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "CategoryProduct__Category__ID__FK"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "CategoryProduct__Product__ID__FK"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionCustomTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseCustomConventionForForeignKeyNames((f, p, c, i) => string.Format("foreign_{0}_{1}_{2}", f.Name, p.Name, c.Name, i.Name))
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "foreign_Product_Category_Product"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "foreign_Product_Category_Category"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is the default one
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFK_TableNameDefaultConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Default)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c.StartsWith("FK")));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c.StartsWith("FK")));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Lowercase
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFK_TableNameLowercaseConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "fk__category_product__category"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "fk__category_product__product"));
        }

        /// <summary>
        /// Tests the primary key constraints naming convention when the convention is FK_TableName and the convention for constraint names is Uppercase
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyNamingConventionFK_TableNameUppercaseConstraintNameTest()
        {
            var entities = new List<Type>() { typeof(ManyToManyModel.Category), typeof(ManyToManyModel.Product) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ManyToManyModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForConstraintNames(ConstraintNamingConvention.Uppercase)
                        .UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var categoryProductTable = tables.SingleOrDefault(t => t.Name == "CategoryProduct");
            Assert.IsNotNull(categoryProductTable);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Count == 2);
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CATEGORY_PRODUCT__CATEGORY__ID"));
            Assert.IsTrue(categoryProductTable.ForeignKeys.Any(c => c == "FK__CATEGORY_PRODUCT__PRODUCT__ID"));
        }

        #endregion
    }
}
