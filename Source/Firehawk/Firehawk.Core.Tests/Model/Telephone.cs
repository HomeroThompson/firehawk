using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Model
{
    /// <summary>
    /// A telephone contact
    /// </summary>
    public class Telephone
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Telephone"/> class
        /// </summary>
        public Telephone()
        { 
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Telephone"/> class
        /// </summary>
        /// <param name="number">The phone number</param>
        /// <param name="type">The phone type</param>
        public Telephone(string number, TelephoneType type)
        {
            this.Number = number;
            this.Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the phone number
        /// </summary>
        public virtual string Number { get; protected set; }

        /// <summary>
        /// Gets the phone type
        /// </summary>
        public virtual TelephoneType Type { get; protected set; }

        #endregion
    }
}
