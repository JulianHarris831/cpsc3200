//carbCustomer.cpp
//Julian Harris

#include "carbCustomer.h"

CarbCustomer::CarbCustomer(unsigned int newNumber, unsigned int newLimit)
{
    accountNumber = newNumber; 
    carbLimit = newLimit;
    currentCarbs = 0;
    balance = 0;
    identifier = 2;
}

bool CarbCustomer::buyOne(Vendor& market, string foodName, unsigned int number, string currentDate)
{
    unsigned int carbs = market.getCarbs(foodName);
    if(carbs < 0){
        cout << "That item was not found.\n";
        return false;
    }
    if((currentCarbs + carbs) > carbLimit){
        cout << "Purchasing this item goes over the daily carbohydrate limit.\n";
        return false;
    }
    if(Customer::buyOne(market, foodName, number, currentDate)){ //if purchase is successful
        currentCarbs += carbs;
        return true;
    }
    return false;
}