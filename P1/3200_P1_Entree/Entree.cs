//Julian Harris
//Entree.cs

//9/22/21 created file.
//9/24/21 added default constructor, added properties. 
//9/25/21 added parameterized constructor. 
//9/26/21 added updated parameterized constructor for fileread, added dateTime vars, expired, spoiled, contained.  
//9/27/21 added setInFridge, setExp, randomBool.
//9/28/21 updated outage, added printNutrient.
//9/29/21 added unit test helper functions

//Class invariants:
//Entree object needs unchanging name, servings, calories, totalFat, satFat, transFat,
//cholesterol, carbs, fiber, sugars, protein, ingredients, and contains.

//Interface invariants:
//File can't have any additional data

using System;

namespace _3200_P1_Entree
{
    public class Entree
    {
        private string name;
        private float servings;  //servings and fats are floats as they have fractions.
        private uint calories;
        private float totalFat;
        private float satFat;
        private float transFat;

        private uint cholest; //other nutrients are ints as they are whole numbers. Can't be negative.
        private uint sodium;
        private uint carbs;
        private uint fiber;       
        private uint sugars;
        private uint protein; 
        
        private string ingredients;
        private string[] contains;    ////Stored in arr to make it easier to check w/input data

        private DateTime expDate;
        private bool needsFridge;  //All this data needs private helpers, as they can't get data from the file. 
        private bool inFridge;

        //PRE: N/A
        //POST: Returns a DateTime object.
        private DateTime setExp()   //helper for expired
        {
            Random addDate = new Random();
            DateTime start = new DateTime(2020, 1, 1);  //setting the range for our 
            DateTime end = new DateTime(2025, 1, 1);    //expiration dates
            int range = (end - start).Days;
            return start.AddDays(addDate.Next(range));
        }

        //PRE: N/A
        //POST: Returns a bool
        private bool randomBool()  //helper for spoiled
        {
            Random boolGen = new Random();
            return (boolGen.Next(2) == 0); 
        }

        //PRE: Requires a string with function call of desired ingredient.
        //POST: Returns different bool whether found or not found.
        public bool contained(string input)  //Prints whether or not input was in Entree. 
        {
            if (contains.Length == 0)
            {
                Console.WriteLine($"Entree {name} does not contain {input}.");
                return false;
            }
            else
            {
                int counter = 0;
                while (counter < contains.Length && contains[counter] != input)
                    counter++;
                if (counter < contains.Length && contains[counter] == input)
                {
                    Console.WriteLine($"Entree {name} contains {input}");
                    return true;
                }
                Console.WriteLine($"Entree {name} does not contain {input}.");
                return false;
            }
        }

        //PRE: expDate must be set before this runs.
        //POST: Returns bool of whether or not expired.
        public bool expired() //bool functionality is for unit testing
        {
            if (DateTime.Now > expDate) //checks if the expiration date has passed. 
            { 
                Console.WriteLine($"Entree {name} has expired.");
                return true;
            }
            Console.WriteLine($"Entree {name} has not expired yet.");
            return false;
        }

        //PRE: expDate, needsFridge & inFridge must be set before this runs.
        //POST: Returns bool of whether or not spoiled.
        public bool spoiled()  //bool functionality is for unit testing 
        {
            if (DateTime.Now > expDate || (needsFridge && !inFridge)) //Checks if expired already OR if it wasn't refrigerated.
            {
                Console.WriteLine($"Entree {name} has spoiled.");
                return true;
            }
            Console.WriteLine($"Entree {name} has not spoiled yet.");
            return false;
        }

        //PRE: N/A
        //POST: N/A
        public string getName() { return name; }

        //PRE: N/A
        //POST: N/A
        public DateTime getExpDate() { return expDate; }  //for unit testing

        //PRE: N/A
        //POST: N/A
        public bool getNeedsFridge() { return needsFridge; } //for unit testing

        //PRE: N/A
        //POST: N/A
        public void setNeedsFridge(bool input) { needsFridge = input; } //for testing outage in unit test

        //PRE: N/A
        //POST: N/A
        public void setNotExpired() { expDate = new DateTime(2030, 1, 1); } //for ensuring consistent unit test

        //PRE: N/A
        //POST: N/A
        public void setInFridge(bool input) { inFridge = input; }

        //PRE: N/A
        //POST: N/A
        public bool getInFridge() { return inFridge; } //for unit testing

        //PRE: N/A
        //POST: N/A
        public void outage() { inFridge = false; }

        //PRE: Requires a string with function call of desired nutrient to print.
        //POST: N/A
        public void printNutrient(string target) //this will print a specific nutrient information var. 
        {
            switch (target)
            {
                case "calories":
                    Console.WriteLine($"{name} {target} info: {calories}.");
                    break;
                case "servings":
                    Console.WriteLine($"{name} {target} info: {servings}");
                    break;
                case "total fat":
                    Console.WriteLine($"{name} {target} info: {totalFat}");
                    break;
                case "saturated fat":
                    Console.WriteLine($"{name} {target} info: {satFat}");
                    break;
                case "trans fat":
                    Console.WriteLine($"{name} {target} info: {transFat}");
                    break;
                case "cholesterol":
                    Console.WriteLine($"{name} {target} info: {cholest}");
                    break;
                case "sodium":
                    Console.WriteLine($"{name} {target} info: {sodium}");
                    break;
                case "carbohydrates":
                    Console.WriteLine($"{name} {target} info: {carbs}");
                    break;
                case "fiber":
                    Console.WriteLine($"{name} {target} info: {fiber}");
                    break;
                case "sugars":
                    Console.WriteLine($"{name} {target} info: {sugars}");
                    break;
                case "protein":
                    Console.WriteLine($"{name} {target} info: {protein}");
                    break;
                default:
                    Console.WriteLine($"{name} does not have {target} info, sorry.");
                    break;
            }
        }

        public Entree(string data)  //Pass the unparsed string into constructor, use String.Split  
        {
            string[] items = data.Split('\t');
            for(int i = 0; i < items.Length; i++) //now set all vars in order, switch cause we know which var is when
            {
                string item = items[i];

                if (item != "") {
                    switch (i) //add a case for each var
                    {
                        case 0:  //potentially check if invalid, have a return. 
                            name = item; 
                            break;
                        case 1:
                            servings = float.Parse(item); //turning string into float and inserting
                            break;
                        case 2:
                            calories = uint.Parse(item);
                            break;
                        case 3:
                            totalFat = float.Parse(item);
                            break;
                        case 4:
                            satFat = float.Parse(item);
                            break;
                        case 5:
                            transFat = float.Parse(item);
                            break;
                        case 6:
                            cholest = uint.Parse(item);
                            break;
                        case 7:
                            sodium = uint.Parse(item);
                            break;
                        case 8:
                            carbs = uint.Parse(item);
                            break;
                        case 9:
                            fiber = uint.Parse(item);
                            break;
                        case 10:
                            sugars = uint.Parse(item);
                            break;
                        case 11:
                            protein = uint.Parse(item);
                            break;
                        case 12:
                            ingredients = item; 
                            break;
                        case 13:
                            contains = item.Split('$');   
                            break;
                    }
                }
            }
            expDate = setExp(); //sets random expiration date, it's independent data
            needsFridge = randomBool();
            inFridge = needsFridge;   //will matter in power outage
        } 
    }
    //Implementation invariants:
    //Data passed into constructor must be formatted in the correct order according to cases
}
