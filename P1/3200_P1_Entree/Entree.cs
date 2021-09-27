//Julian Harris
//Entree.cs

//9/22/21 created file.
//9/24/21 added default constructor, added properties. 
//9/25/21 added parameterized constructor. 
//9/26/21 added updated parameterized constructor for fileread, added dateTime vars and expired. 

using System;

namespace _3200_P1_Entree
{
    public class Entree
    {
        //Properties & methods go here

        //Vars of Entree, if we want higher security should all these be private and I make getters? Unsure. 
        //We do know that I need to make up whatever for the expiration dates and fridge stuff. 
        private string name;
        private float servings;  //servings and fats are floats as they have fractions. Make sure they can't be negative.
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
        
        private bool needsFridge;
        private bool inFridge;

        //Functions we will need are check expired, check spoiled, check if contains input, 
        //for checking if expired: return (DateTime.Now > expDate), or print result.

        public bool contained(string input)
        {
            if (contains.Length == 0)
                return false;
            int counter = 0;
            while (counter < contains.Length && contains[counter] != input) 
                counter++; 
            return (contains[counter] == input);
        }

        public bool expired(){ return (DateTime.Now > expDate);}

        public bool spoiled()
        {
            if (expired())
                return true;
            return (needsFridge && !inFridge);
        }

        public string getName() { return name; }

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
                        case 14:
                            expDate = Convert.ToDateTime(item);
                            break;
                        case 15:
                            if (item == "yes")
                                needsFridge = true;
                            else
                                needsFridge = false;
                            break;
                        case 16:
                            if (item == "yes")
                                inFridge = true;
                            else
                                inFridge = false;
                            break;
                    }
                }
            }
        } 
    }

}
