//Julian Harris
//Customer.cpp

#include "customer.h"

using namespace std;

Customer::Customer(unsigned int newAccountNumber) //user creates their account number
{
    accountNumber = newAccountNumber;
    balance = 0;
}

void Customer::addFunds(unsigned int deposit, unsigned int number)
{
    if(number != accountNumber)
        cout << "Invalid account number.\n";
    else
        balance += deposit;
}

bool Customer::purchaseItem(unsigned int withdrawl, unsigned int number)
{
    if(number != accountNumber){
        cout << "Invalid account number.\n";
        return false;
    }
    else if(withdrawl > balance){
            cout << "Balance too low.";
            return false;
        }
            else{
                balance -= withdrawl;
                return true;
            }
}
