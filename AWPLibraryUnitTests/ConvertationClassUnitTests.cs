using AWPLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

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

        #region GetFormatedDate

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения даты
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

        #endregion

        #region GetFormatedTime

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения времени
        /// </summary>
        [TestMethod]
        public void GetFormatedTime_TestMethod1_TrueReturned()
        {
            // Arrange
            DateTime timeSpan = new DateTime(1, 1, 1, 14, 0, 0);

            string expected = "14:00:00";

            // Act
            string result = ConvertationClass.GetFormatedTime(timeSpan);

            // Assert

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Метод переводящий DateTime в String для более приятного визуального отображения времени
        /// Введено 25 часов - вывод 1 час
        /// </summary>
        [TestMethod]
        public void GetFormatedTime_TestMethod2_TrueReturned()
        {
            // Arrange
            TimeSpan timeSpan = new TimeSpan(25, 0, 0);

            string expected = "01:00:00";

            // Act
            string result = ConvertationClass.GetFormatedTime(timeSpan);

            // Assert

            Assert.AreEqual(expected, result);
        }

        #endregion


    }
}
