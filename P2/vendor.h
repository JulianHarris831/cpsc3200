//Julian Harris
//Vendor.cpp 

#ifndef VENDOR_H
#define VENDOR_H

#include <iostream>
#include <sstream> //needed for print statements
#include "entree.h" 
#include "customer.h"

using namespace std;

//Class invariants:
//-Vendor holds a name, refrigeration status, and a head pointer always.
//-Vendor object is in an ideal valid state when created with the parameterized constructor,
//  though the object should be valid without any input.
//-Vendor will only properly enter most of its functions once head is active.

//Interface invarients:
//-When creating a Vendor, the client is responsible for the following:
//  1. Keeping track of the current date
//  2. Using only the parameterized constructor or copy
//  3. Passing string formatted properly for Entree in load, and size = 16.

//Implementation invarients:
//-Client can safely call all functions, though until they load nothing will 
//  happen.
//-Client must call cleanStock to remove unneeded Entrees, isStocked will remind 
//  them automatically but ultimately they must use cleanStock.

class Vendor
{
private:

    class Node { 
    public:      //linked list to store Entrees and relevant data within vendor
        Entree* food; 
        float price;
        unsigned int stock;
        Node* next = nullptr;
    };

    string vendorName = "empty";
    bool refrigerated = false;

public:

    Node* head = nullptr; 

    Vendor(string newName, bool isFridge); //default constructor

    //PRE: Properly formatted string and correct size passed in.
    //POST: A new node is initialized within Vendor's linked list
    void load(string data, unsigned int size, float newPrice, unsigned int newStock);
    
    //PRE: None, though to return a non empty copy ideally a linked list is made
    //POST: Returns a deep copy of current vendor (with linked list, name)
    Vendor copy(); //returns a copy of itself
    
    //PRE: Node must be properly initialized and full.
    //POST: A new node is initialized within Vendor's linked list, deep copy.
    void load(Node* newFood); //works with copy to construct Entree with a node  
    
    //PRE: Customer class setup and valid, date correctly formatted. Name must be exactly 
    //  the same as formatted in print.
    //POST: Potentially stock of a node decremented by 1, if sell was successful.
    void sell(Customer buyer, unsigned int accountNumber, string foodName, string currentDate);
   
    //PRE: None
    //POST: refrigerated bools in every Entree in the list set to false
    void powerOutage();
   
    //PRE: Parameter date formatted correctly
    //POST: Removes spoiled or out of stock Entrees from list, relinks.
    void cleanStock(string currentDate);
    
    //PRE: Date formatted correctly, foodName must exactly match to be found.
    //POST: None
    bool isStocked(string foodName, string currentDate);
    
    //PRE: None, works with empty or initialized list fine.
    //POST: None
    string printMenu();
    
    //PRE: None, works with empty or initialized list fine.
    //POST: None
    string printAllInfo();
    
    //PRE: None
    //POST: None
    string getName(){ return vendorName; }
    
    //PRE: None
    //POST: vendorName is set to string passed in.
    void setName(string newName) { vendorName = newName; }

    ~Vendor(); //default destructor, need to deallocate linked list. 
    
    //void operator=(const Vendor &copy); //restricting assignment operator
    //Vendor(Vendor &newCopy); //copy constructor just needs to make a list copy
};

#endif