//Julian Harris
//Vendor.cs

using System;

//LEFT TO DO 
//DOCUMENTATION

//CLASS INVARIANTS:
//-Vendor holds a resizing array of Items, which contain class Entree and relevant info,
//and a vendor name and whether or not the vendor is refrigerated.
//-Vendor is in a valid state when called with the parameterized constructor, or when created 
//through the copy function. Blank constructor is for deep copying.
//-Vendor can work without its name or refrigerated properly created.

//INTERFACE INVARIANTS:
//-When creating a vendor, the client is responsible for the following:
//	1. For the string in the load function, formatting MUST be in proper order for use with Entree.
//  2. Client cannot insert a negative float for the price.

//IMPLEMENTATION INVARIANTS:
//-Client will not get anything from calls until Item data is loaded into the array,
//though the Vendor will not break if called before then.
//-Client must request the cleanStock function to remove invalid items, though they will never
//be sold when expired or out of stock.
//-All data within Entrees must be called and found through a method and is not saved.

namespace P3
{
	public class Vendor //class declaration
	{
		private string vendorName = "empty";
		private bool refrigerated = false;
		public struct Item
        {
			public Entree food;
			public uint stock;
			public double price;
        }
		private Item[] foodList; //resizing array to store Entrees.
		private uint count = 0;

		public Vendor(string newName, bool isFridge) //basic constructor
		{
			vendorName = newName;
			refrigerated = isFridge;
		}

		public Vendor() { } //needed for the copy method below

		//PRE: For a proper copy, must have Entrees loaded already, or it will only save the name and fridge.
		//POST: A new deep copy of Vendor is returned to main.
		public Vendor copy() 
        { //creates a deep copy and returns it.
			Vendor newCopy = new Vendor(vendorName, refrigerated);
			if(foodList != null)
				newCopy.foodList = new Item[foodList.Length]; //make a new arr
			for(int i = 0; i < count; i++) //copy arr loop, won't run if count is zero
				newCopy.load(foodList[i]);
			return newCopy;
        }

		//PRE: A valid Item struct is passed in with non null data.
		//POST: New Entree object is added to the Vendor array.
		private void load(Item data) //private as only meant for use in copy
        {
			if (foodList == null) //if arr is currently empty, create one starting at 5.
				foodList = new Item[5];
			if (count >= foodList.Length)
				Array.Resize(ref foodList, foodList.Length + 5);
			//now we are sure of no size or null issues, insert
			Item newData;
			newData.food = data.food; //Shallow copy, but makes no difference
			newData.price = data.price;
			newData.stock = data.stock;
			foodList[count] = newData;
			count++;
        }

		//PRE: A properly formatted string must be passed in to avoid error, a non 
		//negative float price, and then a stock.
		//POST: New Entree object is added to the Vendor array.
		public void load(string data, double newPrice, uint newStock)
        {
			if(foodList == null) //if arr is currently empty, create one starting at 5.
				foodList = new Item[5];
			if(count >= foodList.Length) //if arr is at the max size, add 5 to max size.
				Array.Resize(ref foodList, foodList.Length + 5); 
			//now we are sure of no size or null issues, insert.
			foodList[count].food = new Entree(data);
			foodList[count].price = newPrice;
			foodList[count].stock = newStock;
			count++;
		}

		//PRE: None, though proper output requires at least one load to be called.
		//POST: None
		public string printMenu() 
		{ //builds a menu string and returns for printing
			if (foodList == null)
				return ("There is nothing on the menu, sorry!\n\n");
			string output = ($"Printing menu of {vendorName}\n\n");
			for (int i = 0; i < count; i++)
				output += ($"Item: {foodList[i].food.getName()}\n" +
					$"Price: ${foodList[i].price}\nStock: {foodList[i].stock}\n\n");
			return output;
		}

