using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A products order
    /// </summary>
    public class ProductsOrder : Entity
    {
        #region Members

        private IList<OrderItem> items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsOrder"/> class.
        /// </summary>
        public ProductsOrder()
        {
            this.items = new List<OrderItem>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the order date
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets the shipping address
        /// </summary>
        public virtual Address ShippingAddress { get; protected set; }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual Customer Customer { get; protected set; }

        /// <summary>
        /// Gets the list of items
        /// </summary>
        public virtual IReadOnlyCollection<OrderItem> Items 
        {
            get { return new ReadOnlyCollection<OrderItem>(this.items); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the order
        /// </summary>
        /// <param name="quantity">The product quantity</param>
        /// <param name="price">The product price</param>
        /// <param name="product">The product being ordered</param>
        public virtual void AddItem(double quantity, decimal price, Product product)
        {
            var item = new OrderItem(this.items.Count + 1, quantity, price, product);
            this.items.Add(item);
        }

        /// <summary>
        /// Adds an item to the order
        /// </summary>
        /// <param name="item">The item to remove</param>
        public virtual void RemoveItem(OrderItem item)
        {
            this.items.Remove(item);
        }

        /// <summary>
        /// Sets the order shipping address
        /// </summary>
        /// <param name="street">The street name</param>
        /// <param name="number">The house number</param>
        /// <param name="city">The city</param>
        public virtual void SetShippingAddress(string street, string number, City city)
        {
            this.ShippingAddress = new Address(street, number, city);
        }

        /// <summary>
        /// Sets the order customer
        /// </summary>
        public virtual void SetCustomer(Customer customer)
        {
            this.Customer = customer;
        }

        #endregion
    }
}
