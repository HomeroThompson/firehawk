using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Common.Data
{
    /// <summary>
    /// Represents a database table
    /// </summary>
    public class Table
    {
        /// <summary>
        /// Gets or sets the table schema
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the table name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of table columns
        /// </summary>
        public IList<string> Columns { get; set; }

        /// <summary>
        /// Gets or sets the primary key
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the list of foreign keys
        /// </summary>
        public IList<string> ForeignKeys { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
