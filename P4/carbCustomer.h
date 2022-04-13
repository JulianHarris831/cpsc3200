//Julian Harris
//CarbCustomer.h

//CLASS INVARIANTS:
//-CarbCustomer class requires a carb limit to be given with the constructor along with the account number.
//-CarbCustomer class is in a valid state immediately after instantiation.

//INTERFACE INVARIANTS:
//-When creating a CarbCustomer, the client is responsible for the following:
//  1. Keeping track of their own account number to use to access the object.
//  2. Setting their own limit for carbohydrate intake.

//IMPLEMENTATION INVARIANTS:
//Current carbs should never go above the carb limit.
//Customer does not turn off for the day if carb limit is near reached, instead 
//will check with each food if buying is still okay.

#ifndef CARBCUSTOMER_H
#define CARBCUSTOMER_H

#include <iostream>
#include "customer.h"
#include "vendor.h"

class CarbCustomer : public Customer
{
    protected:
        unsigned int carbLimit;
        unsigned int currentCarbs;

    public:
        CarbCustomer(){ balance = 0; accountNumber = 0; identifier = 10; carbLimit = 0; currentCarbs = 0; } 
        CarbCustomer(unsigned int newNumber, unsigned int newLimit);
        //PRE: Requires a valid vendor, string foodname, and account number to function.
        //POST: Current carb amount may be increased if purchase was successful and contained them, 
        //account balance may be decreased
        bool buyOne(Vendor& market, string foodName, unsigned int number, string currentDate) override;
};

#endif