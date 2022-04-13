//Julian Harris
//dbetCustomer.h

//CLASS INVARIANTS:
//-DbetCustomer holds a balance, an accountNumber, a sugar limit and a sugar intake.
//-DbetCustomer is in a valid state when given a proper account number and limit in constructor.

//INTERFACE INVARIANTS:
//-When creating a DbetCustomer, the client is responsible for the following:
//  1. Keeping track of their own account number to use to access the object.
//  2. Setting their own limit for sugar intake, as different diets mean different amounts.

//IMPLEMENTATION INVARIANTS:
//Current sugar will never go above sugar limit.
//Customer does not turn off for the day if sugar limit is near reached, instead 
//will check with each food if buying is still okay.

#ifndef DBETCUSTOMER_H
#define DBETCUSTOMER_H

#include <iostream>
#include "customer.h"
#include "vendor.h"

class DbetCustomer : public Customer
{
    protected:
        unsigned int sugarLimit;
        unsigned int currentSugar;

    public:
        DbetCustomer(){ balance = 0; accountNumber = 0; identifier = 10; sugarLimit = 0; currentSugar = 0; } 
        DbetCustomer(unsigned int newNumber, unsigned int newLimit);
        //PRE: Valid vendor, a string of desired food, and the customers account number must be passed in.
        //POST: Current sugar amount is increased if purchase is successful, and balance is decreased.
        bool buyOne(Vendor& market, string foodName, unsigned int number, string currentDate) override;
};

#endif