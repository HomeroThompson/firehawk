using Fhwk.Core.Tests.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
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
    /// A base class for tests that run against a SQL Server database
    /// </summary>
    public class BaseSqlServerTest
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
                    db.Dialect<MsSql2012Dialect>();
                    db.Driver<SqlClientDriver>();
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.IsolationLevel = IsolationLevel.ReadCommitted;
                    db.SchemaAction = SchemaAutoAction.Create;
                    db.ConnectionStringName = ConnectionStringNames.SQLServerDB;
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
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringNames.SQLServerDB].ConnectionString;
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT t.Table_Schema, t.Table_Name,	(SELECT STUFF((SELECT ',' + COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = t.TABLE_NAME FOR XML PATH('')) ,1,1,'')) as Column_Names, (SELECT STUFF((SELECT ',' + [name] FROM sysobjects WHERE [xtype] = 'PK'AND [parent_obj] = OBJECT_ID(t.Table_Schema + '.' + t.Table_Name) FOR XML PATH('')) ,1,1,'')) as Primary_Keys, (SELECT STUFF((SELECT ',' + [name] FROM sysobjects WHERE [xtype] = 'F'AND [parent_obj] = OBJECT_ID(t.Table_Schema + '.' + t.Table_Name) FOR XML PATH('')) ,1,1,'')) as Foreign_Keys FROM information_schema.tables t WHERE table_type = 'BASE TABLE' ORDER BY t.Table_Name";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Table table = new Table()
                    {
                        Schema = (string)reader["Table_Schema"],
                        Name = (string)reader["Table_Name"],
                        Columns = (reader["Column_Names"] != System.DBNull.Value) ? ((string)reader["Column_Names"]).Split(',').ToList() : new List<string>(),
                        PrimaryKey = (reader["Primary_Keys"] != System.DBNull.Value) ? ((string)reader["Primary_Keys"]).Split(',').FirstOrDefault() : default(string),
                        ForeignKeys = (reader["Foreign_Keys"] != System.DBNull.Value) ? ((string)reader["Foreign_Keys"]).Split(',').ToList() : new List<string>(),
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
