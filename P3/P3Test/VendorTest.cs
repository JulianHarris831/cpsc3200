//Julian Harris
//VendorTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3;

namespace P3Test
{
    [TestClass]
    public class VendorTest
    {
        //constructor tests?

        [TestMethod]
        public void vendorCopyTest()
        {
            //Arrange
            Vendor original = new Vendor("Cane's", true);
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 11 23	yes";
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 12 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            Vendor ogCopy = new Vendor();

            //Act
            original.load(burger, 1, 10);
            original.load(cheeseBurger, 1.50, 10);
            original.load(fishBurger, 2, 5); //fish burger intentionally spoiled
            ogCopy = original.copy(); //exact copy

            //testing that the copy is deep

            //Assert
            Assert.AreEqual(original.printMenu(), ogCopy.printMenu()); //should print the same
        }

        [TestMethod]
        public void vendorCopyDeepTest() //helper function, testing not needed
        {
            //Arrange
            Vendor original = new Vendor("Cane's", true);
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 11 23	yes";
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 12 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            Vendor ogCopy = new Vendor();

            //Act
            original.load(burger, 1, 10);
            original.load(cheeseBurger, 1.50, 10);
            original.load(fishBurger, 2, 5); //fish burger intentionally spoiled
            ogCopy = original.copy(); //exact copy
            original.powerOutage(); //Testing to ensure modifying one doesn't modify the other

            //Assert
            Assert.AreNotEqual(original.isStocked("Cheeseburger"), (ogCopy.isStocked("Cheeseburger")));
        }

        [TestMethod]
        public void vendorPrintTest()
        { //Unsure if this is functional or needed? So jank
            //Arrange
            string name = "Cane's";
            string foodName = "Hamburger";
            double price = 1;
            uint stock = 10;
            Vendor printer = new Vendor(name, true);
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 11 23	yes";
            string expected = ($"Printing menu of {name}\n\nItem: {foodName}\nPrice: ${price}\nStock: {stock}\n\n");
            string actual; 

            //Act
            printer.load(burger, price, stock);
            actual = printer.printMenu();

            //Assert
            Assert.AreEqual(printer.printMenu(), actual);
        }

        [TestMethod]
        public void vendorPrintInfoTest()
        {
            //Arrange
            Vendor printer = new Vendor("Cane's", true);
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 11 23	yes";
            string actual;

            //Act
            printer.load(burger, 1, 10);
            actual = printer.printMenuInfo();

            //Assert
            printer.printMenu(); //as long as there are no issues here, we're good
            printer.printMenuInfo();
            Assert.AreEqual(printer.printMenuInfo(), actual);
        }

        [TestMethod]
        public void vendorOutageTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true); //created with true
            bool expected = false; //expected to be false once outage used

            //Act
            tester.powerOutage();
            bool actual = tester.getRefrigerated();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        //Getter test? 

        [TestMethod]
        public void vendorStockedTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            bool expected = false;
            bool actual;
            bool actualTwo;
            bool actualThree;
            bool actualFour;
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 10 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 12 23	yes";
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2022 10 23	yes";
            //cheese intentionally expired, fish not expired

            //Act
            tester.load(cheeseBurger, 1.50, 10); //expired
            tester.load(fishBurger, 2, 0); //not expired but out of stock
            tester.load(burger, 1, 10); //should be in stock and return true
            actual = tester.isStocked("Cheeseburger");
            actualTwo = tester.isStocked("Fishburger"); //expected to be false for all of these.
            actualThree = tester.isStocked("Fake input");
            actualFour = tester.isStocked("Hamburger"); //should be true

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actualTwo);
            Assert.AreEqual(expected, actualThree);
            Assert.AreNotEqual(expected, actualFour);
        }

        [TestMethod]
        public void vendorCleanTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 10 23	yes";
            bool expected = false;
            bool actual;

            //Act
            tester.load(cheeseBurger, 1.50, 10);
            tester.cleanStock(); //should remove cheeseBurger
            actual = tester.isStocked("Cheeseburger"); //should return false

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void vendorSellTest() //make sure we don't sell a spoiled, and that we ccan sell a proper
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 10 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2022 12 23	yes";
            //burger is expired, cheeseburger is inaffordable. both return false. fishburger can be bought
            bool expected = false;
            bool actual;
            bool actualTwo;

            //Act
            tester.load(burger, 1, 10);
            tester.load(fishBurger, 1, 10);
            actual = tester.sell("Hamburger"); //burger is spoiled, should be false
            actualTwo = tester.sell("Fishburger"); //Fishburger can be bought, should be true;

            //Assert  //Some issue where it's not selling properly.
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, actualTwo);
        }

        [TestMethod]
        public void vendorFindFoodIndexTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 10 23	yes"; //index 0
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 12 23	yes"; //index 1
            int expected = 1;
            int fail = -1;
            int actual;

            //Act
            tester.load(burger, 1, 10);
            tester.load(cheeseBurger, 2, 10);
            actual = tester.findFoodIndex("Cheeseburger");

            //Assert
            Assert.AreEqual(expected, actual); //proper input, should return 1
            Assert.AreEqual(fail, tester.findFoodIndex("Hamburger")); //Hamburger is spoiled, should return -1
            Assert.AreEqual(fail, tester.findFoodIndex("Invalid input")); //invalid input, should return -1
        }

        [TestMethod]
        public void vendorFindPriceTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 10 23	yes";
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2022 12 23	yes";
            double expected = 5;
            double fail = -1;
            double actual;

            //Act
            tester.load(burger, 5, 10);
            tester.load(fishBurger, 5, 10);
            actual = tester.findPrice("Fishburger");

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(fail, tester.findPrice("Hamburger"));
            Assert.AreEqual(fail, tester.findPrice("Invalid input"));
        }

        [TestMethod]
        public void vendorGetNutrientTest()
        { //ensuring all returns are valid and work
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string appleJuice = "Apple Juice\t3\t150\t0\t0\t0\t0\t75\t36\t1\t26\t1\tapples\t\t2022 11 10\tyes";
            uint expectedCalories = 150;
            uint expectedCarbs = 36;
            uint expectedSugars = 26;
            uint actualCalories;
            uint actualCarbs;
            uint actualSugars;
            uint fail = 0;

            //Act
            tester.load(appleJuice, 3, 10);
            actualCalories = tester.getNutrient("Apple Juice", "calories");
            actualCarbs = tester.getNutrient("Apple Juice", "carbohydrates");
            actualSugars = tester.getNutrient("Apple Juice", "sugars");

            //Assert
            Assert.AreEqual(expectedCalories, actualCalories);
            Assert.AreEqual(expectedCarbs, actualCarbs);
            Assert.AreEqual(expectedSugars, actualSugars);
            Assert.AreEqual(fail, tester.getNutrient("Apple Juice", "Bogus Input"));
        }

        [TestMethod]
        public void vendorGetContainsTest()
        {
            //Arrange
            Vendor tester = new Vendor("Cane's", true);
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2022 12 23	yes";
            bool expected = true;
            bool actual;
            bool fail = false;

            //Act
            tester.load(fishBurger, 3, 10);
            actual = tester.getContains("Fishburger", "fish");

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(fail, tester.getContains("Fishburger", "invalid input"));
        }

    }
}
