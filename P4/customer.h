//Julian Harris
//Customer.h

#ifndef CUSTOMER_H
#define CUSTOMER_H

#include <iostream>
#include "vendor.h"

using namespace std; 

//Class invariants:
//-Customer needs to hold a balance, must have an accountNumber, and an identifier
//Reasons for avoided overloaded operators
//-Stream was not overloaded for Customer as there was nothing really to print.
//-Subtraction/Arithmetic was not overloaded because Customers have no need to add or subtract together.
//  I did not think adding balances together made sense, as that's pretty private.
//-Indexing operators were avoided because Customer only stores single simple types.
//-Logical operators were avoided because a Customer is not ever really true or false.
//-Comparison operators were avoided because I did not think comparing bank balances was necessary.

//Interface invarients:
//-When creating a Customer, the client is responsible for the following:
//  1. Passing in their own non negative int to use as their accountNumber.
    // Must also be used to properly access any of Customer's functions.

//Implementation invarients:
//-Client cannot make any calls without a valid accountNumber.


class Customer
{
    protected:
        float balance; //customer funds
        unsigned int accountNumber; //access key
        unsigned int identifier;

    public:
        Customer(){ balance = 0; accountNumber = 0; identifier = 10; }

        Customer(unsigned int newAccountNumber); 

        virtual unsigned int whoami() { return identifier; } //used to tell children apart

        virtual bool operator==(Customer& b){ return identifier == b.whoami(); } //returns if a is the same customer type as b
     
        virtual bool operator!=(Customer& b){ return identifier != b.whoami(); }

        //PRE: Requires a non negative number to be added to balance, and the
        //Customer's account number to log in.
        //POST: Customer has their balance increased
        virtual void addFunds(double deposit, unsigned int number);
        //PRE: Requires a non negative number less than or equal to the user's balance,
        //and the Customer's account number to log in.
        //POST: Subtracts user funds, returns whether or not transaction was successful
        virtual bool buyOne(Vendor& market, string foodName, unsigned int number, string currentDate);
        //PRE: Requires a non negative number less than or equal to the user's balance,
        //and the Customer's account number to log in.
        //POST: If purchase is successful, balance is decreased.
        virtual void buy(Vendor& market, string foodList[], unsigned int arrSize, unsigned int number, string currentDate);     
};

#endif