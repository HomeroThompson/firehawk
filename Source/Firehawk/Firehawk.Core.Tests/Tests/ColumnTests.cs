using Fhwk.Core.Naming;
using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ManyToManyModel = Fhwk.Core.Tests.Model.ManyToMany;
using InheritanceModel = Fhwk.Core.Tests.Model.Inheritance;
using ElementModel = Fhwk.Core.Tests.Model.Elements;
using System.Configuration;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the table columns config
    /// </summary>
    [TestClass]
    public class ColumnTests : BaseDatabaseTest
    {
        #region Entities Table Columns

        /// <summary>
        /// Tests the columns naming convention when the convention is Pascal Case
        /// </summary>
        [TestMethod]
        public void ColumnsNamingConventionPascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the columns naming convention when the convention is Camel Case
        /// </summary>
        [TestMethod]
        public void ColumnsNamingConventionCamelCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "zipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "state"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "postalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "city"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "version"));
        }

        /// <summary>
        /// Tests the columns naming convention when the convention is Lowercase Underscore Separated
        /// </summary>
        [TestMethod]
        public void ColumnsNamingConventionLowercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "zip_code"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "state"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "postal_code"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "city"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "version"));
        }

        /// <summary>
        /// Tests the columns naming convention when the convention is Uppercase Underscore Separated
        /// </summary>
        [TestMethod]
        public void ColumnsNamingConventionUppercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Uppercase)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "NAME"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZIP_CODE"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "STATE"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "VERSION"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "NAME"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "VERSION"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "POSTAL_CODE"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "CITY"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "VERSION"));
        }

        /// <summary>
        /// Tests the columns naming convention when the convention is a custom function
        /// </summary>
        [TestMethod]
        public void ColumnsNamingConventionCustomConventionTest()
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
                        .UseCustomConventionForColumnNames(c => string.Format("_{0}_", c.Name).ToUpper())
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "_ID_"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "_NAME_"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "_ZIPCODE_"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "_STATE_"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "_VERSION_"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "_ID_"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "_NAME_"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "_VERSION_"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "_ID_"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "_POSTALCODE_"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "_CITY_"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "_VERSION_"));
        }

        #endregion

        #region Elements Table Columns

        /// <summary>
        /// Tests the columns conventions on elements
        /// </summary>
        [TestMethod]
        public void ElementsColumnNamingConventionsTest()
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

            var elementListOfStringTable = tables.Single(t => t.Name == "Element_ListOfString");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "Element_ListOfInt");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "Element_ListOfGuid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "Element_DictionaryLongString");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "IdKey"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Value"));

        }

        /// <summary>
        /// Tests the columns conventions on elements when the selected convention is camel case
        /// </summary>
        [TestMethod]
        public void ElementsColumnNamingConventionsCamelCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
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

            var elementListOfStringTable = tables.Single(t => t.Name == "Element_ListOfString");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "Element_ListOfInt");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "Element_ListOfGuid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "Element_DictionaryLongString");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "idKey"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "value"));

        }

        /// <summary>
        /// Tests the columns conventions on elements when the selected convention is uppercase
        /// </summary>
        [TestMethod]
        public void ElementsColumnNamingConventionsUppercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .UseConventionForColumnNames(ColumnsNamingConvention.Uppercase)
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "ELEMENT"));
            Assert.IsTrue(tables.Any(t => t.Name == "ELEMENT_LIST_OF_STRING"));
            Assert.IsTrue(tables.Any(t => t.Name == "ELEMENT_LIST_OF_INT"));
            Assert.IsTrue(tables.Any(t => t.Name == "ELEMENT_LIST_OF_GUID"));
            Assert.IsTrue(tables.Any(t => t.Name == "ELEMENT_DICTIONARY_LONG_STRING"));

            var elementListOfStringTable = tables.Single(t => t.Name == "ELEMENT_LIST_OF_STRING");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "ELEMENT"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "VALUE"));

            var elementListOfIntTable = tables.Single(t => t.Name == "ELEMENT_LIST_OF_INT");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "ELEMENT"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "VALUE"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "ELEMENT_LIST_OF_GUID");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "ELEMENT"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "VALUE"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "ELEMENT_DICTIONARY_LONG_STRING");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "ELEMENT"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "ID_KEY"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "VALUE"));

        }

        /// <summary>
        /// Tests the columns conventions on elements when the selected convention is lowercase
        /// </summary>
        [TestMethod]
        public void ElementsColumnNamingConventionsLowercaseTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.Lowercase)
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 5);
            Assert.IsTrue(tables.Any(t => t.Name == "element"));
            Assert.IsTrue(tables.Any(t => t.Name == "element_list_of_string"));
            Assert.IsTrue(tables.Any(t => t.Name == "element_list_of_int"));
            Assert.IsTrue(tables.Any(t => t.Name == "element_list_of_guid"));
            Assert.IsTrue(tables.Any(t => t.Name == "element_dictionary_long_string"));

            var elementListOfStringTable = tables.Single(t => t.Name == "element_list_of_string");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "element_list_of_int");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "element_list_of_guid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "element_dictionary_long_string");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "element"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "id_key"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "value"));

        }

        #endregion

        #region Components Table Columns

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is ComponentName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionComponentNamePascalCaseTest()
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
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Street"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "City"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Street"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "City"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is ComponentName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionComponentNameCamelCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "street"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "number"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "city"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "street"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "number"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "city"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is EntityPropertyNameComponentPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionEntityPropertyNameComponentPropertyNameUppercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Uppercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "ADDRESS_STREET"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "ADDRESS_NUMBER"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "ADDRESS_CITY"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "SHIPPING_ADDRESS_STREET"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "SHIPPING_ADDRESS_NUMBER"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "SHIPPING_ADDRESS_CITY"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is EntityPropertyName_ComponentPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionEntityPropertyName_ComponentPropertyNameLowercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "address_street"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "address_number"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "address_city"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "shipping_address_street"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "shipping_address_number"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "shipping_address_city"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is EntityPropertyName_ComponentPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionComponentNamePropertyNamePascalCaseTest()
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
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.ComponentNamePropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "AddressStreet"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "AddressNumber"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "AddressCity"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "AddressStreet"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "AddressNumber"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "AddressCity"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is EntityPropertyName_ComponentPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsColumnNamingConventionComponentName_PropertyNamePascalCaseTest()
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
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.ComponentName_PropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address_Street"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address_Number"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address_City"));

            // Products Order
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrder");

            // Products Order - Address
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Address_Street"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Address_Number"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Address_City"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is custom
        /// </summary>
        [TestMethod]
        public void ComponentsColumnCustomNamingConventionTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .UseCustomConventionForComponentColumnNames((c, p) =>
                            {
                                return string.Format("{0}-{1}", c.Name, p.Name);
                            })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer
            var customerTable = tables.Single(t => t.Name == "Customer");

            // Customer - Address
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address-Street"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address-Number"));
            Assert.IsTrue(customerTable.Columns.Any(c => c == "Address-City"));
        }

        /// <summary>
        /// Tests the column naming convention for components when the selected convention is custom and no custom convention has been set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ComponentsColumnCustomNamingConventionNoConfigTest()
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
                        .UseConventionForTableNames(TablesNamingConvention.PascalCase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.Custom)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);
        }

        #endregion

        #region Foreign Key Columns

        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionDefaultPascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionPropertyNameIdPropertyNamePascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCodeId"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "StateId"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "CityId"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionPropertyNameIdPropertyNameLowercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "zip_code_id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "state_id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "postal_code"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "city_id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "version"));
        }


        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionPropertyName_IdPropertyNamePascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode_Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State_Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City_Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionPropertyName_IdPropertyNameUppercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Uppercase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "NAME"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZIP_CODE_ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "STATE_ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "VERSION"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "NAME"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "VERSION"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "POSTAL_CODE"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "CITY_ID"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "VERSION"));
        }

        /// <summary>
        /// Tests the foreign key columns naming convention when the convention is Custom
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnsNamingConventionCustomPascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseCustomConventionForForeignKeyColumnNames((p, i) =>
                            {
                                return string.Format("{0}-{1}", p.Name, i.Name);
                            })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode-ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State-ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City-ID"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is Default
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionDefaultPascalCaseTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Customer"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ProductsOrder"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ItemIndex"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Product"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionPropertyNameIdPropertyNamePascalCaseTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "CustomerID"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ProductsOrderID"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ItemIndex"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ProductID"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionPropertyNameIdPropertyNameLowercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "customer_id"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "products_order_id"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "item_index"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "product_id"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionPropertyName_IdPropertyNamePascalCaseTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Customer_ID"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ProductsOrder_ID"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ItemIndex"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Product_ID"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionPropertyName_IdPropertyNameLowercaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "customer_id"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "products_order_id"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "item_index"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "product_id"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for components when the selected convention is Custom
        /// </summary>
        [TestMethod]
        public void ComponentsForeignKeyColumnNamingConventionCustonPascalCaseTest()
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
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName)
                        .UseCustomConventionForForeignKeyColumnNames((r, p) =>
                            {
                                return string.Format("{0}-{1}", r.Name, p.Name);
                            })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            // Customer Telephones
            var customerTelephonesTable = tables.Single(t => t.Name == "CustomerTelephones");
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Customer-ID"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Number"));
            Assert.IsTrue(customerTelephonesTable.Columns.Any(c => c == "Type"));

            // Products Order Items
            var productsOrderTable = tables.Single(t => t.Name == "ProductsOrderItems");
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ProductsOrder-ID"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "ItemIndex"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Quantity"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Price"));
            Assert.IsTrue(productsOrderTable.Columns.Any(c => c == "Product-ID"));
        }

        /// <summary>
        /// Tests the foreing key columns conventions on elements when the selected convention is the default one
        /// </summary>
        [TestMethod]
        public void ElementsForeignKeyColumnNamingConventionDefaultTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default)
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

            var elementListOfStringTable = tables.Single(t => t.Name == "Element_ListOfString");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "Element_ListOfInt");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "Element_ListOfGuid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "Element_DictionaryLongString");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Element"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "IdKey"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Value"));

        }

        /// <summary>
        /// Tests the foreing key columns conventions on elements when the selected convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ElementsForeignKeyColumnNamingConventionPropertyNameIdPropertyNameTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
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

            var elementListOfStringTable = tables.Single(t => t.Name == "Element_ListOfString");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "ElementID"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "Element_ListOfInt");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "ElementID"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "Element_ListOfGuid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "ElementID"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "Element_DictionaryLongString");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "ElementID"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "IdKey"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Value"));

        }

        /// <summary>
        /// Tests the foreing key columns conventions on elements when the selected convention is Custom
        /// </summary>
        [TestMethod]
        public void ElementsForeignKeyColumnNamingConventionCustomNameTest()
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
                        .UseCustomConventionForForeignKeyColumnNames((p, i) =>
                            {
                                return string.Format("{0}-{1}", p.Name, i.Name);
                            })
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

            var elementListOfStringTable = tables.Single(t => t.Name == "Element_ListOfString");
            Assert.IsTrue(elementListOfStringTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Element-ID"));
            Assert.IsTrue(elementListOfStringTable.Columns.Any(c => c == "Value"));

            var elementListOfIntTable = tables.Single(t => t.Name == "Element_ListOfInt");
            Assert.IsTrue(elementListOfIntTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Element-ID"));
            Assert.IsTrue(elementListOfIntTable.Columns.Any(c => c == "Value"));

            var elementListOfGuidTable = tables.Single(t => t.Name == "Element_ListOfGuid");
            Assert.IsTrue(elementListOfGuidTable.Columns.Count == 2);
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Element-ID"));
            Assert.IsTrue(elementListOfGuidTable.Columns.Any(c => c == "Value"));

            var elementDictionaryLongStringTable = tables.Single(t => t.Name == "Element_DictionaryLongString");
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Count == 3);
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Element-ID"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "IdKey"));
            Assert.IsTrue(elementDictionaryLongStringTable.Columns.Any(c => c == "Value"));

        }

        /// <summary>
        /// Tests the foreign key column naming convention for many to many relationships when the convention is the Default one
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyColumnNamingConventionDefaultPascalCaseTest()
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

            var categoryTable = tables.Single(t => t.Name == "Category");
            Assert.IsTrue(categoryTable.Columns.Count == 3);
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Version"));

            var productTable = tables.Single(t => t.Name == "Product");
            Assert.IsTrue(productTable.Columns.Count == 4);
            Assert.IsTrue(productTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Description"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Version"));

            var categoryProductTable = tables.Single(t => t.Name == "Category_Product");
            Assert.IsTrue(categoryProductTable.Columns.Count == 2);
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Category_Key"));
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Product_Key"));

        }

        /// <summary>
        /// Tests the foreign key column naming convention for many to many relationships when the convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyColumnNamingConventionPropertyNameIdPropertyNamePascalCaseTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category_Product"));

            var categoryTable = tables.Single(t => t.Name == "Category");
            Assert.IsTrue(categoryTable.Columns.Count == 3);
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Version"));

            var productTable = tables.Single(t => t.Name == "Product");
            Assert.IsTrue(productTable.Columns.Count == 4);
            Assert.IsTrue(productTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Description"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Version"));

            var categoryProductTable = tables.Single(t => t.Name == "Category_Product");
            Assert.IsTrue(categoryProductTable.Columns.Count == 2);
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "CategoryID"));
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "ProductID"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for many to many relationships when the convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyColumnNamingConventionPropertyName_IdPropertyNamePascalCaseTest()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category_Product"));

            var categoryTable = tables.Single(t => t.Name == "Category");
            Assert.IsTrue(categoryTable.Columns.Count == 3);
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Version"));

            var productTable = tables.Single(t => t.Name == "Product");
            Assert.IsTrue(productTable.Columns.Count == 4);
            Assert.IsTrue(productTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Description"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Version"));

            var categoryProductTable = tables.Single(t => t.Name == "Category_Product");
            Assert.IsTrue(categoryProductTable.Columns.Count == 2);
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Category_ID"));
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Product_ID"));
        }

        /// <summary>
        /// Tests the foreign key column naming convention for many to many relationships when the convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ManyToManyForeignKeyColumnNamingConventionCustomPascalCaseTest()
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
                        .UseCustomConventionForForeignKeyColumnNames((p, i) =>
                            {
                                return string.Format("{0}-{1}", p.Name, i.Name);
                            })
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Product"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category"));
            Assert.IsTrue(tables.Any(t => t.Name == "Category_Product"));

            var categoryTable = tables.Single(t => t.Name == "Category");
            Assert.IsTrue(categoryTable.Columns.Count == 3);
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(categoryTable.Columns.Any(c => c == "Version"));

            var productTable = tables.Single(t => t.Name == "Product");
            Assert.IsTrue(productTable.Columns.Count == 4);
            Assert.IsTrue(productTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Description"));
            Assert.IsTrue(productTable.Columns.Any(c => c == "Version"));

            var categoryProductTable = tables.Single(t => t.Name == "Category_Product");
            Assert.IsTrue(categoryProductTable.Columns.Count == 2);
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Category-ID"));
            Assert.IsTrue(categoryProductTable.Columns.Any(c => c == "Product-ID"));
        }

        /// <summary>
        /// Tests the table naming convention for inheritance joined sub class mapping when the selected convention is the default one
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnNamingConventionJoinedSubClassDefaultMap()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Chief"));
            Assert.IsTrue(tables.Any(t => t.Name == "Employee"));
            Assert.IsTrue(tables.Any(t => t.Name == "Manager"));

            var employeeTable = tables.Single(t => t.Name == "Employee");
            Assert.IsTrue(employeeTable.Columns.Count == 5);
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "FirstName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "LastName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "Version"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "DateOfBirth"));

            var chiefTable = tables.Single(t => t.Name == "Chief");
            Assert.IsTrue(chiefTable.Columns.Count == 2);
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "Employee"));
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "Phone"));

            var managerTable = tables.Single(t => t.Name == "Manager");
            Assert.IsTrue(managerTable.Columns.Count == 3);
            Assert.IsTrue(managerTable.Columns.Any(c => c == "Employee"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "Department"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "OfficeNumber"));
        }

        /// <summary>
        /// Tests the table naming convention for inheritance joined sub class mapping when the selected convention is PropertyNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnNamingConventionJoinedSubClassPropertyNameIdPropertyNameMap()
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
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Chief"));
            Assert.IsTrue(tables.Any(t => t.Name == "Employee"));
            Assert.IsTrue(tables.Any(t => t.Name == "Manager"));

            var employeeTable = tables.Single(t => t.Name == "Employee");
            Assert.IsTrue(employeeTable.Columns.Count == 5);
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "ID"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "FirstName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "LastName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "Version"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "DateOfBirth"));

            var chiefTable = tables.Single(t => t.Name == "Chief");
            Assert.IsTrue(chiefTable.Columns.Count == 2);
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "EmployeeID"));
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "Phone"));

            var managerTable = tables.Single(t => t.Name == "Manager");
            Assert.IsTrue(managerTable.Columns.Count == 3);
            Assert.IsTrue(managerTable.Columns.Any(c => c == "EmployeeID"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "Department"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "OfficeNumber"));
        }

        /// <summary>
        /// Tests the table naming convention for inheritance joined sub class mapping when the selected convention is PropertyName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnNamingConventionJoinedSubClassPropertyName_IdPropertyNameMap()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Chief"));
            Assert.IsTrue(tables.Any(t => t.Name == "Employee"));
            Assert.IsTrue(tables.Any(t => t.Name == "Manager"));

            var employeeTable = tables.Single(t => t.Name == "Employee");
            Assert.IsTrue(employeeTable.Columns.Count == 5);
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "firstName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "lastName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "version"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "dateOfBirth"));

            var chiefTable = tables.Single(t => t.Name == "Chief");
            Assert.IsTrue(chiefTable.Columns.Count == 2);
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "employee_id"));
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "phone"));

            var managerTable = tables.Single(t => t.Name == "Manager");
            Assert.IsTrue(managerTable.Columns.Count == 3);
            Assert.IsTrue(managerTable.Columns.Any(c => c == "employee_id"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "department"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "officeNumber"));
        }

        /// <summary>
        /// Tests the table naming convention for inheritance joined sub class mapping when the selected convention is Custom
        /// </summary>
        [TestMethod]
        public void ForeignKeyColumnNamingConventionJoinedSubClassCustomMap()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
                        .UseCustomConventionForForeignKeyColumnNames((p, i) =>
                            {
                                return string.Format("{0}-{1}", p.Name, i.Name);
                            })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);
            Assert.IsTrue(tables.Any(t => t.Name == "Chief"));
            Assert.IsTrue(tables.Any(t => t.Name == "Employee"));
            Assert.IsTrue(tables.Any(t => t.Name == "Manager"));

            var employeeTable = tables.Single(t => t.Name == "Employee");
            Assert.IsTrue(employeeTable.Columns.Count == 5);
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "id"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "firstName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "lastName"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "version"));
            Assert.IsTrue(employeeTable.Columns.Any(c => c == "dateOfBirth"));

            var chiefTable = tables.Single(t => t.Name == "Chief");
            Assert.IsTrue(chiefTable.Columns.Count == 2);
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "Employee-ID"));
            Assert.IsTrue(chiefTable.Columns.Any(c => c == "phone"));

            var managerTable = tables.Single(t => t.Name == "Manager");
            Assert.IsTrue(managerTable.Columns.Count == 3);
            Assert.IsTrue(managerTable.Columns.Any(c => c == "Employee-ID"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "department"));
            Assert.IsTrue(managerTable.Columns.Any(c => c == "officeNumber"));
        }

        #endregion

        #region Primary Key Columns

        /// <summary>
        /// Tests the primary key columns naming convention when the convention is the default one
        /// </summary>
        [TestMethod]
        public void PrimaryKeyColumnsNamingConventionDefaultPascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.Default)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the primary key columns naming convention when the convention is EntityNameIdPropertyName
        /// </summary>
        [TestMethod]
        public void PrimaryKeyColumnsNamingConventionEntityNameIdPropertyNamePascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityNameIdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "CityId"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "StateId"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "ZipCodeId"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the primary key columns naming convention when the convention is EntityName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void PrimaryKeyColumnsNamingConventionEntityName_IdPropertyNamePascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "City_Id"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "State_Id"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "ZipCode_Id"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        /// <summary>
        /// Tests the primary key columns naming convention when the convention is EntityName_IdPropertyName
        /// </summary>
        [TestMethod]
        public void PrimaryKeyColumnsNamingConventionCustomPascalCaseTest()
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
                        .UseConventionForColumnNames(ColumnsNamingConvention.PascalCase)
                        .UseCustomConventionForPrimaryKeyColumnNames((e, i) =>
                            {
                                return string.Format("{0}-{1}", e.Name, i.Name);
                            })
                        .EndConfig()
                    .EndConfiguration()
                 .BuildMappings(NHConfig);

            var tables = GetTables();

            Assert.IsTrue(tables.Count == 3);

            var cityTable = tables.Single(t => t.Name == "City");
            Assert.IsTrue(cityTable.Columns.Count == 5);
            Assert.IsTrue(cityTable.Columns.Any(c => c == "City-ID"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "ZipCode"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "State"));
            Assert.IsTrue(cityTable.Columns.Any(c => c == "Version"));

            var stateTable = tables.Single(t => t.Name == "State");
            Assert.IsTrue(stateTable.Columns.Count == 3);
            Assert.IsTrue(stateTable.Columns.Any(c => c == "State-ID"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Name"));
            Assert.IsTrue(stateTable.Columns.Any(c => c == "Version"));

            var zipCodeTable = tables.Single(t => t.Name == "ZipCode");
            Assert.IsTrue(zipCodeTable.Columns.Count == 4);
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "ZipCode-ID"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "PostalCode"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "City"));
            Assert.IsTrue(zipCodeTable.Columns.Any(c => c == "Version"));
        }

        #endregion
    }
}
