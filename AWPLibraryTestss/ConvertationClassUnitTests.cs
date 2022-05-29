using AWPLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AWPLibraryUnitTests
{
    [TestClass]
    public class ConvertationClassUnitTests
    {

        /*
        [TestMethod]
        public void Name_TestMethod_Returned()
        {
            // Arrange


            // Act


            // Assert


        }
        */

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения
        /// </summary>
        [TestMethod]
        public void GetFormatedDate_TestMethod1_TrueReturned()
        {
            // Arrange
            DateTime dateTime = new DateTime(1997, 01, 07);

            string expected = "1997-01-07";

            // Act
            string result = ConvertationClass.GetFormatedDate(dateTime);

            // Assert

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения
        /// </summary>
        [TestMethod]
        public void GetFormatedDate_TestMethod2_TrueReturned()
        {
            // Arrange
            DateTime dateTime = new DateTime(1568, 05, 02);

            string expected = "1568-05-02";

            // Act
            string result = ConvertationClass.GetFormatedDate(dateTime);

            // Assert

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения
        /// </summary>
        [TestMethod]
        public void GetFormatedDate_TestMethod3_TrueReturned()
        {
            // Arrange
            DateTime dateTime = new DateTime(1568, 13, 02);

            // Act
            string result = ConvertationClass.GetFormatedDate(dateTime);

            // Assert

            Assert.Throws(() => result);
        }

    }
}
