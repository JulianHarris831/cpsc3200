//Julian Harris
//P2.cpp

#include <iostream>
#include <vector>
#include <memory>
#include "entree.h"
#include "vendor.h" 
#include "customer.h"

using namespace std;

const string TODAY = "2021 10 22"; //today's date is assumed constant for convenience
const int SIZE = 16; //size of input strings for Entree constructors assumed correct

//functional decomposition

//PRE: Must have vector of Vendor shared pointers initialized
//POST: Vector is loaded with 3 vendors that have 5 entrees each, ready for testing
void loadingVendors(vector <shared_ptr<Vendor>>& FoodCourt); //adds data to vector of Vendors

//PRE: Vendor must be stocked with Entrees, as Entree function is called.
//POST: None
void printMenu(shared_ptr<Vendor> EntreeList); //prints a vendors Entrees.

//PRE: Vendor must be stocked with Entrees
//POST: None
void printMenuInfo(shared_ptr<Vendor> EntreeList); //prints a vendors Entree nutrition facts.

//PRE: Vendor must be initialized, a Customer needs to be initialized,
//  target must match exactly the food in question.
//POST: If successful, Customer balance goes down, and Vendor stock is decremented by 1.
void sellItem(shared_ptr<Vendor> EntreeList, Customer buyer, unsigned int accountNumber, string target); //decrements stock if available to sell

//PRE: loadingVendors has been called, vector is loaded with Vendors with Entrees.
//POST: Entrees within Vendors refrigeration is set to false, perishables will be removed with clean
void outage(vector <shared_ptr<Vendor>>& FoodCourt);  //works for whole foodCourt list!

//PRE: loadingVendors has been called, vector is loaded with Vendors with Entrees.
//POST: Spoiled or out of stock Entrees are deleted from Vendors.
void cleanStock(vector <shared_ptr<Vendor>>& FoodCourt);

//PRE: Vendor must have Entrees initialized within it, loadingVendors must be called.
//  target string must exactly match the name of item in question.
//POST: None
void isStocked(shared_ptr<Vendor> EntreeList, string target);

int main()
{
    vector <shared_ptr<Vendor>> FoodCourt; //Vendors stored in a shared_ptr list.
    Customer Jimmy(12345);  //Test customer given $10
    Jimmy.addFunds(10, 12345); 

    loadingVendors(FoodCourt); //Adds three vendors and fills them with 5 Entrees each
    shared_ptr<Vendor> BackupFreezer = make_shared<Vendor>(FoodCourt.at(2)->copy());
    BackupFreezer->setName("Meal Backup Freezer"); //deep copy test for after outage

    printMenu(FoodCourt.at(0)); //Printing menu of drinks
    printMenuInfo(FoodCourt.at(0)); //Printing nutrient info of drinks

    isStocked(FoodCourt.at(0), "Orange Soda"); //testing 0 stock food in stock
    isStocked(FoodCourt.at(0), "Whole Milk"); //testing spoiled food in stock
    isStocked(FoodCourt.at(2), "Fishburger"); //testing if valid item is in stock

    sellItem(FoodCourt.at(1), Jimmy, 12345, "Carrot Sticks"); //buying a valid item
    sellItem(FoodCourt.at(0), Jimmy, 12345, "Whole Milk"); //attempting to buy a spoiled item
    sellItem(FoodCourt.at(2), Jimmy, 12345, "Salad"); //attempting to buy an item that the customer can't afford

    cleanStock(FoodCourt);
    outage(FoodCourt);    //cleaning stock before and after power outage,
    cleanStock(FoodCourt);//testing if food spoils with power off properly.

    //backup print, testing validity of deep copy.
    printMenu(BackupFreezer);

    return 0;
}


//functional decomposition bodies

void printMenu(shared_ptr<Vendor> EntreeList)
{
    cout << EntreeList->printMenu();
}

void printMenuInfo(shared_ptr<Vendor> EntreeList)
{
    cout << EntreeList->printAllInfo();
}

void cleanStock(vector <shared_ptr<Vendor>>& FoodCourt)
{
    for(int i = 0; i < 3; i++){ 
        cout << "Cleaning " << FoodCourt.at(i)->getName() << " now...\n";
        FoodCourt.at(i)->cleanStock(TODAY);
    }
    cout << '\n';
}

void isStocked(shared_ptr<Vendor> EntreeList, string target)
{
    if(EntreeList->isStocked(target, TODAY))
        cout << target << " is stocked in " << EntreeList->getName() << "!\n";
    else
        cout << target << " is not stocked in " <<EntreeList->getName() << ".\n";
}

