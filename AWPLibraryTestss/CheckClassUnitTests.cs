using AWPLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AWPLibraryUnitTests
{
    [TestClass]
    public class CheckClassUnitTests
    {
        CheckClass cc = new CheckClass();

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
        /// Метод проверки на то, что число current находится в диапазоне чисел min и max
        /// Число 3 находится в диапазоне от 1 до 5 
        /// </summary>
        [TestMethod]
        public void InRange_TestMethod1_TrueReturned()
        {
            // Arrange
            int min = 1;
            int max = 5;
            int current = 3;

            // Act
            bool result = cc.InRange(current, min, max);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Метод проверки на то, что число current находится в диапазоне чисел min и max
        /// Число 5 находится в диапазоне от 1 до 5
        /// </summary>
        [TestMethod]
        public void InRange_TestMethod2_TrueReturned()
        {
            // Arrange
            int min = 1;
            int max = 5;
            int current = 5;

            // Act
            bool result = cc.InRange(current, min, max);

            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Метод проверки на то, что число current находится в диапазоне чисел min и max
        /// Число 7 не находится в диапазоне от 1 до 5
        /// </summary>
        [TestMethod]
        public void InRange_TestMethod3_FalseReturned()
        {
            // Arrange
            int min = 1;
            int max = 5;
            int current = 7;

            // Act
            bool result = cc.InRange(current, min, max);

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Метод проверки на то, что число current находится в диапазоне чисел min и max
        /// Число -24 не находится в диапазоне от 1 до 5
        /// </summary>
        [TestMethod]
        public void InRange_TestMethod4_FalseReturned()
        {
            // Arrange
            int min = 1;
            int max = 5;
            int current = -24;

            // Act
            bool result = cc.InRange(current, min, max);

            // Assert
            Assert.IsFalse(result);
        }

    }
}
