//allergyCustomer.cpp
//Julian Harris

#include "allergyCustomer.h"

AllergyCustomer::AllergyCustomer(unsigned int newNumber, string newList[], unsigned int arrSize)
{
    allergenCount = arrSize;
    allergenList = new string[arrSize];
    accountNumber = newNumber; 
    balance = 0;
    identifier = 3;
    for(int i = 0; i < arrSize; i++) //building list of allergens with passed in data
        allergenList[i] = newList[i];
}

bool AllergyCustomer::buyOne(Vendor& market, string foodName, unsigned int number, string currentDate)
{
    bool allergic = false; //assumed fine to start
    for(int i = 0; i < allergenCount; i++)
        if(market.checkContains(foodName, allergenList[i])) //if we ever find a match, allergic is true
            allergic = true;
    if(allergic == true){
        cout << "Customer is allergic to food item and cannot purchase.\n";
        return false;
    }
    if(Customer::buyOne(market, foodName, number, currentDate)) 
        return true; //if purchase is successful, return true. otherwise return false.
    return false;
}