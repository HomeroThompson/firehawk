using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhwk.Core.Tests.Common.Utils
{
    /// <summary>
    /// Provides extension methods for the string class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///  Removes all leading and trailing occurrences of the square brackets charactets
        /// </summary>
        /// <param name="str">The input string</param>
        /// <returns>The string with removed brackets</returns>
        public static string RemoveSquareBrackets(this string str)
        {
            return !string.IsNullOrEmpty(str) ? str.Trim('[', ']') : str;
        }
    }
}
