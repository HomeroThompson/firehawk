using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model.ManyToMany
{
    /// <summary>
    /// A product category
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class
        /// </summary>
        public Category()
        {
            this.Products = new List<Product>();
        }

        /// <summary>
        /// Gets or sets the category name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of products that belong to this category
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Adds a product to this category
        /// </summary>
        /// <param name="product">The product to add</param>
        public virtual void AddProduct(Product product)
        {
            if (!Products.Contains(product))
            {
                Products.Add(product);
                product.Categories.Add(this);
            }
        }

        /// <summary>
        /// Removes a product from this category
        /// </summary>
        /// <param name="product">The product to remove</param>
        public virtual void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
