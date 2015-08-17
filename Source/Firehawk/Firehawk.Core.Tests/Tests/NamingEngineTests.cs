using Fhwk.Core.Config;
using Fhwk.Core.Naming;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ElementModel = Fhwk.Core.Tests.Model.Elements;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the NamingEngine class
    /// </summary>
    [TestClass]
    public class NamingEngineTests
    {
        /// <summary>
        /// Tests the ToTableName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToTableNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Table Name => Default
            config.UseConventionForTableNames(TablesNamingConvention.Default);
            string result = namingEngine.ToTableName(typeof(OrderItem));
            Assert.AreEqual("OrderItem", result);

            // Table Name => Camel Case
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            result = namingEngine.ToTableName(typeof(OrderItem));
            Assert.AreEqual("orderItem", result);

            // Table Name => Pascal Case
            config.UseConventionForTableNames(TablesNamingConvention.PascalCase);
            result = namingEngine.ToTableName(typeof(OrderItem));
            Assert.AreEqual("OrderItem", result);

            // Table Name => Lowercase Underscore Separated
            config.UseConventionForTableNames(TablesNamingConvention.Lowercase);
            result = namingEngine.ToTableName(typeof(OrderItem));
            Assert.AreEqual("order_item", result);

            // Table Name => Uppercase Underscore Separated
            config.UseConventionForTableNames(TablesNamingConvention.Uppercase);
            result = namingEngine.ToTableName(typeof(OrderItem));
            Assert.AreEqual("ORDER_ITEM", result);

            // Table Name => Custom
            config.UseCustomConventionForTableNames(t =>
            {
                Assert.AreEqual(typeof(Customer), t);

                return "CustomTableName";
            });
            result = namingEngine.ToTableName(typeof(Customer));
            Assert.AreEqual("CustomTableName", result);

        }

        /// <summary>
        /// Tests the ToTableName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToTableNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Table Name => Custom convention
            config.UseConventionForTableNames(TablesNamingConvention.Custom);
            string result = namingEngine.ToTableName(typeof(Customer));
        }

        /// <summary>
        /// Tests the ToComponentTableName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToComponentTableNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => ComponentName
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.ComponentName);
            string result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("orderItem", result);

            // Component Table Name => EntityName & ComponentName
            config.UseConventionForTableNames(TablesNamingConvention.PascalCase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityName_ComponentName);
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("ProductsOrder_OrderItem", result);

            // Component Table Name => EntityName & Relationship Name
            config.UseConventionForTableNames(TablesNamingConvention.Lowercase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityName_RelationshipName);
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("products_order_items", result);

            // Component Table Name => EntityName & Relationship Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName);
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("productsOrderOrderItem", result);

            // Component Table Name => EntityName & Relationship Name
            config.UseConventionForTableNames(TablesNamingConvention.Uppercase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName);
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("PRODUCTS_ORDER_ITEMS", result);

            // Component Table Name => EntityName & Relationship Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.RelationshipName);
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("items", result);

            // Component Table Name => EntityName & Relationship Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseCustomConventionForComponentTableNames((e, c, p) =>
                {
                    Assert.AreEqual(typeof(ProductsOrder), e);
                    Assert.AreEqual(typeof(OrderItem), c);
                    Assert.AreEqual(typeof(ProductsOrder).GetMember("Items").Single(), p);

                    return "CustomComponentTableName";
                });
            result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("CustomComponentTableName", result);
        }

        /// <summary>
        /// Tests the ToComponentTableName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToComponentTableNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.Custom);
            string result = namingEngine.ToComponentTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
        }

        /// <summary>
        /// Tests the ToElementTableName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToElementTableNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Element Table Name => ElementName
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.ElementTypeName);
            string result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("orderItem", result);

            // Element Table Name => EntityName & ElementName
            config.UseConventionForTableNames(TablesNamingConvention.PascalCase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_ElementName);
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("ProductsOrder_OrderItem", result);

            // Element Table Name => EntityName & Property Name
            config.UseConventionForTableNames(TablesNamingConvention.Lowercase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.EntityName_PropertyName);
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("products_order_items", result);

            // Element Table Name => EntityName & Property Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.EntityNameElementName);
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("productsOrderOrderItem", result);

            // Element Table Name => EntityName & Property Name
            config.UseConventionForTableNames(TablesNamingConvention.Uppercase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.EntityNamePropertyName);
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("PRODUCTS_ORDER_ITEMS", result);

            // Element Table Name => EntityName & Property Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.PropertyName);
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("items", result);

            // Element Table Name => EntityName & Property Name
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseCustomConventionForElementTableNames((e, c, p) =>
            {
                Assert.AreEqual(typeof(ProductsOrder), e);
                Assert.AreEqual(typeof(OrderItem), c);
                Assert.AreEqual(typeof(ProductsOrder).GetMember("Items").Single(), p);

                return "CustomElementTableName";
            });
            result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
            Assert.AreEqual("CustomElementTableName", result);
        }

        /// <summary>
        /// Tests the ToElementTableName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToElementTableNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Element Table Name => Custom convention
            config.UseConventionForElementTableNames(ElementsTableNamingConvention.Custom);
            string result = namingEngine.ToElementTableName(typeof(ProductsOrder), typeof(OrderItem), typeof(ProductsOrder).GetMember("Items").Single());
        }

        /// <summary>
        /// Tests the ToElementTableName method for a dictionary
        /// </summary>
        [TestMethod]
        public void ToElementTableNameDictionaryTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            string result = namingEngine.ToElementTableName(typeof(ElementModel.Element), typeof(Dictionary<long, string>), typeof(ElementModel.Element).GetMember("DictionaryLongString").Single());
        }

        /// <summary>
        /// Tests the ToColumnName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToColumnNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);
            var memberInfo = typeof(Customer).GetMember("FirstName").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            string result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("FirstName", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("firstName", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("FirstName", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("FIRST_NAME", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("first_name", result);

            // Column Name => Custom
            config.UseCustomConventionForColumnNames(m =>
                {
                    Assert.AreEqual(memberInfo, m);

                    return "CustomColumnName";
                });
            result = namingEngine.ToColumnName(memberInfo);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the ToColumnName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToColumnNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);
            var memberInfo = typeof(Customer).GetMember("FirstName").FirstOrDefault();

            // Component Table Name => Custom convention
            config.UseConventionForColumnNames(ColumnsNamingConvention.Custom);
            string result = namingEngine.ToColumnName(memberInfo);
        }

        /// <summary>
        /// Tests the ToPrimaryColumnName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToPrimaryKeyColumnNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);

            // Column Name => Default
            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.Default);
            string result = namingEngine.ToPrimaryKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("id", result);

            // Column Name => EntityName & IdPropertyName
            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName);
            result = namingEngine.ToPrimaryKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("customer_id", result);

            // Column Name => EntityName & IdPropertyName
            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityNameIdPropertyName);
            result = namingEngine.ToPrimaryKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("customerId", result);

            // Column Name => Custom
            config.UseCustomConventionForPrimaryKeyColumnNames((e, i) =>
                {
                    Assert.AreEqual(typeof(Customer), e);
                    Assert.AreEqual(typeof(Customer).GetMember("ID").Single(), i);


                    return "CustomPrimaryKeyColumnName";
                });
            result = namingEngine.ToPrimaryKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CustomPrimaryKeyColumnName", result);
        }


        /// <summary>
        /// Tests the ToPrimaryKeyColumnName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToPrimaryKeyColumnNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.Custom);
            string result = namingEngine.ToPrimaryKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
        }

        /// <summary>
        /// Tests the method ToComponentColumnName when the selected convention is ComponentNamePropertyName
        /// </summary>
        [TestMethod]
        public void ToComponentColumNameComponentNamePropertyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            var parentProperty = typeof(Customer).GetMember("Address").FirstOrDefault();
            var childProperty = typeof(Address).GetMember("Street").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            config.UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.ComponentNamePropertyName);
            string result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("AddressStreet", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("addressStreet", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("AddressStreet", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("ADDRESS_STREET", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Custom
            config.UseCustomConventionForComponentColumnNames((c, p) =>
            {
                Assert.AreEqual(childProperty, p);
                Assert.AreEqual(typeof(Address), c);

                return "CustomColumnName";
            });
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method ToComponentColumnName when the selected convention is ComponentNamePropertyName
        /// </summary>
        [TestMethod]
        public void ToComponentColumNameComponentName_PropertyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            var parentProperty = typeof(Customer).GetMember("Address").FirstOrDefault();
            var childProperty = typeof(Address).GetMember("Street").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            config.UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.ComponentName_PropertyName);
            string result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("Address_Street", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("Address_Street", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("ADDRESS_STREET", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Custom
            config.UseCustomConventionForComponentColumnNames((c, p) =>
            {
                Assert.AreEqual(childProperty, p);
                Assert.AreEqual(typeof(Address), c);

                return "CustomColumnName";
            });
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method ToComponentColumnName when the selected convention is ComponentName_PropertyName
        /// </summary>
        [TestMethod]
        public void ToComponentColumNameCustomTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            var parentProperty = typeof(Customer).GetMember("Address").FirstOrDefault();
            var childProperty = typeof(Address).GetMember("Street").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            config
                .UseCustomConventionForComponentColumnNames((e, m) =>
                {
                    Assert.AreEqual(typeof(Address), e);
                    Assert.AreEqual(childProperty, m);

                    return "CustomColumnName";
                });
            string result = namingEngine.ToComponentColumnName(childProperty, parentProperty);

            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method ToComponentColumnName when the selected convention is ComponentNamePropertyName
        /// </summary>
        [TestMethod]
        public void ToComponentColumNameEntityPropertyNameComponentPropertyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            var parentProperty = typeof(Customer).GetMember("Address").FirstOrDefault();
            var childProperty = typeof(Address).GetMember("Street").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            config.UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName);
            string result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("AddressStreet", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("addressStreet", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("AddressStreet", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("ADDRESS_STREET", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Custom
            config.UseCustomConventionForComponentColumnNames((c, p) =>
            {
                Assert.AreEqual(childProperty, p);
                Assert.AreEqual(typeof(Address), c);

                return "CustomColumnName";
            });
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method ToComponentColumnName when the selected convention is ComponentNamePropertyName
        /// </summary>
        [TestMethod]
        public void ToComponentColumNameEntityPropertyName_ComponentPropertyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            var parentProperty = typeof(Customer).GetMember("Address").FirstOrDefault();
            var childProperty = typeof(Address).GetMember("Street").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            config.UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName);
            string result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("Address_Street", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("Address_Street", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("ADDRESS_STREET", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("address_street", result);

            // Column Name => Custom
            config.UseCustomConventionForComponentColumnNames((c, p) =>
            {
                Assert.AreEqual(childProperty, p);
                Assert.AreEqual(typeof(Address), c);

                return "CustomColumnName";
            });
            result = namingEngine.ToComponentColumnName(childProperty, parentProperty);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method to element key column name
        /// </summary>
        [TestMethod]
        public void ToElementKeyColumNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);
            var memberInfo = typeof(ElementModel.Element).GetMember("DictionaryLongString").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            string result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("IdKey", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("idKey", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("IdKey", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("ID_KEY", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("id_key", result);

            // Column Name => Custom
            config.UseCustomConventionForColumnNames(m =>
            {
                Assert.AreEqual(memberInfo, m);

                return "CustomColumnName";
            });
            result = namingEngine.ToElementKeyColumnName(memberInfo);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the method to element value column name
        /// </summary>
        [TestMethod]
        public void ToElementValueColumNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);
            var memberInfo = typeof(ElementModel.Element).GetMember("DictionaryLongString").FirstOrDefault();

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Default);
            string result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("Value", result);

            // Column Name => Camel Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.CamelCase);
            result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("value", result);

            // Column Name => Pascal Case
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("Value", result);

            // Column Name => Uppercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("VALUE", result);

            // Column Name => Lowercase Underscore Separated
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("value", result);

            // Column Name => Custom
            config.UseCustomConventionForColumnNames(m =>
            {
                Assert.AreEqual(memberInfo, m);

                return "CustomColumnName";
            });
            result = namingEngine.ToElementValueColumnName(memberInfo);
            Assert.AreEqual("CustomColumnName", result);
        }

        /// <summary>
        /// Tests the ToForeignKeyName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToForeignKeyNameEntityNameIdNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName);
            config.UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase);

            // Foreign Key Name => Default
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default);
            string result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.IsTrue(string.IsNullOrEmpty(result));

            // Foreign Key Name => FK_FKTable_PKTable
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable);
            result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.AreEqual("fk__city__state", result);

            // Foreign Key Name => FK_FKTable_PKTable_PKColumn
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn);
            result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.AreEqual("fk__city__state__state_id", result);

            // Foreign Key Name => FKTable_PKTable_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK);
            result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.AreEqual("city__state__fk", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK);
            result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.AreEqual("city__state__state_id__fk", result);

            // Foreign Key Name => Custom
            config.UseCustomConventionForForeignKeyNames((s, t, f, i) =>
            {
                Assert.AreEqual(typeof(City), s);
                Assert.AreEqual(typeof(State), t);
                Assert.AreEqual(typeof(City).GetMember("State").Single(), f);
                Assert.AreEqual(typeof(State).GetMember("ID").Single(), i);

                return "CustomForeignKeyName";
            });
            result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
            Assert.AreEqual("CustomForeignKeyName", result);
        }

        /// <summary>
        /// Tests the ToForeignKeyName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToForeignKeyNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Custom);
            string result = namingEngine.ToForeignKeyName(typeof(City), typeof(State), typeof(City).GetMember("State").Single(), typeof(State).GetMember("ID").Single());
        }

        /// <summary>
        /// Tests the ToComponentForeignKeyName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToComponentForeignKeyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameComponentName);
            config.UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.Default);
            config.UseConventionForConstraintNames(ConstraintNamingConvention.Uppercase);

            // Foreign Key Name => Default
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default);
            string result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.IsTrue(string.IsNullOrEmpty(result));

            // Foreign Key Name => FK_FKTable_PKTable
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("FK__CUSTOMER_TELEPHONE__CUSTOMER", result);

            // Foreign Key Name => FK_FKTable_PKTable_PKColumn
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("FK__CUSTOMER_TELEPHONE__CUSTOMER__ID", result);

            // Foreign Key Name => FKTable_PKTable_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER_TELEPHONE__CUSTOMER__FK", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER_TELEPHONE__CUSTOMER__ID__FK", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseCustomConventionForForeignKeyNames((s, t, f, i) =>
                {
                    Assert.AreEqual(typeof(Customer), s);
                    Assert.AreEqual(typeof(Telephone), t);
                    Assert.AreEqual(typeof(Customer).GetMember("Telephones").Single(), f);
                    Assert.AreEqual(typeof(Customer).GetMember("ID").Single(), i);

                    return "CustomForeignKeyName";
                });
            result = namingEngine.ToComponentForeignKeyName(typeof(Customer), typeof(Telephone), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CustomForeignKeyName", result);
        }

        /// <summary>
        /// Tests the ToForeignKeyName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToComponentForeignKeyNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Custom);
            string result = namingEngine.ToComponentForeignKeyName(typeof(Customer), typeof(Telephone), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
        }

        /// <summary>
        /// Tests the ToComponentForeignKeyName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToComponentForeignKeyNameEntityNameIdNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName);
            config
               .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName)
               .UseConventionForConstraintNames(ConstraintNamingConvention.Uppercase);

            // Foreign Key Name => Default
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default);
            string result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.IsTrue(string.IsNullOrEmpty(result));

            // Foreign Key Name => FK_FKTable_PKTable
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("FK__CUSTOMER_TELEPHONES__CUSTOMER", result);

            // Foreign Key Name => FK_FKTable_PKTable_PKColumn
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("FK__CUSTOMER_TELEPHONES__CUSTOMER__CUSTOMER_ID", result);

            // Foreign Key Name => FKTable_PKTable_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER_TELEPHONES__CUSTOMER__FK", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK);
            result = namingEngine.ToComponentForeignKeyName(typeof(Telephone), typeof(Customer), typeof(Customer).GetMember("Telephones").Single(), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER_TELEPHONES__CUSTOMER__CUSTOMER_ID__FK", result);
        }

        /// <summary>
        /// Tests the ToForeignKeyColumnName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToForeignKeyColumnNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default);
            string result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER", result);

            // Column Name => Target EntityName & IdPropertyName
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName);
            result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("customer_id", result);

            // Column Name => Target EntityName & IdPropertyName
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName);
            result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CustomerId", result);

            // Column Name => Target & IdPropertyName
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName);
            result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(Customer).GetMember("ID").Single());
            Assert.AreEqual("CUSTOMER_ID", result);

            // Column Name => Custom
            config.UseCustomConventionForForeignKeyColumnNames((e, i) =>
            {
                Assert.AreEqual(typeof(Customer), e);
                Assert.AreEqual(typeof(ProductsOrder).GetMember("ID").Single(), i);

                return "CustomForeignKeyColumnName";
            });
            result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("CustomForeignKeyColumnName", result);
        }

        /// <summary>
        /// Tests the ToMnayToManyForeignKeyColumnName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToManyToManyForeignKeyColumnNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Column Name => Default
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default);
            string result = namingEngine.ToManyToManyForeignKeyColumnName(typeof(Product), typeof(Product).GetMember("ID").Single());
            Assert.AreEqual("PRODUCT_KEY", result);

            // Column Name => Target EntityName
            config.UseConventionForColumnNames(ColumnsNamingConvention.PascalCase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Default);
            result = namingEngine.ToManyToManyForeignKeyColumnName(typeof(Product), typeof(Product).GetMember("ID").Single());
            Assert.AreEqual("Product_Key", result);

            // Column Name => Target EntityName & IdPropertyName
            config.UseConventionForColumnNames(ColumnsNamingConvention.Lowercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName);
            result = namingEngine.ToManyToManyForeignKeyColumnName(typeof(Product), typeof(Product).GetMember("ID").Single());
            Assert.AreEqual("product_id", result);

            // Column Name => Target & IdPropertyName
            config.UseConventionForColumnNames(ColumnsNamingConvention.Uppercase);
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName);
            result = namingEngine.ToManyToManyForeignKeyColumnName(typeof(Product), typeof(Product).GetMember("ID").Single());
            Assert.AreEqual("PRODUCT_ID", result);

            // Column Name => Custom
            config.UseCustomConventionForForeignKeyColumnNames((e, i) =>
            {
                Assert.AreEqual(typeof(Product), e);
                Assert.AreEqual(typeof(Product).GetMember("ID").Single(), i);

                return "CustomForeignKeyColumnName";
            });
            result = namingEngine.ToForeignKeyColumnName(typeof(Product), typeof(Product).GetMember("ID").Single());
            Assert.AreEqual("CustomForeignKeyColumnName", result);
        }


        /// <summary>
        /// Tests the ToForeignKeyColumnName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToForeignKeyColumnNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.Custom);
            string result = namingEngine.ToForeignKeyColumnName(typeof(Customer), typeof(ProductsOrder).GetMember("ID").Single());
        }

        /// <summary>
        /// Tests the ToManyToManyForeignKeyName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToManyToManyForeignKeyNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config.UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName);
            config
               .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName)
               .UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase);

            // Foreign Key Name => Default
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default);
            string result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.IsTrue(string.IsNullOrEmpty(result));

            // Foreign Key Name => FK_FKTable_PKTable
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("fk__category_product__category", result);

            // Foreign Key Name => FK_FKTable_PKTable_PKColumn
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("fk__category_product__category__id", result);

            // Foreign Key Name => FKTable_PKTable_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("category_product__category__fk", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("category_product__category__id__fk", result);

            // Foreign Key Name => Custom
            config.UseCustomConventionForForeignKeyNames((s, t, f, i) =>
            {
                Assert.AreEqual(typeof(Product), s);
                Assert.AreEqual(typeof(Category), t);
                Assert.AreEqual(typeof(Category), f);
                Assert.AreEqual(typeof(ProductsOrder).GetMember("ID").Single(), i);

                return "CustomForeignKeyName";
            });
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("CustomForeignKeyName", result);
        }

        /// <summary>
        /// Tests the ToManyToManyForeignKeyName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToManyToManyForeignKeyNameTableNameTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            config
                .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameToSecondTableName);
            config
               .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName)
               .UseConventionForConstraintNames(ConstraintNamingConvention.Lowercase);

            // Foreign Key Name => Default
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Default);
            string result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.IsTrue(string.IsNullOrEmpty(result));

            // Foreign Key Name => FK_FKTable_PKTable
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("fk__category_to_product__category", result);

            // Foreign Key Name => FK_FKTable_PKTable_PKColumn
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("fk__category_to_product__category__id", result);

            // Foreign Key Name => FKTable_PKTable_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FK);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("category_to_product__category__fk", result);

            // Foreign Key Name => FKTable_PKTable_PKColumn_FK
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK);
            result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(ProductsOrder).GetMember("ID").Single());
            Assert.AreEqual("category_to_product__category__id__fk", result);
        }

        /// <summary>
        /// Tests the ToManyToManyForeignKeyName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToManyToManyForeignKeyNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Component Table Name => Custom convention
            config.UseConventionForForeignKeyNames(ForeignKeyNamingConvention.Custom);
            string result = namingEngine.ToManyToManyForeignKeyName(typeof(Product), typeof(Category), typeof(Category), typeof(Product).GetMember("ID").Single());
        }

        /// <summary>
        /// Tests the ToManyToManyTableName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToManyToManyTableNameCamelCaseTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Table Name => Default
            config.UseConventionForTableNames(TablesNamingConvention.CamelCase);
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName);
            string result = namingEngine.ToManyToManyTableName(typeof(Product), typeof(Category));
            Assert.AreEqual("category_product", result);

            // Table Name => First Table Name & Second Table Name
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName);
            result = namingEngine.ToManyToManyTableName(typeof(Category), typeof(Product));
            Assert.AreEqual("category_product", result);

            // Table Name => First Table Name to Second Table Name
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameToSecondTableName);
            result = namingEngine.ToManyToManyTableName(typeof(Category), typeof(Product));
            Assert.AreEqual("categoryToProduct", result);

            // Table Name => Custom
            config.UseCustomConventionForManyToManyTableNames((f, s) =>
            {
                Assert.AreEqual(typeof(Category), f);
                Assert.AreEqual(typeof(Product), s);

                return "CustomTableName";
            });
            result = namingEngine.ToManyToManyTableName(typeof(Category), typeof(Product));
            Assert.AreEqual("CustomTableName", result);

        }

        /// <summary>
        /// Tests the ToManyToManyTableName method according the selected configuration
        /// </summary>
        [TestMethod]
        public void ToManyToManyTableNamePascalCaseTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Table Name => Default
            config.UseConventionForTableNames(TablesNamingConvention.PascalCase);
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableName_SecondTableName);
            string result = namingEngine.ToManyToManyTableName(typeof(Product), typeof(Category));
            Assert.AreEqual("Category_Product", result);

            // Table Name => First Table Name to Second Table Name
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameToSecondTableName);
            result = namingEngine.ToManyToManyTableName(typeof(Category), typeof(Product));
            Assert.AreEqual("CategoryToProduct", result);

            // Table Name => Custom
            config.UseCustomConventionForManyToManyTableNames((f, s) =>
            {
                Assert.AreEqual(typeof(Category), f);
                Assert.AreEqual(typeof(Product), s);

                return "CustomTableName";
            });
            result = namingEngine.ToManyToManyTableName(typeof(Category), typeof(Product));
            Assert.AreEqual("CustomTableName", result);

        }

        /// <summary>
        /// Tests the ToManyToManyTableName method when custom naming convention was selected but no custom function has been provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ToManyToManyTableNameNoCustomMethodTest()
        {
            NamingConventionsConfig config = new NamingConventionsConfig(null);
            NamingEngine namingEngine = new NamingEngine(config);

            // Table Name => Custom convention
            config.UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.Custom);
            string result = namingEngine.ToManyToManyTableName(typeof(Product), typeof(Category));
        }

    }
}
