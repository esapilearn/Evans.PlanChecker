using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESAPX_StarterUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Constraints;

namespace ESAPX_StarterUI.ViewModels.Tests
{
    [TestClass()]
    public class CTDateConstraintTests
    {
        [TestMethod()]
        public void ConstraintCTDateFAILSTest()
        {
            //Arrange
            var oldDate = DateTime.Now.AddDays(-61); //test an old CT

            //Act
            var actual = new CTDateConstraint().ConstraintCTDate(oldDate).ResultType;

            //Assert
            var expected = ResultType.ACTION_LEVEL_3;
            Assert.AreEqual(expected, actual); //Special Assert class just for this purpose
        }

        [TestMethod()]
        public void ConstraintCTDatePASSEDTest()
        {
            //Arrange
            var newDate = DateTime.Now.AddDays(-59); //test an old CT

            //Act
            var actual = new CTDateConstraint().ConstraintCTDate(newDate).ResultType;

            //Assert
            var expected = ResultType.PASSED;
            Assert.AreEqual(expected, actual); //Special Assert class just for this purpose
        }
    }
}