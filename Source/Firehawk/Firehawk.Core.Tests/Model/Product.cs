using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A product
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class
        /// </summary>
        public Product()
        {
            this.Categories = new List<Category>();
        }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets a product description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of cateogries this product belongs
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; }
    }
}
