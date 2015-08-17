using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ElementModel = Fhwk.Core.Tests.Model.Elements;
using InheritanceModel = Fhwk.Core.Tests.Model.Inheritance;
using ManyToManyModel = Fhwk.Core.Tests.Model.ManyToMany;
using SchemaModel = Fhwk.Core.Tests.Model.Schemas;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the database tables config
    /// </summary>
    [TestClass]
    public class TableTests : BaseDatabaseTest
    {
        #region Entities

        /// <summary>
        /// Tests the table naming convention when the convention is Pascal Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionPascalCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
        }

        /// <summary>
        /// Tests the table naming convention when the convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionCamelCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.CamelCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "city"));
            Assert.IsTrue(tables.Any(t => t.Name == "state"));
            Assert.IsTrue(tables.Any(t => t.Name == "zipCode"));
        }

        /// <summary>
        /// Tests the table naming convention when the convention is Lowercase Underscore Separated
        /// </summary>
        [TestMethod]
        public void TableNamingConventionLowercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Lowercase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "city"));
            Assert.IsTrue(tables.Any(t => t.Name == "state"));
            Assert.IsTrue(tables.Any(t => t.Name == "zip_code"));
        }

        /// <summary>
        /// Tests the table naming convention when the convention is Uppercase Underscore Separated
        /// </summary>
        [TestMethod]
        public void TableNamingConventionUppercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "CITY"));
            Assert.IsTrue(tables.Any(t => t.Name == "STATE"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZIP_CODE"));
        }

        /// <summary>
        /// Tests the table naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void TableNamingConventionCustomConventionTest()
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
                        .UseCustomConventionForTableNames(t => string.Format("_{0}_", t.Name).ToLower())
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();
                      
            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "_city_"));
            Assert.IsTrue(tables.Any(t => t.Name == "_state_"));
            Assert.IsTrue(tables.Any(t => t.Name == "_zipcode_"));
        }

        /// <summary>
        /// Tests the table naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void TableNamingConventionDefaultTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
        }

        #endregion

        #region Components

        /// <summary>
        /// Tests the table naming convention for components when the convention is Pascal Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionComponentNamePascalCaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrder"));
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "OrderItem"));
            Assert.IsTrue(tables.Any(t => t.Name == "Customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "Telephone"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is EntityName_ComponentName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionComponentEntityName_ComponentNameCamelCaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.CamelCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityName_ComponentName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "zipCode"));
            Assert.IsTrue(tables.Any(t => t.Name == "state"));
            Assert.IsTrue(tables.Any(t => t.Name == "city"));
            Assert.IsTrue(tables.Any(t => t.Name == "productsOrder"));
            Assert.IsTrue(tables.Any(t => t.Name == "product"));
            Assert.IsTrue(tables.Any(t => t.Name == "category"));
            Assert.IsTrue(tables.Any(t => t.Name == "productsOrder_orderItem"));
            Assert.IsTrue(tables.Any(t => t.Name == "customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "customer_telephone"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is EntityName_RelationshipName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionComponentEntityName_RelationshipNameUppercaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode) , typeof(Customer)};

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
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityName_RelationshipName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "PRODUCTS_ORDER_ITEMS"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZIP_CODE"));
            Assert.IsTrue(tables.Any(t => t.Name == "STATE"));
            Assert.IsTrue(tables.Any(t => t.Name == "CITY"));
            Assert.IsTrue(tables.Any(t => t.Name == "PRODUCTS_ORDER"));
            Assert.IsTrue(tables.Any(t => t.Name == "PRODUCT"));
            Assert.IsTrue(tables.Any(t => t.Name == "CATEGORY"));
            Assert.IsTrue(tables.Any(t => t.Name == "CUSTOMER"));
            Assert.IsTrue(tables.Any(t => t.Name == "CUSTOMER_TELEPHONES"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is EntityNameComponentName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionEntityNameComponentNameLowercaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.Lowercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "products_order_order_item"));
            Assert.IsTrue(tables.Any(t => t.Name == "zip_code"));
            Assert.IsTrue(tables.Any(t => t.Name == "state"));
            Assert.IsTrue(tables.Any(t => t.Name == "city"));
            Assert.IsTrue(tables.Any(t => t.Name == "products_order"));
            Assert.IsTrue(tables.Any(t => t.Name == "product"));
            Assert.IsTrue(tables.Any(t => t.Name == "category"));
            Assert.IsTrue(tables.Any(t => t.Name == "customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "customer_telephone"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is EntityNameRelationshipName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionEntityNameRelationshipNamePascalCaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrderItems"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrder"));
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "CustomerTelephones"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is RelationshipName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionRelationshipNamePascalCaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.RelationshipName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "Items"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrder"));
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "Telephones"));
        }

        /// <summary>
        /// Tests the table naming convention for components when the convention is EntityNameRelationshipName and the column convention is Camel Case
        /// </summary>
        [TestMethod]
        public void TableNamingConventionEntityName_RelationshipNamePascalCaseTest()
        {
            var entities = new List<Type>() { typeof(ProductsOrder), typeof(Category), typeof(Product), typeof(City), typeof(State), typeof(ZipCode), typeof(Customer) };

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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityName_RelationshipName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 9);
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrder_Items"));
            Assert.IsTrue(tables.Any(t => t.Name == "ZipCode"));
            Assert.IsTrue(tables.Any(t => t.Name == "State"));
            Assert.IsTrue(tables.Any(t => t.Name == "City"));
            Assert.IsTrue(tables.Any(t => t.Name == "ProductsOrder"));
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Customer"));
            Assert.IsTrue(tables.Any(t => t.Name == "Customer_Telephones"));
        }

        #endregion

        #region Elements

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is ElementTypeName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameElementTypeNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.ElementTypeName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "String"));
            Assert.IsTrue(tables.Any(t => t.Name == "Int32"));
            Assert.IsTrue(tables.Any(t => t.Name == "Guid"));
            Assert.IsTrue(tables.Any(t => t.Name == "KeyValuePair"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is ElementTypeName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameElementTypeNamePascalCaseTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.ElementTypeName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "String"));
            Assert.IsTrue(tables.Any(t => t.Name == "Int32"));
            Assert.IsTrue(tables.Any(t => t.Name == "Guid"));
            Assert.IsTrue(tables.Any(t => t.Name == "KeyValuePair"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is PropertyName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNamePropertyNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "ListOfString"));
            Assert.IsTrue(tables.Any(t => t.Name == "ListOfInt"));
            Assert.IsTrue(tables.Any(t => t.Name == "ListOfGuid"));
            Assert.IsTrue(tables.Any(t => t.Name == "DictionaryLongString"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is EntityNamePropertyName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameEntityNamePropertyNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityNamePropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementListOfString"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementListOfInt"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementListOfGuid"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementDictionaryLongString"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is EntityName_PropertyName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameEntityName_PropertyNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_ListOfString"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_ListOfInt"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_ListOfGuid"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_DictionaryLongString"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is EntityNameElementName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameEntityNameElementNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityNameElementName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementString"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementInt32"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementGuid"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementKeyValuePair"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is EntityName_ElementName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameEntityName_ElementNameTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_ElementName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_String"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_Int32"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_Guid"));
            Assert.IsTrue(tables.Any(t => t.Name == "Element_KeyValuePair"));
        }

        /// <summary>
        /// Tests the convention used when mapping elements to database tables names and the selected convention is Custom
        /// </summary>
        [TestMethod]
        public void TableNamingConventionElementNameCustomTest()
        {
            var entities = new List<Type>() { typeof(ElementModel.Element) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<ElementModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseCustomConventionForElementTableNames((ent, ele, prop) =>
                        {
                            return string.Concat(ent.Name, ele.Name.Replace("`", ""), prop.Name);
                        })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "Element"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementStringListOfString"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementInt32ListOfInt"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementGuidListOfGuid"));
            Assert.IsTrue(tables.Any(t => t.Name == "ElementKeyValuePair2DictionaryLongString"));
        }

        #endregion

        #region Many to Many

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is the Default one
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyDefaultPascalCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category_Product"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is FirstTableNameSecondTableName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyFirstTableNameSecondTableNamePascalCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "CategoryProduct"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is FirstTableNameToSecondTableName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyFirstTableNameToSecondTableNamePascalCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition.RelationshipPropertyNamesEqualToPluralizedEntityNames)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameToSecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "CategoryToProduct"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is the Default one
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyDefaultCamelCaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.CamelCase)
                        .IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition.RelationshipPropertyNamesEqualToPluralizedEntityNames)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "product"));
            Assert.IsTrue(tables.Any(t => t.Name == "category"));
            Assert.IsTrue(tables.Any(t => t.Name == "category_product"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is FirstTableName_SecondTableName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyFirstTableName_SecondTableNameUppercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition.RelationshipPropertyNamesEqualToPluralizedEntityNames)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "PRODUCT"));
            Assert.IsTrue(tables.Any(t => t.Name == "CATEGORY"));
            Assert.IsTrue(tables.Any(t => t.Name == "CATEGORY_PRODUCT"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is Lowercase
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyFirstTableName_SecondTableNameLowercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Lowercase)
                        .IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition.RelationshipPropertyNamesEqualToPluralizedEntityNames)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "product"));
            Assert.IsTrue(tables.Any(t => t.Name == "category"));
            Assert.IsTrue(tables.Any(t => t.Name == "category_product"));
        }

        /// <summary>
        /// Tests the table naming convention for many to many relationships when the convention is FirstTableNameToSecondTableName
        /// </summary>
        [TestMethod]
        public void TableNamingConventionManyToManyCustomPascalCaseTest()
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
                        .UseCustomConventionForTableNames(s => string.Format("_{0}_", s.Name))
                        .IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition.RelationshipPropertyNamesEqualToPluralizedEntityNames)
                        .UseCustomConventionForManyToManyTableNames((t1, t2) =>
                        {
                            return string.Format("_{0}To{1}_", t1.Name, t2.Name);
                        })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "_Product_"));
            Assert.IsTrue(tables.Any(t => t.Name == "_Category_"));
            Assert.IsTrue(tables.Any(t => t.Name == "_CategoryToProduct_"));
        }
                       
        #endregion

        #region Inheritance

        /// <summary>
        /// Tests the table naming convention for inheritance joined sub class mapping
        /// </summary>
        [TestMethod]
        public void TableNamingConventionJoinedSubClassMap()
        {
            var entities = new List<Type>() { typeof(InheritanceModel.Chief), typeof(InheritanceModel.Employee), typeof(InheritanceModel.Manager) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<InheritanceModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Chief"));
            Assert.IsTrue(tables.Any(t => t.Name == "Employee"));
            Assert.IsTrue(tables.Any(t => t.Name == "Manager"));
        }

        /// <summary>
        /// Tests the table naming convention for inheritance union sub class mapping
        /// </summary>
        [TestMethod]
        public void TableNamingConventionUnionsubClassMap()
        {
            var entities = new List<Type>() { typeof(InheritanceModel.Vehicle), typeof(InheritanceModel.Bike), typeof(InheritanceModel.Car) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<InheritanceModel.Entity>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Bike"));
            Assert.IsTrue(tables.Any(t => t.Name == "Car"));
        }

        #endregion

        #region Schema Names

        /// <summary>
        /// Tests the table naming convention when the entities are mapped to different database schemas
        /// </summary>
        [TestMethod]
        public void TableNamingConventionWithSchemas()
        {
            var entities = new List<Type>() {typeof(State), typeof(SchemaModel.EntityA1), typeof(SchemaModel.EntityB1), typeof(SchemaModel.EntityC1) };

            Firehawk.Init()
                .Configure()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                    .EndConfig()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .AddBaseEntity<SchemaModel.BaseEntityA>()
                        .AddBaseEntity<SchemaModel.BaseEntityB>()
                        .AddBaseEntity<SchemaModel.BaseEntityC>()
                        .AddEntities(entities)
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 4);
            Assert.IsTrue(tables.Any(t => t.Name == "State" && t.Schema == null));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityA1" && t.Schema == "SchemaA"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityB1" && t.Schema == "SchemaB"));
            Assert.IsTrue(tables.Any(t => t.Name == "EntityC1" && t.Schema == "SchemaC"));
        }

        #endregion
    }
}
