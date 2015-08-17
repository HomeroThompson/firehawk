using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A customer entity
    /// </summary>
    public class Customer : Entity
    {
        #region Members

        private IList<Telephone> telephones;
        private IList<Telephone> mainTelephones;
        private IList<ProductsOrder> orders;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Customer"/> class.
        /// </summary>
        public Customer()
        {
            this.telephones = new List<Telephone>();
            this.mainTelephones = new List<Telephone>();
            this.orders = new List<ProductsOrder>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets the customer code
        /// </summary>
        public virtual string Code { get; protected set; }

        /// <summary>
        /// Gets or sets the customer's date of birth
        /// </summary>
        public virtual DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets the customers age
        /// </summary>
        public virtual int Age
        {
            get
            {
                int age = DateTime.Today.Year - DateOfBirth.Year;
                if (DateOfBirth > DateTime.Today.AddYears(-age)) age--;

                return age;
            }
        }
                
        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public virtual Address Address { get; protected set; }

        /// <summary>
        /// Gets the list of phones
        /// </summary>
        public virtual IReadOnlyCollection<Telephone> Telephones
        {
            get { return new ReadOnlyCollection<Telephone>(this.telephones); }
        }
            
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the customer address
        /// </summary>
        /// <param name="street">The street name</param>
        /// <param name="number">The house number</param>
        /// <param name="city">The city</param>
        public virtual void SetAddress(string street, string number, City city)
        {
            this.Address = new Address(street, number, city);
        }

        /// <summary>
        /// Adds a new phone to the phones list
        /// </summary>
        /// <param name="number">The phone number</param>
        /// <param name="type">The phone type</param>
        public virtual void AddTelephone(string number, TelephoneType type)
        {
            this.telephones.Add(new Telephone(number, type));
        }

        /// <summary>
        /// Removes a telephone from the phones list
        /// </summary>
        /// <param name="telephone">The telephone to remove</param>
        public virtual void RemoveTelephone(Telephone telephone)
        {
            this.telephones.Remove(telephone);
        }

        #endregion

    }
}
