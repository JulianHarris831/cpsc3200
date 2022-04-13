//Julian Harris
//AllergyCustomerTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class AllergyCustomerTest
    {
        [TestMethod]
        public void AllergyCustomerBuyOneTest()
        {
            //Arrange
            string[] newAllergies = new string[3]; //alergic to three things
            newAllergies[0] = "peanuts";
            newAllergies[1] = "fish";
            newAllergies[2] = "dairy";
            AllergyCustomer tester = new AllergyCustomer(12345, newAllergies);
            Vendor market = new Vendor("Cane's", true);
            string appleJuice = "Apple Juice	3	150	0	0	0	0	75	36	1	26	1	apples		2025 11 10	yes";
            string wholeMilk = "Whole Milk	1	150	8	5	0	35	130	13	0	12	8	Grade A Organic Milk, Vitamin D3	dairy	2021 10 15	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            bool expected = true;
            bool fail = false;
            bool actual;
            bool actualTwo;
            bool actualThree;

            //Act
            tester.addFunds(20, 12345);
            market.load(appleJuice, 3, 10); //is fine to consume
            market.load(wholeMilk, 3, 10); //has dairy
            market.load(fishBurger, 3, 10); //has fish
            actual = tester.buyOne(market, "Apple Juice", 12345); //expected to succeed
            actualTwo = tester.buyOne(market, "Whole Milk", 12345); //expected to fail, allergic to dairy
            actualThree = tester.buyOne(market, "Fishburger", 12345); //expected to fail, allergic to fish

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(fail, actualTwo);
            Assert.AreEqual(fail, actualThree);
        }
    }
}