		//PRE: None, though proper output requires at least one load to be called.
		//POST: None
		public string printMenuInfo()
        { //builds a nutrient info string and returns for printing
			if (foodList == null || count < 1)
				return ("There is nothing on the menu, sorry!\n\n");
			string output = ($"Printing nutrient info of {vendorName}\n\n");
			for (int i = 0; i < count; i++)
				output += (foodList[i].food.printNutrientInfo());
			return output;
        }

		//PRE: None
		//POST: Refrigerated is set to false, only a change if it was true.
		public void powerOutage() { refrigerated = false; }

		//PRE:
		//POST:
		public bool getRefrigerated() { return refrigerated; }

		//PRE: For proper call, requires at least one load to have been called. Also 
		//requires a foodName to be passed in.
		//POST: None
		public bool isStocked(string foodName)
        {
			if (foodList == null || count == 0)
				return false; //arr is for sure empty
			uint counter = 0;
			bool stockStatus = false;
			while (counter < count && stockStatus == false)
			{ //if the food is there, and there is more than 0, and it is not spoiled, we say it's in stock
				if (foodList[counter].food.getName() == foodName && !(foodList[counter].food.isSpoiled(refrigerated)) && foodList[counter].stock > 0)
					stockStatus = true;
				counter++;
			}
			return stockStatus;
        }

		//PRE: None, though for proper call requires some Entrees to be loaded.
		//POST: foodList array may have changed size and been rearranged.
		public void cleanStock()
        {
			Console.WriteLine($"Cleaning stock of {vendorName}...");
			if (foodList == null || count == 0)
				Console.WriteLine("There is nothing to clean!\n");
            else
            {
				for (int i = 0; i < count; i++)
                {
					if (!isStocked(foodList[i].food.getName()))
                    { //if this is out of stock (spoiled or out of items), remove
						Console.WriteLine($"Removing {foodList[i].food.getName()}...");
						foodList[i] = foodList[count - 1];
						Array.Resize(ref foodList, (int)count - 1);
						count--;
						i--; //to make sure we stay in line with the removed items.
                    }
                }
				Console.WriteLine("The stock has been cleaned!\n");
			}
        }

		//PRE: Requires a string to be passed in from main.
		//POST: Stock of an Item in foodList array may be decremented.
		public bool sell(string foodName)
        {
			int foodIndex = findFoodIndex(foodName); //checks if stocked
			if (foodIndex < 0)
			{
				Console.WriteLine($"The item {foodName} is not available.\n");
				return false;
			}

			foodList[foodIndex].stock -= 1;
			Console.WriteLine($"Sale complete! Enjoy your {foodList[foodIndex].food.getName()}!\n");
			return true;
        }

		//PRE: Requires a food string to be passed in for a check.
		//POST: None
		public int findFoodIndex(string foodName)
        { //CANNOT be used for clean, or isStocked, as this will never return the index of a spoiled or expired item.
			if(foodList == null || !isStocked(foodName))
				return -1;
            else
            {
				bool done = false;
				int index = 0;
				while(!done && index < count)
                {
					if (foodList[index].food.getName() == foodName)
						done = true;
					else
						index++;
                }
				return index;
			}
        }

		//PRE: Requires a food string to be passed in for a check
		//POST: None
		public double findPrice(string foodName) //helper for purchasing in customer
        {
			int foodIndex = findFoodIndex(foodName);
			if (foodIndex < 0)
				return -1;
			return foodList[foodIndex].price;
        }

		//PRE: Requires a food string to be passed in for a check, and a desired nutrient string.
		//POST: None
		public uint getNutrient(string foodName, string target)
        {
			int foodIndex = findFoodIndex(foodName);
			if (foodIndex < 0)
				return 0;
			return foodList[foodIndex].food.getNutrient(target);
        }

		//PRE: Requires a food string to be passed in for a check, and a target allergen.
		//POST: None
		public bool getContains(string foodName, string target)
        {
			int foodIndex = findFoodIndex(foodName);
			if (foodIndex < 0)
				return false;
			return foodList[foodIndex].food.contained(target);
        }

	}
}
