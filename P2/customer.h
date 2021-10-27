//Julian Harris
//Customer.h

#ifndef CUSTOMER_H
#define CUSTOMER_H

#include <iostream>

using namespace std; 

//Class invariants:
//-Customer needs to hold a balance, must have an accountNumber.

//Interface invarients:
//-When creating a Customer, the client is responsible for the following:
//  1. Passing in their own non negative int to use as their accountNumber.
    // Must also be used to properly access any of Customer's functions.

//Implementation invarients:
//-Client cannot make any calls without a valid accountNumber.

class Customer
{
    private:
        float balance; //customer funds
        unsigned int accountNumber; //access key

    public:
        //PRE: Requires a non negative number to be added to balance, and the
        //Customer's account number to log in.
        //POST: Customer has their balance increased
        void addFunds(unsigned int deposit, unsigned int number);
        //PRE: Requires a non negative number less than or equal to the user's balance,
        //and the Customer's account number to log in.
        //POST: Subtracts user funds, returns whether or not transaction was successful
        bool purchaseItem(unsigned int withdrawl, unsigned int number);

        Customer(unsigned int newAccountNumber); 
};

#endif