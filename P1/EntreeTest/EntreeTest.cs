//Julian Harris, 9/22/21
//EntreeTest.cs 

using _3200_P1_Entree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EntreeTest
{
    [TestClass]
    public class EntreeTest
    {
        [TestMethod]
        public void entreePrintFunctionality()
        {
            //Arrange
            string sampleData = "Fresh Brand - Sliced Apples	2.5	70	0	0	0	0	0	19	3	15	0	apples	";

            string validNutrientInput = "calories";
            string invalidNutrientInput = "donkyDonks";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);

            //Act (function calls here)
            testSnack.printNutrient(validNutrientInput);   //print is a void, as long as it runs with both valid and 
            testSnack.printNutrient(invalidNutrientInput); //invalid input it works.
        }

        [TestMethod]
        public void entreeExpireTest() 
        {
            //Arrange
            string sampleData = "Fresh Brand - Sliced Apples	2.5	70	0	0	0	0	0	19	3	15	0	apples	";
            
            //set exp data and spoil stuff
            bool expectedExp;
            bool actualExp;
            
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);

            //Act
            expectedExp = DateTime.Now > testSnack.getExpDate();
            actualExp = testSnack.expired();

            //Assert
            Assert.AreEqual(expectedExp, actualExp);
        }

        [TestMethod]
        public void entreeSpoiledTest()
        {
            //Assemble
            string sampleData = "Fresh Brand - Sliced Apples	2.5	70	0	0	0	0	0	19	3	15	0	apples	";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedSpoiled;
            bool actualSpoiled;

            //Act
            expectedSpoiled = (DateTime.Now > testSnack.getExpDate() || (testSnack.getNeedsFridge() && !testSnack.getInFridge()));
            actualSpoiled = testSnack.spoiled();

            //Assert
            Assert.AreEqual(expectedSpoiled, actualSpoiled);

        }

        [TestMethod]
        public void entreeOutageTest()
        {
            //Assemble
            string sampleData = "Fresh Brand - Sliced Apples	2.5	70	0	0	0	0	0	19	3	15	0	apples	";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedOutage;
            bool actualOutage;
            testSnack.setNeedsFridge(true);
            testSnack.setNotExpired();

            //Act
            expectedOutage = true;
            testSnack.outage();  //Food will now spoil if it needed fridge
            actualOutage = testSnack.spoiled();

            //Assert
            Assert.AreEqual(expectedOutage, actualOutage);
        }

        [TestMethod]
        public void entreeContainedTest()
        {
            //Arrange
            string sampleData = "Planters Nuts on the Go Salted Peanuts	1	170	14	2	0	0	95	5	2	1	7	Peanuts$Peanut and/or Cottonseed oil$sea salt	peanuts";

            bool expectedContain;
            bool actualContain;

            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);

            //Act
            expectedContain = true;
            actualContain = testSnack.contained("peanuts");  //seeing if it properly detects peanuts

            testSnack.contained("Garbanzo"); //testing invalid input

            //Assert
            Assert.AreEqual(expectedContain, actualContain);
        }

        //GET & SET tests below, repetitive.

        [TestMethod]
        public void entreeGetNameTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            string expectedName;
            string actualName;

            //Act
            expectedName = "Skinny Pop Popcorn";
            actualName = testSnack.getName();

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }
        [TestMethod]
        public void entreeGetExpDateTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            DateTime expectedDate;
            DateTime actualDate;
            testSnack.setNotExpired();

            //Act
            expectedDate = new DateTime(2030, 1, 1);
            actualDate = testSnack.getExpDate();

            //Assert
            Assert.AreEqual(expectedDate, actualDate);
        }
        [TestMethod]
        public void entreeGetNeedsFridgeTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedFridge;
            bool actualFridge;
            testSnack.setNeedsFridge(true);

            //Act
            expectedFridge = true;
            actualFridge = testSnack.getNeedsFridge();

            //Assert
            Assert.AreEqual(expectedFridge, actualFridge);
        }

        [TestMethod]
        public void entreeSetNeedsFridgeTest()  //Get and set are the same as set is required to have a consistent var.
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedFridge;
            bool actualFridge;

            //Act
            testSnack.setNeedsFridge(true);
            expectedFridge = true;
            actualFridge = testSnack.getNeedsFridge();

            //Assert
            Assert.AreEqual(expectedFridge, actualFridge);
        }
        [TestMethod]
        public void entreeSetNotExpiredTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedExpired;
            bool actualExpired;

            //Act
            testSnack.setNotExpired();
            expectedExpired = false;
            actualExpired = testSnack.expired();

            //Assert
            Assert.AreEqual(expectedExpired, actualExpired);
        }
        [TestMethod]
        public void entreeGetInFridgeTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedFridge;
            bool actualFridge;

            //Act
            testSnack.setInFridge(true);
            expectedFridge = true; 
            actualFridge = testSnack.getInFridge();

            //Assert
            Assert.AreEqual(expectedFridge, actualFridge);
        }

        [TestMethod]
        public void entreeSetInFridgeTest()
        {
            //Arrange
            string sampleData = "Skinny Pop Popcorn	1	100	6	0.5	0	9	45	9	9	0	2	Popcorn$Sunflower oil$salt";
            _3200_P1_Entree.Entree testSnack = new _3200_P1_Entree.Entree(sampleData);
            bool expectedFridge;
            bool actualFridge;

            //Act
            testSnack.setInFridge(true);
            expectedFridge = true;
            actualFridge = testSnack.getInFridge();

            //Assert
            Assert.AreEqual(expectedFridge, actualFridge);
        }
    }
}
