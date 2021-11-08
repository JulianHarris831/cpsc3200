//Julian Harris
//P3.cs

using System;

namespace P3
{
    public class P3
    {
        const string filename = "P3Data.txt";


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to P3! No output means a failed purchase.");
             
            Vendor market = new Vendor("Surplus Snacks", true); 
            Customer[] buyers = new Customer[4]; //heterogeneous collection of customers
            string[] menuList = new string[15]; //for randomly picking items to buy, we have a list of names

            marketLoad(market);  //vendor loaded with 15 entrees
            buyersLoad(buyers); //0 is default, 1 is diabetic, 2 is carb, 3 is allergic
            loadMenuList(ref menuList);

            Console.WriteLine(market.printMenu()); //printing menu to see out of stocks

            //Default customer buys enough food to run out of money
            Console.WriteLine("\nTesting the default customer buyOne.");
            buy(buyers[0], menuList, market); //buying one and then multiple foods with default customer
            Console.WriteLine("\nTesting the default customer buy.");
            buyMeal(buyers[0], menuList, market);

            //Diabetic customer buys enough sugary foods to stop buying them
            Console.WriteLine("\nTesting the diabetic customer buyOne.");
            buy(buyers[1], menuList, market); //buying one and then multiple foods with DbetCustomer
            Console.WriteLine("\nTesting the diabetic customer buy.");
            buyMeal(buyers[1], menuList, market);

            //Carb customer buys enough carbs to stop buying them
            Console.WriteLine("\nTesting the carbs customer buyOne.");
            buy(buyers[2], menuList, market);
            Console.WriteLine("\nTesting the carbs customer buy.");
            buyMeal(buyers[2], menuList, market);

            //Allergic customer tries and fails to buy foods with allergies
            Console.WriteLine("\nTesting the allergic customer buyOne.");
            buy(buyers[3], menuList, market);
            Console.WriteLine("\nTesting the allergic customer buy.");
            buyMeal(buyers[3], menuList, market);

            //Otherwise, you're done that's it!!!!
            Console.WriteLine("\nP3 has finished! Hooray!");
        }


        //functional decomposition

        //PRE: Basic Vendor required to be declared and passed in, txt file with Entree data must be in the folder with the .exe
        //POST: Vendor loaded with 15 Entrees. 
        public static void marketLoad(Vendor market)
        { //load in fileName and stuff into market here, pass by reference keeps it in main
            string line;
            Random rnd = new Random();
            uint stock = 0;
            float price = 0;

            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            file.ReadLine(); //skips title line in txt file.
            while ((line = file.ReadLine()) != null)
            {
                //random num gen for stock (0-3), price (1 - 10)
                price = (float)rnd.Next(10);
                stock = (uint)rnd.Next(3);
                market.load(line, price, stock); 
            }
        }

        //PRE: Array of Customers must be declared and passed in.
        //POST: Array of Customers is loaded with 4 different Customer classes.
        public static void buyersLoad(Customer[] buyers)
        { //load up list of buyers here, returned to main through pass by reference
            string[] allergyList = new string[3];
            allergyList[0] = "peanuts";
            allergyList[1] = "fish";
            allergyList[2] = "dairy";
            buyers[0] = new Customer(12345); //gave them all the same accountNumber for ease of use
            buyers[1] = new DbetCustomer(12345, 50); //diabetic can only have 25 grams of sugar
            buyers[2] = new CarbCustomer(12345, 30); //dieter can have 30 carbohydrates
            buyers[3] = new AllergyCustomer(12345, allergyList);

            //giving each buyer $20 the same way 
            for (int i = 0; i < 4; i++)
                buyers[i].addFunds(20, 12345);
        }

        //PRE: Array of strings declared with size 15 required to be passed in.
        //POST: Array of strings loaded with names for use with buy and buyMeal
        public static void loadMenuList(ref string[] menuList)
        { //size 15 array full of all possible menu names
            menuList[0] = "Whole Milk";
            menuList[1] = "Dark Cola";
            menuList[2] = "Root Beer";
            menuList[3] = "Orange Soda";
            menuList[4] = "Apple Juice";
            menuList[5] = "Cheesy Tortilla Chips";
            menuList[6] = "Jalapeno Chips";
            menuList[7] = "Cheese Puffs";
            menuList[8] = "Carrot Sticks";
            menuList[9] = "Apple Slices";
            menuList[10] = "Hamburger";
            menuList[11] = "Cheeseburger";
            menuList[12] = "Fishburger";
            menuList[13] = "Chicken Nuggets";
            menuList[14] = "Grilled Chicken Salad";
        }

        //PRE: Vendor loaded with Entrees, instantiated buyer, and loaded menuList required for proper execution.
        //POST: If successfully bought, stock in the Vendor is decreased, and the funds of the Customer are decreased.
        public static bool buy(Customer buyer, string[] menuList, Vendor market)
        { //randomly picks from 0 to 14, from 15 items.
            Random rnd = new Random();
            uint random = (uint)rnd.Next(14);
            return buyer.buyOne(market, menuList[random], 12345); 
        }

        //PRE: Vendor loaded with Entrees, instantiated buyer, and loaded menuList required for proper execution.
        //POST: If successfully bought, stock in the Vendor is decreased, and the funds of the Customer are decreased.
        public static void buyMeal(Customer buyer, string[] menuList, Vendor market)
        { //buys up to five items
            Random rnd = new Random();
            uint random = (uint)rnd.Next(5);
            uint successCount = 0;
            for (int i = 0; i < random; i++)
                if (buy(buyer, menuList, market))
                    successCount++;
            Console.WriteLine($"{successCount}, of the {random} purchases were successful.");
        }
    }
}
