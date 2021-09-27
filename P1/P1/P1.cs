//Julian Harris, 9/22/21
//P1.cs
//Main driver file for P1 Entree project. 

//9/25/21 added fileRead function. 
//9/26/21

using System;
using _3200_P1_Entree;

//Constant expiration date not needed, use DateTime.now

namespace P1
{
    class P1
    {
        const string filename = "EntreesTabDelimited.txt";
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to P1"); 

            _3200_P1_Entree.Entree[] foodList = new _3200_P1_Entree.Entree[10]; //class declaration with size 10

            fileRead(foodList);  //Loading data from txt file into class. 
        }

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

        //public static void find(string input, Entree[] foodList), searches the array and returns the matched Entree. 

        public static void foodPrint(Entree[] foodList)
        {

        }
    }
}
