//Julian Harris
//EmployeeTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void paydayTestOne() 
        {
            //Arrange
            Employee test = new Employee(1, 12345, "Jimmy"); //1st payment level of 200
            double expected = 200;
            double actual;
            //Act
            test.payday(); //should add 200
            actual = test.getBalance();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void paydayTestTwo()
        {
            //Arrange
            Employee test = new Employee(2, 12345, "Jimmy"); //1st payment level of 200
            double expected = 400;
            double actual;
            //Act
            test.payday(); //should add 400
            actual = test.getBalance();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void paydayTestThree()
        {
            //Arrange
            Employee test = new Employee(3, 12345, "Jimmy"); //1st payment level of 200
            double expected = 600;
            double actual;
            //Act
            test.payday(); //should add 600
            actual = test.getBalance();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void setLevelTest()
        {
            //Arrange
            Employee test = new Employee(1, 12345, "Jimmy"); //1st payment level is 200
            double expected = 200;
            double actual;
            //Act
            test.setLevel(); //will set weeklyPay to payLevel of 200.
            test.payday(); //then we add the amount to balance.
            actual = test.getBalance();
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}