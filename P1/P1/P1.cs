//Julian Harris, 9/22/21
//P1.cs
//Main driver file for P1 Entree project. 

//9/25/21 added fileRead function. 
//9/26/21 added print function, unfinished.
//9/27/21 added outage, findFoodIndex.

using System;
using _3200_P1_Entree;

namespace P1
{
    class P1
    {
        const string filename = "EntreesTabDelimited.txt";
        
        static void Main(string[] args) 
        {
            Console.WriteLine("Welcome to P1!\n"); 

            _3200_P1_Entree.Entree[] foodList = new _3200_P1_Entree.Entree[10]; //class declaration with size 10

            fileRead(foodList);  //Loading data from txt file into class. 

            printNutrient("Cheez It", "calories", foodList); //testing printing a specific nutrient
            checkExpired("Cheez It", foodList); //testing if food expired
            checkSpoiled("Cheez It", foodList); //testing if food spoiled before an outage
            outage(foodList); //all fridged food should now spoil
            checkSpoiled("Cheez It", foodList); //If this food needed fridge, it should spoil
            checkContained("Cheez It", "egg", foodList); //testing if a food contains a particular thing
        }

        //all main implementation below

        public static void fileRead(Entree[] foodList) //reading line by line
        {
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filename);
           
            file.ReadLine(); //skips title line in txt file.
            while ((line = file.ReadLine()) != null && counter < 10)
            {
                foodList[counter] = new Entree(line);
                counter++;
            }
           
        }

        public static int findFoodIndex(string target, Entree[] foodList) //returns index for use with print or contains queries. 
        {
            int index = 0;
            while(foodList[index] != null && index < foodList.Length && foodList[index].getName() != target)
                index++;
            if (foodList[index] != null && foodList[index].getName() == target)
                return index;
            return -1; //if Entree is not found
        }

        public static void printNutrient(string target, string nutrient, Entree[] foodList)   //prints a specific value, i.e. calories or transFat
        {
            int index = findFoodIndex(target, foodList);
            if (index < 0)  //Checking to ensure valid entry
                Console.WriteLine($"We do not have the {target} Entree.");
            else
                foodList[index].printNutrient(nutrient);
        }

        public static void checkContained(string target, string input, Entree[] foodList)
        {
            int index = findFoodIndex(target, foodList);
            if (index < 0)  //Checking to ensure valid entry
                Console.WriteLine($"We do not have the {target} Entree.");
            else
                foodList[index].contained(input);
        }

        public static void checkExpired(string target, Entree[] foodList)  
        {
            int index = findFoodIndex(target, foodList);
            if (index < 0)  //Checking to ensure valid entry
                Console.WriteLine($"We do not have the {target} Entree.");
            else
                foodList[index].expired();
        }

        public static void checkSpoiled(string target, Entree[] foodList) 
        {
            int index = findFoodIndex(target, foodList);
            if (index < 0)  //Checking to ensure valid entry
                Console.WriteLine($"We do not have the {target} Entree.");
            else
                foodList[index].spoiled();
        }

            public static void outage(Entree[] foodList)
        {
            int index = 0;
            while (foodList[index] != null)
            {
                foodList[index].setInFridge(false);
                index++;
            }
            Console.WriteLine("Outage! All refrigerated food has spoiled.");
        }
    }
}
