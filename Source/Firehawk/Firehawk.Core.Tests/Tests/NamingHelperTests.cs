using Fhwk.Core.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fhwk.Core.Tests.Tests
{
    /// <summary>
    /// Tests the NamingHelper class
    /// </summary>
    [TestClass]
    public class NamingHelperTests
    {
        #region ToPascalCase

        /// <summary>
        /// Tests the method ToPascalCase when the input string is already in pascal case
        /// </summary>
        [TestMethod]
        public void ToPascalCaseAlreadyPascalCase()
        {
            string input = "TestString";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("TestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string is in camel case
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromCamelCase()
        {
            string input = "testString";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("TestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWords()
        {
            string input = "Another test String";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("AnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWordsUppercase()
        {
            string input = "ANOTHER TEST STRING";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("AnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWordsLowercase()
        {
            string input = "another test string";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("AnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWordsLowercaseUnderscoreSeparated()
        {
            string input = "another_test_string";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("AnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWordsUppercaseUnderscoreSeparated()
        {
            string input = "ANOTHER_TEST_STRING";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("AnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string consists on words separated by underscores and spaces and any casing
        /// </summary>
        [TestMethod]
        public void ToPascalCaseFromWordsMixed()
        {
            string input = "just ANOTHER_Test string";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("JustAnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string is null
        /// </summary>
        [TestMethod]
        public void ToPascalCaseNullValue()
        {
            string input = null;
            string result = NamingHelper.ToPascalCase(input);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string is emtpy
        /// </summary>
        [TestMethod]
        public void ToPascalCaseEmptyValue()
        {
            string input = "";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Tests the method ToPascalCase when the input string is a single char
        /// </summary>
        [TestMethod]
        public void ToPascalCaseSingleCharString()
        {
            string input = "c";
            string result = NamingHelper.ToPascalCase(input);

            Assert.AreEqual("C", result);
        }

        #endregion

        #region ToCamelCase

        /// <summary>
        /// Tests the method ToCamelCase when the input string is already in camel case
        /// </summary>
        [TestMethod]
        public void ToCamelCaseAlreadyCamelCase()
        {
            string input = "testString";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("testString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string is in pascal case
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromPascalCase()
        {
            string input = "TestString";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("testString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWords()
        {
            string input = "Another test String";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("anotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWordsUppercase()
        {
            string input = "ANOTHER TEST STRING";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("anotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWordsLowercase()
        {
            string input = "another test string";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("anotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWordsLowercaseUnderscoreSeparated()
        {
            string input = "another_test_string";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("anotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWordsUppercaseUnderscoreSeparated()
        {
            string input = "ANOTHER_TEST_STRING";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("anotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string consists on words separated by underscores and spaces and any casing
        /// </summary>
        [TestMethod]
        public void ToCamelCaseFromWordsMixed()
        {
            string input = "just ANOTHER_Test string";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("justAnotherTestString", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string is null
        /// </summary>
        [TestMethod]
        public void ToCamelCaseNullValue()
        {
            string input = null;
            string result = NamingHelper.ToCamelCase(input);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string is emtpy
        /// </summary>
        [TestMethod]
        public void ToCamelCaseEmptyValue()
        {
            string input = "";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Tests the method ToCamelCase when the input string is a single char
        /// </summary>
        [TestMethod]
        public void ToCamelCaseSingleCharString()
        {
            string input = "C";
            string result = NamingHelper.ToCamelCase(input);

            Assert.AreEqual("c", result);
        }
        
        #endregion

        #region ToUppercase

        /// <summary>
        /// Tests the method ToUppercase when the input string is already in uppercase case separated by underscores
        /// </summary>
        [TestMethod]
        public void ToUppercaseAlreadyUppercaseWithUnderscores()
        {
            string input = "TEST_STRING";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string is in camel case
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromCamelCase()
        {
            string input = "testString";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWords()
        {
            string input = "Another test String";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWordsUppercase()
        {
            string input = "ANOTHER TEST STRING";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWordsLowercase()
        {
            string input = "another test string";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWordsLowercaseUnderscoreSeparated()
        {
            string input = "another_test_string";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWordsPascalCase()
        {
            string input = "AnotherTestString";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string consists on words separated by underscores and spaces and any casing
        /// </summary>
        [TestMethod]
        public void ToUppercaseFromWordsMixed()
        {
            string input = "just ANOTHER_Test string";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("JUST_ANOTHER_TEST_STRING", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string is null
        /// </summary>
        [TestMethod]
        public void ToUppercaseNullValue()
        {
            string input = null;
            string result = NamingHelper.ToUppercase(input);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string is emtpy
        /// </summary>
        [TestMethod]
        public void ToUppercaseEmptyValue()
        {
            string input = "";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Tests the method ToUppercase when the input string is a single char
        /// </summary>
        [TestMethod]
        public void ToUppercaseSingleCharString()
        {
            string input = "c";
            string result = NamingHelper.ToUppercase(input);

            Assert.AreEqual("C", result);
        }

        #endregion

        #region ToLowercase

        /// <summary>
        /// Tests the method ToLowercase when the input string is already in lower case separated by underscores
        /// </summary>
        [TestMethod]
        public void ToLowercaseAlreadyLowercaseWithUnderscores()
        {
            string input = "test_string";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string is in camel case
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromCamelCase()
        {
            string input = "testString";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWords()
        {
            string input = "Another test String";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWordsUppercase()
        {
            string input = "ANOTHER TEST STRING";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by blank spaces
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWordsLowercase()
        {
            string input = "another test string";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWordsUppercaseUnderscoreSeparated()
        {
            string input = "ANOTHER_TEST_STRING";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by underscores
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWordsPascalCase()
        {
            string input = "AnotherTestString";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string consists on words separated by underscores and spaces and any casing
        /// </summary>
        [TestMethod]
        public void ToLowercaseFromWordsMixed()
        {
            string input = "just ANOTHER_Test string";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("just_another_test_string", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string is null
        /// </summary>
        [TestMethod]
        public void ToLowercaseNullValue()
        {
            string input = null;
            string result = NamingHelper.ToLowercase(input);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string is emtpy
        /// </summary>
        [TestMethod]
        public void ToLowercaseEmptyValue()
        {
            string input = "";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Tests the method ToLowercase when the input string is a single char
        /// </summary>
        [TestMethod]
        public void ToLowercaseSingleCharString()
        {
            string input = "c";
            string result = NamingHelper.ToLowercase(input);

            Assert.AreEqual("c", result);
        }

        #endregion
    }
}