void outage(vector <shared_ptr<Vendor>>& FoodCourt)
{
    for(int i = 0; i < 3; i++)
        FoodCourt.at(i)->powerOutage();
    cout << "Power outage! Refrigerated food has spoiled.\n\n";
}

void sellItem(shared_ptr<Vendor> EntreeList, Customer buyer, unsigned int accountNumber, string target)
{
    EntreeList->sell(buyer, accountNumber, target, TODAY);
}

void loadingVendors(vector <shared_ptr<Vendor>>& FoodCourt)
{
    //string input for Entree constructor is assumed to be correct, input for vendor "Drinks"
    string milkData = "Whole Milk	1	150	8	5	0	35	130	13	0	12	8	Grade A Organic Milk$Vitamin D3	milk	2021 10 15	yes";
    string darkCola = "Dark Cola	1	140	0	0	0	0	45	39	0	39	0	carbonated water, high fructose corn syrup, caramel color, natural flavors, caffeine		2022 02 12	no";
    string rootBeer = "Root Beer	1	170	0	0	0	0	65	47	0	45	0	carbonated water, high fructose corn syrup, caramel color, natural and artifical flavors		2022 03 21	no";
    string orangeSoda = "Orange Soda	1	170	0	0	0	0	70	44	0	43	0	carbonated water, high fructose corn syrup, citric acid, natural flavors, caffeine		2022 01 10	no";
    string appleJuice = "Apple Juice	3	150	0	0	0	0	75	36	1	26	1	apples		2021 11 10	yes";
    //input for vendor "Snacks", correct formatting expected of user
    string cheeseChip = "Cheesy Tortilla Chips	1	150	8	1	0	0	210	18	1	1	2	cheese, corn, salt	dairy	2022 10 11	no";
    string jalapChip = "Jalapeno Chips	2	300	18	2	0	0	360	32	3	2	5	potato, jalapeno, salt		2022 05 12	no";
    string cheesePoof = "Cheese Puffs	6	150	10	1.5	0	6	200	13	0	1	2	corn, cheese, whey, salt	dairy	2019 04 10	no";
    string carrotSticks = "Carrot Sticks	1	35	0	0	0	0	60	8	2	4	1	carrots		2021 10 25	yes";
    string appleSlices = "Apple Slices	1	130	0	0	0	0	0	34	5	25	1	apples		2021 10 24	yes";
    //input for vendor "Meals", correct formatting expected of user
    string burger = "Hamburger 	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2021 10 23	yes";
    string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2021 10 23	yes";
    string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2021 10 23	yes";
    string nuggets = "Chicken Nuggets	1	470	30	5	0	65	900	30	2	0	22	chicken, breading		2021 10 26	yes";
    string salad = "Grilled Chicken Salad	1	320	9	5	0	70	960	30	6	11	30	chicken, lettuce, ranch, cheese	dairy	2021 10 28	yes";

    //all vendors are refrigerated
    shared_ptr<Vendor> drinks(new Vendor("Drinks", true));
    shared_ptr<Vendor> snacks(new Vendor("Snacks", true));
    shared_ptr<Vendor> meals(new Vendor("Meals", true));
    FoodCourt.push_back(drinks);
    FoodCourt.push_back(snacks);
    FoodCourt.push_back(meals);

    //loading vendor "Drinks" with Entrees
    FoodCourt.at(0)->load(milkData, SIZE, 5, 6);  //milk is intentionally expired
    FoodCourt.at(0)->load(darkCola, SIZE, 1.50, 1);
    FoodCourt.at(0)->load(rootBeer, SIZE, 1.50, 3);
    FoodCourt.at(0)->load(orangeSoda, SIZE, 1.50, 0); //orange soda intentionally out of stock
    FoodCourt.at(0)->load(appleJuice, SIZE, 2.50, 10);

    //loading vendor "Snacks" with Entrees
    FoodCourt.at(1)->load(cheeseChip, SIZE, 1.50, 3);
    FoodCourt.at(1)->load(jalapChip, SIZE, 1.50, 0); //intentionally out of stock
    FoodCourt.at(1)->load(cheesePoof, SIZE, 1, 8);
    FoodCourt.at(1)->load(carrotSticks, SIZE, 3, 10);
    FoodCourt.at(1)->load(appleSlices, SIZE, 3, 5);

    //loading vendor "Meals" with Entrees
    FoodCourt.at(2)->load(burger, SIZE, 1, 5);
    FoodCourt.at(2)->load(cheeseBurger, SIZE, 1.50, 2);
    FoodCourt.at(2)->load(fishBurger, SIZE, 2.50, 3);
    FoodCourt.at(2)->load(nuggets, SIZE, 5, 6);
    FoodCourt.at(2)->load(salad, SIZE, 11, 8);
}