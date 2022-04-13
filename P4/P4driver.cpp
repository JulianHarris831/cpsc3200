//Julian Harris
//P4driver.cpp 

#include <iostream>
#include <vector>
#include <time.h>
#include <random>
#include "entree.h"
#include "vendor.h"
#include "customer.h"
#include "dbetCustomer.h"
#include "carbCustomer.h"
#include "allergyCustomer.h"

using namespace std;

//functional decomposition

const int VSIZE = 3; //how many vendors in array
const int CSIZE = 6; //how many customers in array

void loadVendors(Vendor marketList[]);
void loadCustomers(vector<Customer*> &buyerList); //heterogeneous collection
void printMenu(Vendor& market);
void printMenuInfo(Vendor& market);
void printSellData(Vendor& market);
void buyOne(Vendor& market, Customer& buyer, string foodName, unsigned int number, string currentDate);
void buyMeal(Vendor& market, Customer& buyer, unsigned int number, string currentDate);
void outage(Vendor marketList[]);
void cleanStock(Vendor marketList[], string currentDate);
void testVendorComparison(Vendor marketList[]);
void testCustomerComparison(vector<Customer*> &buyerList);
int random(int max);
string randomFoodOne();
string randomFoodTwo();
string randomFoodThree();

int main()
{ 
    cout << "P4 has started\n\n";
    srand(time(0));
    string currentDate = "2021 11 15";
    Vendor marketList[VSIZE];
    loadVendors(marketList);
    vector<Customer*> buyerList;
    loadCustomers(buyerList);

    for(int i = 0; i < VSIZE; i++) //printing the whole menu of loaded vendors
        cout << marketList[i].printMenu();
    for(int i = 0; i < VSIZE; i++) //printing the whole menu info of loaded vendors
        cout << marketList[i].printAllInfo();
    //testing purchases for all
    cout << "\nBuying for normal customer...\n\n";
    buyOne(marketList[0], *buyerList[0], randomFoodOne(), 12345, currentDate); //buying an item with default customer from drinks
    buyMeal(marketList[0], *buyerList[0], 12345, currentDate);
    cout << "\nBuying for diabetic customer...\n\n";
    buyOne(marketList[1], *buyerList[1], randomFoodTwo(), 12345, currentDate); 
    buyMeal(marketList[1], *buyerList[1], 12345, currentDate);
    cout << "\nBuying for carbohydrate customer...\n\n";
    buyOne(marketList[2], *buyerList[2], randomFoodThree(), 12345, currentDate); 
    buyMeal(marketList[2], *buyerList[2], 12345, currentDate);
    cout << "\nBuying for allergic customer...\n\n";
    buyOne(marketList[0], *buyerList[3], randomFoodOne(), 12345, currentDate); 
    buyMeal(marketList[0], *buyerList[0], 12345, currentDate);

    cout << "Printing the sell data of all Vendors";
    for(int i = 0; i < VSIZE; i++)
        printSellData(marketList[i]);

    testVendorComparison(marketList);
    testCustomerComparison(buyerList);
    cout << "\nP4 has finished\n\n";
    return 0;
}

//functional decomposition

