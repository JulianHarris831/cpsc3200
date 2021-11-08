//Julian Harris
//CarbCustomerTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3;

namespace P3Test
{
    [TestClass]
    public class CarbCustomerTest
    {
        [TestMethod]
        public void CarbCustomerBuyOneTest()
        {
            //Arrange
            uint CarbLimit = 35;
            CarbCustomer tester = new CarbCustomer(12345, CarbLimit);
            Vendor market = new Vendor("Cane's", true);
            string darkCola = "Dark Cola	1	140	0	0	0	0	45	39	0	39	0	carbonated water, high fructose corn syrup, caramel color, natural flavors, caffeine		2022 02 12	no";
            string nuggets = "Chicken Nuggets	1	470	30	5	0	65	900	30	2	0	22	chicken, breading		2021 12 26	yes";
            bool expected = true;
            bool fail = false;
            bool actual;
            bool actualTwo;

            //Act
            tester.addFunds(20, 12345);
            market.load(darkCola, 3, 10); //has 39 carbs
            market.load(nuggets, 3, 10); //has 30 carbs
            actual = tester.buyOne(market, "Chicken Nuggets", 12345); //expected to pass
            actualTwo = tester.buyOne(market, "Dark Cola", 12345); //expected to fail, too many carbs

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(fail, actualTwo);
        }
    }
}
