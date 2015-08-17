using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A product category
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Gets or sets the category name
        /// </summary>
        public virtual string Name { get; set; }
    }
}
