//Julian Harris
//CustomerTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void customerAddFundsTest()
        {
            //Arrange
            uint validNum = 12345;
            uint invalidNum = 12344;
            Customer tester = new Customer(validNum);
            bool expected = false;
            bool actual;
            bool actualTwo;

            //Act
            actual = tester.addFunds(4.48, invalidNum); //invalid, false
            actualTwo = tester.addFunds(5.68, validNum); //valid, true

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, actualTwo);
        }

        [TestMethod]
        public void customerBuyOneTest()
        {
            //Arrange
            uint validNum = 12345;
            Customer tester = new Customer(validNum);
            Vendor market = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 12 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2022 12 23	yes";
            bool expected = false;
            bool actual;
            bool actualTwo;

            //Act
            market.load(burger, 1, 10); //$1 price, can purchase.
            market.load(fishBurger, 10, 10);  //$10 price, can't afford.
            tester.addFunds(5, validNum);
            actual = tester.buyOne(market, "Fishburger", validNum); //invalid purchase, cannot afford.
            actualTwo = tester.buyOne(market, "Hamburger", validNum); //valid purchase

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, actualTwo);
        }

        [TestMethod]
        public void customerBuyTest()
        { //just making sure this runs, as all it does is echo buyOne, so the complex testing must be there.
            //Arrange
            Vendor market = new Vendor("Cane's", true);
            Customer tester = new Customer(12345);
            string[] list = new string[5];
            list[0] = "Cheesy Tortilla Chips	1	150	8	1	0	0	210	18	1	1	2	cheese, corn, salt	dairy	2022 10 11	no";
            list[1] = "Jalapeno Chips	2	300	18	2	0	0	360	32	3	2	5	potato, jalapeno, salt		2022 05 12	no";
            list[2] = "Cheese Puffs	6	150	10	1.5	0	6	200	13	0	1	2	corn, cheese, whey, salt	dairy	2019 04 10	no";
            list[3] = "Carrot Sticks	1	35	0	0	0	0	60	8	2	4	1	carrots		2021 10 25	yes";
            list[4] = "Apple Slices	1	130	0	0	0	0	0	34	5	25	1	apples		2021 10 24	yes";

            //Act
            tester.addFunds(100, 12345); //just deposting 100 dollars to ensure they can buy it all
            market.load(list[0], 5, 10);
            market.load(list[1], 5, 10);
            market.load(list[2], 5, 10); //making everything in stock and affordable
            market.load(list[3], 5, 10);
            market.load(list[4], 5, 10);

            //Assert
            tester.buy(market, 12345, list); //buys everything in the list
        }
    }
}
