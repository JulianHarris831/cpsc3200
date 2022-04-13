//Julian Harris
//DbetCustomerTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class DbetCustomerTest
    {
        [TestMethod]
        public void DbetCustomerBuyOneTest()
        {
            //Arrange
            uint sugarLimit = 35;
            DbetCustomer tester = new DbetCustomer(12345, sugarLimit);
            Vendor market = new Vendor("Cane's", true);
            string darkCola = "Dark Cola	1	140	0	0	0	0	45	39	0	39	0	carbonated water, high fructose corn syrup, caramel color, natural flavors, caffeine		2022 02 12	no";
            string nuggets = "Chicken Nuggets	1	470	30	5	0	65	900	30	2	0	22	chicken, breading		2021 12 26	yes";
            bool expected = true;
            bool fail = false;
            bool actual;
            bool actualTwo;

            //Act
            tester.addFunds(20, 12345);
            market.load(darkCola, 3, 10);
            market.load(nuggets, 3, 10);
            actual = tester.buyOne(market, "Chicken Nuggets", 12345); //expected to pass
            actualTwo = tester.buyOne(market, "Dark Cola", 12345); //expected to fail, too sugary

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(fail, actualTwo);
        }
    }
}