void loadVendors(Vendor marketList[])
{   //VSIZE = 3
    //string input for Entree constructor is assumed to be correct, input for vendor "Drinks"
    //milkData is intentionally expired, orange soda is out of stock, and the rest are fine
    string milkData = "Whole Milk	1	150	8	5	0	35	130	13	0	12	8	Grade A Organic Milk	milk	2015 10 15	yes";
    string darkCola = "Dark Cola	1	140	0	0	0	0	45	39	0	39	0	carbonated water, high fructose corn syrup, caramel color, natural flavors, caffeine		2025 02 12	no";
    string rootBeer = "Root Beer	1	170	0	0	0	0	65	47	0	45	0	carbonated water, high fructose corn syrup, caramel color, natural and artifical flavors		2025 03 21	no";
    string orangeSoda = "Orange Soda	1	170	0	0	0	0	70	44	0	43	0	carbonated water, high fructose corn syrup, citric acid, natural flavors, caffeine		2025 01 10	no";
    string appleJuice = "Apple Juice	3	150	0	0	0	0	75	36	1	26	1	apples		2024 11 10	yes";
    Vendor drinks("Drinks", true); //stock and price 1-5 (random)
    drinks.load(milkData, random(5), random(5)); //intentionally expired
    drinks.load(darkCola, random(5), random(5));
    drinks.load(rootBeer, random(5), random(5));
    drinks.load(orangeSoda, random(5), 0); //intentionally out of stock
    drinks.load(appleJuice, random(5), random(5));
    marketList[0] = drinks;
    string cheeseChip = "Cheesy Tortilla Chips	1	150	8	1	0	0	210	18	1	1	2	cheese, corn, salt	dairy	2025 10 11	no";
    string jalapChip = "Jalapeno Chips	2	300	18	2	0	0	360	32	3	2	5	potato, jalapeno, salt		2025 05 12	no";
    string cheesePoof = "Cheese Puffs	6	150	10	1.5	0	6	200	13	0	1	2	corn, cheese, whey, salt	dairy	2019 04 10	no";
    string carrotSticks = "Carrot Sticks	1	35	0	0	0	0	60	8	2	4	1	carrots		2025 10 25	yes";
    string appleSlices = "Apple Slices	1	130	0	0	0	0	0	34	5	25	1	apples		2025 10 24	yes";
    Vendor snacks("Snacks", true);  
    snacks.load(cheeseChip, random(5), random(5)); 
    snacks.load(jalapChip, random(5), random(5));
    snacks.load(cheesePoof, random(5), random(5)); //intentionally expired
    snacks.load(carrotSticks, 500, random(5)); //intentionally overpriced
    snacks.load(appleSlices, random(5), random(5));
    marketList[1] = snacks;
    string burger = "Hamburger	1	250	9	3.5	0.5	25	520	31	2	6	12	beef, flour		2025 10 23	yes";
    string cheeseBurger = "Cheeseburger	1	300	12	6	0.5	40	750	33	2	6	15	beef, flour, cheese	dairy	2025 10 23	yes";
    string fishBurger = "Fishburger	1	380	18	3.5	0	40	640	38	2	5	15	fish, flour	fish	2025 10 23	yes";
    string nuggets = "Chicken Nuggets	1	470	30	5	0	65	900	30	2	0	22	chicken, breading		2019 10 26	yes";
    string salad = "Grilled Chicken Salad	1	320	9	5	0	70	960	30	6	11	30	chicken, lettuce, ranch, cheese	dairy	2025 10 28	yes";
    Vendor meals("Meals", true);
    meals.load(burger, random(5), random(5));
    meals.load(cheeseBurger, 500, random(5)); //intentionally overpriced
    meals.load(fishBurger, random(5), random(5));
    meals.load(nuggets, random(5), random(5)); //intentionally expired
    meals.load(salad, random(5), random(5));
    marketList[2] = meals;
    
    //marketList[0] = 
}
void loadCustomers(vector<Customer*> &buyerList) //heterogeneous collection
{ //CSIZE is 6
    Customer *Jimmy = new Customer(12345);
    Customer *JimmyBro = new Customer(12345);
    Customer *Sweetie = new DbetCustomer(12345, 25); //dbet customer can have 25 sugars
    Customer *Carbo = new CarbCustomer(12345, 55); //carb customer can have 55 carbs
    Customer *CarboBro = new CarbCustomer(12345, 55);
    string allergenList[3] = {"dairy", "eggs", "nuts"};
    Customer *Allan = new AllergyCustomer(12345, allergenList, 3);

    buyerList.push_back(Jimmy);
    buyerList.push_back(Sweetie);
    buyerList.push_back(Carbo);
    buyerList.push_back(Allan);
    buyerList.push_back(JimmyBro); //duplicate normal customer
    buyerList.push_back(CarboBro); //duplicate carb customer
    //do the rest

    for(int i = 0; i < CSIZE; i++) //FIX THIS
        buyerList[i]->addFunds(random(20), 12345); //gives customers between 1 and 20 dollars
}
void printMenu(Vendor& market)
{
    cout << market.printMenu();
}
void printMenuInfo(Vendor& market)
{
    cout << market.printAllInfo();
}
void printSellData(Vendor& market)
{
    cout << market.printDataCount();
}
void buyOne(Vendor& market, Customer& buyer, string foodName, unsigned int number, string currentDate)
{ //MAYBE just check and cout something if it fails, though it kinda does that already
    buyer.buyOne(market, foodName, number, currentDate);
}
void buyMeal(Vendor& market, Customer& buyer, unsigned int number, string currentDate)
{
    int size = random(5);
    string foodList[size];
    if(market.getName() == "Drinks")
        for(int i = 0; i < size; i++)
            foodList[i] = randomFoodOne();
    if(market.getName() == "Snacks")
        for(int i = 0; i < size; i++)
            foodList[i] = randomFoodTwo();
    if(market.getName() == "Meals")
        for(int i = 0; i < size; i++)
            foodList[i] = randomFoodThree();
    buyer.buy(market, foodList, size, number, currentDate);
}
void outage(Vendor marketList[])
{
    for(int i = 0; i < VSIZE; i++)
        marketList[i].powerOutage();
}
void cleanStock(Vendor marketList[], string currentDate)
{
    for(int i = 0; i < VSIZE; i++)
        marketList[i].cleanStock(currentDate);
}
void testVendorComparison(Vendor marketList[])
{
    //Vendors can be greater, less, or equal to. also tests add
    cout << "Testing Vendor operators...\n";
    if(marketList[0] != marketList[1]) //should always be equal after load
        cout << "There is an error with != operator\n";
    marketList[0] + marketList[1]; //adds data in 1 to 0.
    if(marketList[0] == marketList[1]) 
        cout << "There is an error with == operator\n";
    if(marketList[0] < marketList[1])
        cout << "There is an error with < operator\n";
    if(marketList[1] > marketList[0])
        cout << "There is an issue with > operator\n";
    cout << "Drinks + Snacks = \n" << marketList[0].printMenu();
}
void testCustomerComparison(vector<Customer*> &buyerList)
{ //CSIZE = 6
    cout << "Testing Customer operators...\n";
    if(*buyerList[0] == *buyerList[1]) //if a regular customer is identified as a diabetic customer
        cout << "There is an issue with the == operator.\n";
    if(*buyerList[0] != *buyerList[4]) //if Jimmy != JimmyBro (same type)
        cout << "There is an issue with the != operator.\n";
}
int random(int max) //returns number 1-input
{
    return (1 + rand() % max);
}
string randomFoodOne() //returns a food name
{
    string foods[5] = { "Whole Milk", "Dark Cola", "Root Beer", "Orange Soda", "Apple Juice" };
    
    return foods[(0 + rand() % 4)]; //max of 4
}
string randomFoodTwo()
{
    string foods[5] = { "Cheesy Tortilla Chips", "Jalapeno Chips", "Cheese Puffs", "Carrot Sticks", "Apple Slices" };
    return foods[(0 + rand() % 4)]; //max of 4
}
string randomFoodThree()
{
    string foods[5] = { "Hamburger", "Cheeseburger", "Fishburger", "Chicken Nuggets", "Grilled Chicken Salad" };
    return foods[(0 + rand() % 4)]; //max of 4
}