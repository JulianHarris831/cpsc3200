//dbetCustomer.cpp 
//Julian Harris

#include "dbetCustomer.h"

DbetCustomer::DbetCustomer(unsigned int newNumber, unsigned int newLimit)
{
    accountNumber = newNumber; 
    sugarLimit = newLimit;
    currentSugar = 0;
    balance = 0;
    identifier = 1;
}

bool DbetCustomer::buyOne(Vendor& market, string foodName, unsigned int number, string currentDate)
{
    unsigned int sugar = market.getSugar(foodName);
    if(sugar < 0){
        cout << "That item was not found.\n";
        return false;
    }
    if((currentSugar + sugar) > sugarLimit){
        cout << "Purchasing this item goes over the daily sugar limit.\n";
        return false;
    }
    if(Customer::buyOne(market, foodName, number, currentDate)){ //if purchase is successful
        currentSugar += sugar;
        return true;
    }
    return false;
}