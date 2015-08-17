using Fhwk.Core.Tests.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Common.Tests
{
    /// <summary>
    /// A base class for tests that run against a My SQL database
    /// </summary>
    public class BaseMySqlTest
    {
        #region Members

        private ISessionFactory sessionFactory;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the current NHibernate configuration
        /// </summary>
        protected Configuration NHConfig { get; private set; }

        /// <summary>
        /// Gets the current session factory
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get { return sessionFactory ?? (sessionFactory = NHConfig.BuildSessionFactory()); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the tests context
        /// </summary>
        [TestInitialize]
        public void InitializeTestContext()
        {
            NHConfig = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<MySQL5Dialect>();
                    db.ConnectionProvider<DriverConnectionProvider>();
                    db.Driver<MySqlDataDriver>();
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.IsolationLevel = IsolationLevel.ReadCommitted;
                    db.SchemaAction = SchemaAutoAction.Create;
                    db.ConnectionStringName = ConnectionStringNames.MySQLDB;
                });
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the list of database tables
        /// </summary>
        /// <returns>The list of table descriptions</returns>
        protected IList<Table> GetTables()
        {
            IList<Table> result = new List<Table>();
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringNames.MySQLDB].ConnectionString;
            var databaseName = new MySqlConnectionStringBuilder(connString).Database;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = " SELECT t.TABLE_NAME AS 'Table Name', (SELECT GROUP_CONCAT(COLUMN_NAME SEPARATOR ',') FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = t.TABLE_SCHEMA AND TABLE_NAME = t.TABLE_NAME) AS 'Columns', (SELECT GROUP_CONCAT(CONSTRAINT_NAME SEPARATOR ', ') FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA = t.TABLE_SCHEMA AND TABLE_NAME = t.TABLE_NAME AND REFERENCED_TABLE_NAME IS NOT NULL) AS 'Foreign Keys' FROM information_schema.TABLES t WHERE (t.TABLE_TYPE = 'BASE TABLE') AND t.TABLE_SCHEMA = @SchemaName";
                command.Parameters.Add(new MySqlParameter("@SchemaName", MySqlDbType.Text) { Value = databaseName });

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Table table = new Table()
                    {
                        Name = (string)reader["Table Name"],
                        Columns = (reader["Columns"] != System.DBNull.Value) ? ((string)reader["Columns"]).Split(',').ToList() : new List<string>(),
                        ForeignKeys = (reader["Foreign Keys"] != System.DBNull.Value) ? ((string)reader["Foreign Keys"]).Split(',').ToList() : new List<string>(),
                    };

                    result.Add(table);
                }
            }

            return result;
        }


        /// <summary>
        /// Exports the schema and returns the SchemaExport instance
        /// </summary>
        /// <param name="config">The current nh config</param>
        /// <returns>The SchemaExport instance</returns>
        protected SchemaExport ExportSchema(Configuration config)
        {
            var schemaExport = new SchemaExport(config);

            // Create
            schemaExport.Execute(false, true, false);

            return schemaExport;
        }

        /// <summary>
        /// Drops the given schema 
        /// </summary>
        /// <param name="schemaExport">The schema to drop</param>
        protected void DropSchema(SchemaExport schemaExport)
        {
            // Drop
            schemaExport.Execute(false, true, true);
        }

        /// <summary>
        /// Exports the schema, gets the list of exported tables and drops the generated schema
        /// </summary>
        /// <param name="config">The current nh config</param>
        /// <returns>The list of generated tables</returns>
        protected IList<Table> ExportSchemaAndDrop(Configuration config)
        {
            IList<Table> result = null;
            var schemaExport = new SchemaExport(config);

            // Create
            schemaExport.Execute(false, true, false);

            // Get schema
            result = GetTables();

            // Drop
            schemaExport.Execute(false, true, true);

            return result;
        }



        #endregion
    }
}
