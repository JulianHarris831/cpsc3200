//Julian Harris
//Vendor.cpp 

#ifndef VENDOR_H
#define VENDOR_H

#include <iostream>
#include <sstream> //needed for print statements
#include "entree.h" 

using namespace std;

//Class invariants:
//-Vendor holds a name, refrigeration status, and a head pointer always.
//-Vendor object is in an ideal valid state when created with the parameterized constructor,
//  though the object should be valid without any input.
//-Vendor will only properly enter most of its functions once head is active.
//-If they wish to see tracked nutrient data, they must call the printDataCount function.
//-Adding vendors together simply combines two vendors and must be used without an assignment operator like so: a + b
//Reasons for avoided overloaded operators
//-Stream was not overloaded for Vendor because there are three different prints, one for
//  the menu with price and stock, one for menu with nutrient info, and one for total nutrients sold. 
//-Subtraction/Arithmetic was avoided because though it makes sense to add two stocks together,
//  it doesn't really make sense to subtract/other a stock since there are no negatives, items loosely connected.
//-Indexing operators were avoided because the stored data exists in a linked list that puts no significance on order.
//-Logical operators were avoided because a vendor is not ever really true or false.

//Interface invarients:
//-When creating a Vendor, the client is responsible for the following:
//  1. Keeping track of the current date
//  2. Using only the parameterized constructor, assignment operator or copy
//  3. Passing string formatted properly for Entree in load, and size = 16.

//Implementation invarients:
//-Client can safely call all functions, though until they load nothing will 
//  happen.
//-Client must call cleanStock to remove unneeded Entrees, isStocked will remind 
//  them automatically but ultimately they must use cleanStock.

class Vendor
{
private:
    string vendorName;
    bool refrigerated;

    struct EntreeData //used to store information about sold items
    {
        public:
            float servings; 
            unsigned int calories;
            float totalFat;
            float satFat;
            float transFat;
            unsigned int cholest;
            unsigned int sodium;
            unsigned int carbs;
            unsigned int fiber;
            unsigned int sugars; 
            unsigned int protein;
    };
    EntreeData DataCount { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    class Node { 
    public:      //linked list to store Entrees and relevant data within vendor
        Entree* food; 
        double price;
        unsigned int stock;
        Node* next = nullptr;
    };

public: 
    Node* head = nullptr; //set public for copying

    Vendor(string newName, bool isFridge); //parameterized constructor

    Vendor(); //default constructor, needed for overloaded assignment operator

    //OPERATOR OVERLOADING
    void operator=(Vendor& original); //overloading assignment

    void operator+(const Vendor& b); //a + b

    bool operator<(const Vendor& b); //returns if a is less than b

    bool operator>(const Vendor& b); //returns if a is greater than b
    
    bool operator==(const Vendor& b); //returns if a is equal to b

    bool operator!=(const Vendor& b); //returns if a is not equal to b

    //PRE: Properly formatted string and correct size passed in.
    //POST: A new node is initialized within Vendor's linked list
    void load(string data, float newPrice, unsigned int newStock);
    
    //PRE: None, though to return a non empty copy ideally a linked list is made
    //POST: Returns a deep copy of current vendor (with linked list, name)
    Vendor copy(); //returns a copy of itself
    
    //PRE: Node must be properly initialized and full.
    //POST: A new node is initialized within Vendor's linked list, deep copy.
    void load(Node* newFood); //works with copy to construct Entree with a node  
    
    //PRE: Customer class setup and valid, date correctly formatted. Name must be exactly 
    //  the same as formatted in print.
    //POST: Potentially stock of a node decremented by 1, if sell was successful.
    bool sell(string foodName, string currentDate);

    //PRE: Valid entree must be passed in.
    //POST: Data count is updated with the nutrient info in soldItem.
    void loadData(Entree* soldItem);

    //PRE: None, returns -1 if failed
    //POST: None
    double getPrice(string foodName);

    //PRE: None, returns -1 if failed
    //POST: None
    int getSugar(string foodName);

    //PRE: None, returns -1 if failed
    //POST: None
    int getCarbs(string foodName);

    //PRE: None
    //POST: None
    bool checkContains(string foodName, string allergy);

    //PRE: None
    //POST: refrigerated bools in every Entree in the list set to false
    void powerOutage();
   
    //PRE: Parameter date formatted correctly
    //POST: Removes spoiled or out of stock Entrees from list, relinks.
    void cleanStock(string currentDate);
    
    //PRE: Date formatted correctly, foodName must exactly match to be found.
    //POST: None
    bool isStocked(string foodName, string currentDate);
    
    //PRE: None, prints 0s if called immediately.
    //POST: None
    string printDataCount(); //shows customer all data sold in vendor

    //PRE: None, works with empty or initialized list fine.
    //POST: None
    string printMenu();
    
    //PRE: None, works with empty or initialized list fine.
    //POST: None
    string printAllInfo();
    
    //PRE: None
    //POST: None
    string getName(){ return vendorName; }

    bool getRefrigerated(){ return refrigerated; }
    
    //PRE: None
    //POST: vendorName is set to string passed in.
    void setName(string newName) { vendorName = newName; }

    ~Vendor(); //default destructor, need to deallocate linked list. 
    
    //Vendor(Vendor &newCopy); //copy constructor just needs to make a list copy
};

#endif