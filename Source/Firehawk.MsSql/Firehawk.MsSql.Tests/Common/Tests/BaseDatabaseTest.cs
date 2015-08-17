using Fhwk.MsSql.Tests.Common.Data;
using Fhwk.MsSql.Tests.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fhwk.MsSql.Tests.Common.Tests
{
    /// <summary>
    /// A base class for tests that access the database
    /// </summary>
    public class BaseDatabaseTest
    {
        #region Constants

        private const string ConnectionStringName = "FirehawkSQLDB";
        private const string AdminConnectionStringName = "FirehawkSQLDBAdmin";
        private const string DatabaseName = "FirehawkTests";
        private const string LoginName = "FirehawkAdmin";

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the current NHibernate configuration
        /// </summary>
        protected Configuration NHConfig { get; private set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the list of database tables
        /// </summary>
        /// <returns>The list of table descriptions</returns>
        protected IList<Table> GetTables()
        {
            IList<Table> result = new List<Table>();
            result = this.NHConfig.ClassMappings.Select(c => c.Table)
                .Concat(this.NHConfig.CollectionMappings.Select(t => t.CollectionTable))
                .Distinct()
                .Select(t => new Table()
                {
                    Schema = t.Schema.RemoveSquareBrackets(),
                    Name = t.Name.RemoveSquareBrackets(),
                    Columns = t.ColumnIterator.Select(c => c.Name.RemoveSquareBrackets()).ToList(),
                    PrimaryKey = t.HasPrimaryKey ? t.PrimaryKey.Name.RemoveSquareBrackets() : null,
                    ForeignKeys = t.ForeignKeyIterator.Select(f => f.Name.RemoveSquareBrackets()).ToList(),
                })
                .ToList();

            return result;
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
                    db.ConnectionStringName = ConnectionStringName;
                });
        }

        #endregion
             
    }
}
