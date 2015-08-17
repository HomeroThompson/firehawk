using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// An order item
    /// </summary>
    public class OrderItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        public OrderItem()
        { 

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        /// <param name="index">The item index</param>
        /// <param name="quantity">The product quantity</param>
        /// <param name="price">The product price</param>
        /// <param name="product">The product being ordered</param>
        public OrderItem(int index, double quantity, decimal price, Product product)
        {
            this.ItemIndex = index;
            this.Quantity = quantity;
            this.Price = price;
            this.Product = product;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the item index
        /// </summary>
        public virtual int ItemIndex { get; protected set; }

        /// <summary>
        /// Gets the product quantity
        /// </summary>
        public virtual double Quantity { get; protected set; }

        /// <summary>
        /// Gets the product price
        /// </summary>
        public virtual decimal Price { get; protected set; }

        /// <summary>
        /// Gets the product being ordered
        /// </summary>
        public virtual Product Product { get; protected set; }


        #endregion
    }
}
