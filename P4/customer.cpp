//Julian Harris
//Customer.cpp

#include "customer.h"

using namespace std;

Customer::Customer(unsigned int newAccountNumber) //user creates their account number
{
    accountNumber = newAccountNumber;
    balance = 0;
    identifier = 0;
}

void Customer::addFunds(double deposit, unsigned int number)
{
    if(number != accountNumber)
        cout << "Invalid account number.\n";
    else
        balance += deposit;
}

bool Customer::buyOne(Vendor& market, string foodName, unsigned int number, string currentDate)
{
    double price = market.getPrice(foodName); //we need a get price function in vendor
    if(number != accountNumber)
    { 
        cout << "Invalid account number.\n";
        return false;
    }
    if(price > balance)
    {
        cout << "The item " << foodName << " cannot be afforded.\n";
        return false;
    }
    if(market.sell(foodName, currentDate)){ //this will output a successful purchase
        balance -= price;
        return true;
    }
    return false;
}

void Customer::buy(Vendor& market, string foodList[], unsigned int arrSize, unsigned int number, string currentDate)
{
    unsigned int counter = 0;
    for(int i = 0; i < arrSize; i++){
        if(buyOne(market, foodList[i], accountNumber, currentDate))
            counter++;
    }
    cout << counter << " of the " << arrSize << " purchases were successful.\n";
}