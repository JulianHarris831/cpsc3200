//Julian Harris
//Entree.cs

//CLASS INVARIANTS:
//-Entree holds a name, nutrition facts, a list of ingredients, an expiration date,
//and refrigeration requirements.
//-Entree object is in a valid state when called with provided parameterized consturctor
//that has valid input. 

//INTERFACE INVARIANTS:
//-When creating an Entree, the client is responsible for the following
//1. All nutritional facts, expiration date etc in the constructor string 
//MUST be in proper order or the Entree class will not function.
//2. Contains list must be delimited by $ between items.
//3. Expiration date format must be YYYY/MM/DD

//IMPLEMENTATION INVARIANTS:
//-Client must request expiration/spoiled checks when they need it, that 
//data is not stored and must be calculated each time.

using System;

namespace P3
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
        private bool perishable;  //All this data needs private helpers, as they can't get data from the file. 

        //The functions that we will need that didn't exist before are...
        //a load function for later to work with arr, and printNutrientInfo

        private Entree() { } //privatize default constructor, restricting

        public Entree(string data) //basic load constructor, used for every valid Entree
        {
            string[] items = data.Split('\t');
            for (int i = 0; i < items.Length; i++) //now set all vars in order, switch cause we know which var is when
            {
                string item = items[i];

                if (item != "")
                {
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
                        case 14:
                            string[] newDate = item.Split(' ');
                            expDate = new DateTime(int.Parse(newDate[0]), int.Parse(newDate[1]),
                                int.Parse(newDate[2]));
                            break;
                        case 15:
                            perishable = (item == "yes");
                            break;
                    }
                }
            }
        }

        //PRE: None, can be called directly after constructor
        //POST: None, no permanent change
        public bool isExpired() { return (DateTime.Now > expDate); }

        //PRE: Requires a bool from the vendor or other container that tells whether or 
        //not it is currently refrigerated.
        //POST: None
        public bool isSpoiled(bool refrigerated) { return isExpired() || (perishable && !refrigerated); }

        //PRE: None
        //POST: None
        public string getName() { return name; }

        //PRE: Requires an allergen string to be checked with the contains list.
        //POST: None
        public bool contained(string input)  //returns whether or not input was in Entree
        {
            if (contains == null || contains.Length == 0)
                return false;

            int counter = 0;
            while (counter < contains.Length && contains[counter] != input)
                counter++;
            return (counter < contains.Length && contains[counter] == input);
        }

        //PRE: None
        //POST: None
        public string printNutrientInfo() 
        {
            return ($"Name: {name}\nServings: {servings}\nCalories: {calories}\nTotal fat: {totalFat}" +
                $"\nSaturated fat: {satFat}\nTrans fat: {transFat}\nCholesterol: {cholest}\n" +
                $"Sodium: {sodium}\nCarbohydrates: {carbs}\nFiber: {fiber}\nSugars: {sugars}\n" +
                $"Protein: {protein}\nIngredients: {ingredients}\n\n");
        }

        //PRE: Requires a string of the desired nutrient to be passed in. Returns 
        //0 if it is not one of the setup nutrients.
        //POST: None
        public uint getNutrient(string target) //this will print a specific nutrient information var. primarily as a helper for customer
        {
            if(target == "calories")
                return calories;
            if (target == "sugars")
                return sugars;
            if (target == "carbohydrates")
                return carbs;
            return 0; //assumed zero if not found. none of nutrient
        }

    }
}