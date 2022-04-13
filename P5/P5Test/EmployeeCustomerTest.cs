//Julian Harris
//EmployeeCustomerTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class EmployeeCustomerTest
    {
        //Testing for Customer interface
        [TestMethod]
        public void addFundsTest()
        {
            //Arrange
            uint accountNum = 12345;
            Customer testBase = new Customer(accountNum);
            EmployeeCustomer test = new EmployeeCustomer(1, accountNum, "Jimmy", "Cane's", testBase); //1st payment level of 200
            bool expected = true;
            bool actual;
            //Act
            actual = test.addFunds(200, accountNum); //adding 200, returns true if successful
            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void buyOneTestOne()
        { //This is a test of the automatic paying through customer
            //Arrange
            uint accountNum = 12345;
            Customer testBase = new Customer(accountNum);
            EmployeeCustomer tester = new EmployeeCustomer(0, accountNum, "Jimmy", "Cane's", testBase); //pay level 0 so purchase goes to customer funds
            Vendor market = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2025 12 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2025 12 23	yes";
            bool expected = false;
            bool actual;
            bool actualTwo;

            //Act
            market.load(burger, 1, 10); //$1 price, can purchase.
            market.load(fishBurger, 500, 10);  //$500 price, can't afford.
            tester.addFunds(5, accountNum); //$5 can afford the normal burger
            actual = tester.buyOne(market, "Fishburger", accountNum); //invalid purchase, cannot afford.
            actualTwo = tester.buyOne(market, "Hamburger", accountNum); //valid purchase

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, actualTwo);
        }

        [TestMethod]
        public void buyOneTestTwo()
        { //This is a test of the paying through paycheck
            //Arrange
            uint accountNum = 12345;
            Customer testBase = new Customer(accountNum);
            EmployeeCustomer tester = new EmployeeCustomer(1, accountNum, "Jimmy", "Cane's", testBase);
            Vendor market = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2025 12 23	yes";
            bool expected = true;
            bool actual;

            //Act
            market.load(burger, 150, 10); //$150 price, can purchase once
            tester.payday();
            tester.setLevel(); //ensuring we have our $200 beforehand
            tester.addFunds(1000, accountNum); //account gets reimbursed anyways, but will fail if it can't afford. 
            actual = tester.buyOne(market, "Hamburger", accountNum); //invalid purchase, cannot afford.

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void buyTest()
        {  //All complex testing goes into buyOne, as this method only echos that. Just testing this runs.
           //Arrange
            uint accountNum = 12345;
            Vendor market = new Vendor("Cane's", true);
            Customer testBase = new Customer(accountNum);
            EmployeeCustomer tester = new EmployeeCustomer(1, accountNum, "Jimmy", "Cane's", testBase);
            string[] list = new string[5];
            list[0] = "Cheesy Tortilla Chips	1	150	8	1	0	0	210	18	1	1	2	cheese, corn, salt	dairy	2025 10 11	no";
            list[1] = "Jalapeno Chips	2	300	18	2	0	0	360	32	3	2	5	potato, jalapeno, salt		2025 05 12	no";
            list[2] = "Cheese Puffs	6	150	10	1.5	0	6	200	13	0	1	2	corn, cheese, whey, salt	dairy	2019 04 10	no"; //expired, will fail
            list[3] = "Carrot Sticks	1	35	0	0	0	0	60	8	2	4	1	carrots		2025 10 25	yes";
            list[4] = "Apple Slices	1	130	0	0	0	0	0	34	5	25	1	apples		2025 10 24	yes";

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

        //Testing for Employee interface
        [TestMethod]
        public void paydayTestOne()
        {
            //Arrange
            Customer testBase = new Customer(12345);
            EmployeeCustomer test = new EmployeeCustomer(1, 12345, "Jimmy", "Cane's", testBase); //1st payment level of 200
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
            Customer testBase = new Customer(12345);
            EmployeeCustomer test = new EmployeeCustomer(2, 12345, "Jimmy", "Cane's", testBase); //1st payment level of 200
            double expected = 400;
            double actual;
            //Act
            test    .payday(); //should add 400
            actual = test.getBalance();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void paydayTestThree()
        {
            //Arrange
            Customer testBase = new Customer(12345);
            EmployeeCustomer test = new EmployeeCustomer(3, 12345, "Jimmy", "Cane's", testBase); //1st payment level of 200
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
            Customer testBase = new Customer(12345);
            EmployeeCustomer test = new EmployeeCustomer(1, 12345, "Jimmy", "Cane's", testBase); //1st payment level of 200
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
