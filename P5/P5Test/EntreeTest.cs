//Julian Harris
//EntreeTest.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;

namespace P5Test
{
    [TestClass]
    public class EntreeTest
    {
        [TestMethod]
        public void entreeIsExpiredTest()
        {
            //Arrange
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2022 10 23	yes";
            string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 10 23	yes";
            Entree testerValid = new Entree(burger);
            Entree testerInvalid = new Entree(cheeseBurger);
            bool expected = false;
            bool validActual; //expected to be false, not expired
            bool invalidActual; //expected to be true, is expired

            //Act
            validActual = testerValid.isExpired();
            invalidActual = testerInvalid.isExpired();

            //Assert
            Assert.AreEqual(expected, validActual);
            Assert.AreNotEqual(expected, invalidActual);
        }

        [TestMethod]
        public void entreeIsSpoiledTest()
        {
            //Arrange
            string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2022 10 23	yes";
            Entree tester = new Entree(burger);
            bool expected = true;
            bool actual;

            //Act
            if (!tester.isSpoiled(true) && tester.isSpoiled(false))
                actual = true; //if it returns spoiled when unrefrigerated, and not spoiled when it is.
            else
                actual = false;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void entreeGetNameTest() //checks that name is fetched properly
        {
            //Arrange
            string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2022 10 23	yes";
            Entree tester = new Entree(burger);
            string expected = "Hamburger";
            string actual;

            //Act
            actual = tester.getName();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void entreeContainedTest() //testing
        {
            //Arrange
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            Entree tester = new Entree(fishBurger);
            bool expected = true;
            bool actual;
            bool actualTwo;

            //Act
            actual = tester.contained("fish"); //fishBurger should have one contains, fish.
            actualTwo = tester.contained("invalid input");

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected, actualTwo);
        }

        [TestMethod]
        public void entreePrintNutrientInfoTest()
        {
            //Arrange
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            Entree tester = new Entree(fishBurger);

            //Act
            tester.printNutrientInfo(); //as long as print works without errors, we are good.
        }

        [TestMethod]
        public void entreeGetNutrientTest() //testing returns correct values
        {
            //Arrange
            string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
            Entree tester = new Entree(fishBurger);
            uint expectedCarbs = 38;
            uint expectedSugars = 5;
            uint actualCarbs;
            uint actualSugars;

            //Act 
            actualCarbs = tester.getNutrient("carbohydrates");
            actualSugars = tester.getNutrient("sugars");

            //Assert
            Assert.AreEqual(expectedCarbs, actualCarbs);
            Assert.AreEqual(expectedSugars, actualSugars);
        }

    }
}
