using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// Represents an Address
    /// </summary>
    public class Address
    {
        #region Constructors

        /// <summary>
        /// Intializes a new instance of the <see cref="Address"/> class
        /// </summary>
        public Address()
        { 
        }

        /// <summary>
        /// Intializes a new instance of the <see cref="Address"/> class
        /// </summary>
        /// <param name="street">The street name</param>
        /// <param name="number">The house number</param>
        /// <param name="city">The city</param>
        public Address(string street, string number, City city)
        {
            this.Street = street;
            this.Number = number;
            this.City = city;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the street
        /// </summary>
        public virtual string Street { get; protected set; }

        /// <summary>
        /// Gets or sets the number
        /// </summary>
        public virtual string Number { get; protected set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public virtual City City { get; protected set; }

        #endregion
    }
}
