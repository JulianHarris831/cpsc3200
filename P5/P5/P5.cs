//Julian Harris
//P5.cs

using System;

namespace P5
{
    public class P5
    {
        const string filename = "P5Data.txt";
        const string marketName = "Cane's";
        const uint arrSize = 5;
        const uint accountNum = 12345;

        static void Main(string[] args)
        {
            Vendor market = new Vendor(marketName, true);
            string[] menuList = new string[15];
            ICustomer[] buyerList = new ICustomer[arrSize]; //Heterogeneous collection of employees and employeeCustomer.
            IEmployee[] workerList = new IEmployee[arrSize]; //Heterogeneous collection of customers and employeecustomer

            marketLoad(market);
            loadMenuList(ref menuList);
            buyersLoad(buyerList);
            workersLoad(workerList);

            Console.WriteLine("Welcome to P5!");
            Console.WriteLine(market.printMenu()); //printing out our menu, seeing random stocks
            Console.WriteLine(market.printMenuInfo());

            //test collection of buyers, using a for loop
            //Customer testing. 1 is default, 2 is diabetic, 3 is carb, 4 is allergy, 5 is employee.
            for (int i = 0; i < arrSize; i++)
            { //5th is an employee customer, treated the same.
                Console.WriteLine("\nTesting customer buyOne.");
                buy(buyerList[0], menuList, market); //buying one and then multiple foods with default customer
                Console.WriteLine("\nTesting customer buy.");
                buyMeal(buyerList[0], menuList, market);
            }

            //testing collection of workers
            employeeTest(workerList);
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
                //random num gen for stock (0-4), price (1 - 10)
                price = (float)rnd.Next(9)+1; //+1 to make 1-10 vs 0-9
                stock = (uint)rnd.Next(4); //we want this to randomly be out of stock
                market.load(line, price, stock);
            }
        }

        //PRE: Array of Customers must be declared and passed in.
        //POST: Array of Customers is loaded with 4 different Customer classes.
        public static void buyersLoad(ICustomer[] buyerList)
        { //load up list of buyers here, returned to main through pass by reference
            buyerList[0] = new Customer(accountNum);
            buyerList[1] = new DbetCustomer(accountNum, 25); //can have 25g of sugar.
            buyerList[2] = new CarbCustomer(accountNum, 30); //can have 30 carbs.
            string[] allergyList = new string[3];
            allergyList[0] = "peanuts";
            allergyList[1] = "fish";
            allergyList[2] = "dairy";
            buyerList[3] = new AllergyCustomer(accountNum, allergyList);
            Customer EmployeeAccount = new Customer(accountNum); //can be treated just like other customers, heterogeneous
            buyerList[4] = new EmployeeCustomer(1, accountNum, "ECustomer", marketName, EmployeeAccount);

            //giving each buyer $20 the same way 
            for (int i = 0; i < 5; i++)
                buyerList[i].addFunds(20, 12345);
        }

        public static void workersLoad(IEmployee[] workerList)
        { //loading up list of five Employees here.
            Random rnd = new Random();
            //int random = rnd.Next(2) + 1; //random 1-3
            workerList[0] = new Employee((uint)rnd.Next(2) + 1, accountNum, "Emp1");
            Customer Emp2Base = new Customer(accountNum);
            workerList[1] = new EmployeeCustomer((uint)rnd.Next(2) + 1, accountNum, "Emp2", marketName, Emp2Base);
            workerList[2] = new Employee((uint)rnd.Next(2) + 1, accountNum, "Emp3");
            Customer Emp4Base = new Customer(accountNum);
            workerList[3] = new EmployeeCustomer((uint)rnd.Next(2) + 1, accountNum, "Emp4", marketName, Emp4Base);
            workerList[4] = new Employee((uint)rnd.Next(2) + 1, accountNum, "Emp5");
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
        public static bool buy(ICustomer buyer, string[] menuList, Vendor market)
        { //randomly picks from 0 to 14, from 15 items.
            Random rnd = new Random();
            uint random = (uint)rnd.Next(14);
            return buyer.buyOne(market, menuList[random], 12345);
        }

        //PRE: Vendor loaded with Entrees, instantiated buyer, and loaded menuList required for proper execution.
        //POST: If successfully bought, stock in the Vendor is decreased, and the funds of the Customer are decreased.
        public static void buyMeal(ICustomer buyer, string[] menuList, Vendor market)
        { //buys up to five items
            Random rnd = new Random();
            uint random = (uint)rnd.Next(5);
            uint successCount = 0;
            for (int i = 0; i < random; i++)
                if (buy(buyer, menuList, market))
                    successCount++;
            Console.WriteLine($"{successCount}, of the {random} purchases were successful.");
        }
    
        public static void employeeTest(IEmployee[] workerList)
        {
            //do payday and print for each employee, treat them all the same in a for loop.
            Console.WriteLine("Printing Employees...\n");
            for(int i = 0; i < arrSize; i ++)
            {
                workerList[i].payday(); //add their money
                workerList[i].printInfo(); //print their name
            }
        }
    }
}
