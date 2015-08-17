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
    public class MySQLExampleTest : BaseMySqlTest
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
                        .UseConventionForTableNames(TablesNamingConvention.Lowercase)
                        .UseConventionForComponentTableNames(ComponentsTableNamingConvention.EntityNameRelationshipName)
                        .UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention.FirstTableNameSecondTableName)
                        .UseConventionForColumnNames(ColumnsNamingConvention.CamelCase)
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
                Name = "category",
                Columns = new string[] { "categoryId", "name", "version" },
            };
            ValidateTable(expectedCategoryTable, tables);

            // Category Product Table
            var expectedCategoryProductTable = new Table()
            {
                Name = "category_product",
                Columns = new string[] { "categoryId", "productId" },
            };
            ValidateTable(expectedCategoryProductTable, tables);

            // Customer Table
            var expectedCustomerTable = new Table()
            {
                Name = "customer",
                Columns = new string[] { "customerId", "firstName", "lastName", "code", "dateOfBirth", "address_street", "address_number", "address_cityId", "version" },
            };
            ValidateTable(expectedCustomerTable, tables);

            // Product Table
            var expectedProductTable = new Table()
            {
                Name = "product",
                Columns = new string[] { "productId", "name", "description", "version" },
            };
            ValidateTable(expectedProductTable, tables);

            // Products Order Table
            var expectedProductsOrderTable = new Table()
            {
                Name = "products_order",
                Columns = new string[] { "productsOrderId", "date", "shippingAddress_street", "shippingAddress_number", "shippingAddress_cityId", "customerId", "version" },
            };
            ValidateTable(expectedProductsOrderTable, tables);

            // Products Order Items Table
            var expectedProductsOrderItemsTable = new Table()
            {
                Name = "products_order_items",
                Columns = new string[] { "productsOrderId", "itemIndex", "quantity", "price", "productId" },
            };
            ValidateTable(expectedProductsOrderItemsTable, tables);

            // City Table
            var expectedCityTable = new Table()
            {
                Name = "city",
                Columns = new string[] { "cityId", "name", "zipCodeId", "stateId", "version" },
            };
            ValidateTable(expectedCityTable, tables);

            // City Table
            var expectedStateTable = new Table()
            {
                Name = "state",
                Columns = new string[] { "stateId", "name", "version" },
            };
            ValidateTable(expectedStateTable, tables);

            // Zip Code Table
            var expectedZipCodeTable = new Table()
            {
                Name = "zip_code",
                Columns = new string[] { "zipCodeId", "postalCode", "cityId", "version" },
            };
            ValidateTable(expectedZipCodeTable, tables);
        }

        #endregion
    }
}
