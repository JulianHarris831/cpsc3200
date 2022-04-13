//Julian Harris
//AllergyCustomer.h

//CLASS INVARIANTS:
//-AllergyCustomer class requires a list of items they are allergic to to be passed in along with the account number.
//-AllergyCustomer class is in a valid state immediately after instantiation.

//INTERFACE INVARIANTS:
//-When creating a AllergyCustomer, the client is responsible for the following:
//  1. Keeping track of their own account number to use to access the object.
//  2. Setting up their own allergens to be checked with their purchased food.

//IMPLEMENTATION INVARIANTS:
//Client has access to all functionality upon implementation.

#ifndef ALLERGCUSTOMER_H
#define ALLERGCUSTOMER_H

#include <iostream>
#include "customer.h"
#include "vendor.h"

class AllergyCustomer : public Customer
{
    protected:
        string* allergenList;
        unsigned int allergenCount;

    public:
        AllergyCustomer(){ allergenCount = 0; balance = 0; accountNumber = 0; identifier = 10; } 
        AllergyCustomer(unsigned int newNumber, string newList[], unsigned int arrSize);
        //PRE: Valid vendor, a string of desired food, and the customers account number must be passed in.
        //POST: Account balance may be decreased if the purchase was successful.
        bool buyOne(Vendor& market, string foodName, unsigned int number, string currentDate) override;
};

#endif