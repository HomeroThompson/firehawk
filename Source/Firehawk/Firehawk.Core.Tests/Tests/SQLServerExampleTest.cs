using Fhwk.Core.Tests.Common.Data;
using Fhwk.Core.Tests.Common.Tests;
using Fhwk.Core.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// An example test
    /// </summary>
    [TestClass, Ignore]
    public class SQLServerExampleTest : BaseSqlServerTest
    {
        #region Tests

        /// <summary>
        /// Tests the config against a SQL Server database
        /// </summary>
        [TestMethod]
        public void CreateModelTest()
        {
            Firehawk.Init()
                .Configure()
                    .ConfigureEntities()
                        .AddBaseEntity<Entity>()
                        .SearchForEntitiesOnThisAssembly(Assembly.GetExecutingAssembly())
                        .EndConfig()
                    .ConfigureMappings()
                        .SearchForMappingsOnThisAssembly(Assembly.GetExecutingAssembly())
                        .EndConfig()
                    .ConfigureNamingConventions()
                        .UseConventionForTableNames(TablesNamingConvention.Uppercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForColumnNames(ColumnsNamingConvention.Lowercase)
                        .UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention.EntityNameIdPropertyName)
                        .UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName)
                        .UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName)
                        .EndConfig()
                    .EndConfiguration()
                    .AddCustomMapping<Product>(map => map.Bag(x => x.Categories, m => m.Cascade(Cascade.None), m => m.ManyToMany()))
                    .BuildMappings(NHConfig);

            var tables = ExportSchemaAndDrop(NHConfig);

            ValidateResult(tables);
        }

        #endregion

        #region Private Methods

        private void ValidateTable(Table expectedTable, IList<Table> result)
        {
            // Table & Name
            var actualTable = result.SingleOrDefault(t => t.Name.Equals(expectedTable.Name));
            Assert.IsNotNull(actualTable);

            // Columns
            Assert.AreEqual(expectedTable.Columns.Count, actualTable.Columns.Count);
            Assert.IsTrue(expectedTable.Columns.All(c => actualTable.Columns.Contains(c)));
        }

        private void ValidateResult(IList<Table> tables)
        {
            // Category Table
            var expectedCategoryTable = new Table()
            {
                Name = "CATEGORY",
                Columns = new string[] { "category_id", "name", "version" },
            };
            ValidateTable(expectedCategoryTable, tables);

            // Category Product Table
            var expectedCategoryProductTable = new Table()
            {
                Name = "CATEGORY_PRODUCT",
                Columns = new string[] { "category_id", "product_id" },
            };
            ValidateTable(expectedCategoryProductTable, tables);

            // Customer Table
            var expectedCustomerTable = new Table()
            {
                Name = "CUSTOMER",
                Columns = new string[] { "customer_id", "first_name", "last_name", "code", "date_of_birth", "address_street", "address_number", "address_city_id", "version" },
            };
            ValidateTable(expectedCustomerTable, tables);

            // Product Table
            var expectedProductTable = new Table()
            {
                Name = "PRODUCT",
                Columns = new string[] { "product_id", "name", "description", "version" },
            };
            ValidateTable(expectedProductTable, tables);

            // Products Order Table
            var expectedProductsOrderTable = new Table()
            {
                Name = "PRODUCTS_ORDER",
                Columns = new string[] { "products_order_id", "date", "shipping_address_street", "shipping_address_number", "shipping_address_city_id", "customer_id", "version" },
            };
            ValidateTable(expectedProductsOrderTable, tables);

            // Products Order Items Table
            var expectedProductsOrderItemsTable = new Table()
            {
                Name = "PRODUCTS_ORDER_ITEMS",
                Columns = new string[] { "products_order_id", "item_index", "quantity", "price", "product_id" },
            };
            ValidateTable(expectedProductsOrderItemsTable, tables);

            // City Table
            var expectedCityTable = new Table()
            {
                Name = "CITY",
                Columns = new string[] { "city_id", "name", "zip_code_id", "state_id", "version" },
            };
            ValidateTable(expectedCityTable, tables);

            // City Table
            var expectedStateTable = new Table()
            {
                Name = "STATE",
                Columns = new string[] { "state_id", "name", "version" },
            };
            ValidateTable(expectedStateTable, tables);

            // Zip Code Table
            var expectedZipCodeTable = new Table()
            {
                Name = "ZIP_CODE",
                Columns = new string[] { "zip_code_id", "postal_code", "city_id", "version" },
            };
            ValidateTable(expectedZipCodeTable, tables);
        }

        #endregion
    }
}
