//Julian Harris
//CarbCustomer.cs

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

using System;

namespace P5
{
    public class CarbCustomer : Customer
    {
        private uint carbLimit;
        private uint currentCarb;
        public CarbCustomer(uint newNumber, uint newLimit)
        {
            accountNumber = newNumber;
            carbLimit = newLimit;
            currentCarb = 0;
            balance = 0;
        }

        //PRE: Requires a valid vendor, string foodname, and account number to function.
        //POST: Current carb amount may be increased if purchase was successful and contained them, 
        //account balance may be decreased.
        public override bool buyOne(Vendor market, string foodName, uint number)
        {
            uint foodCarbs = market.getNutrient(foodName, "carbohydrates"); //market.getInfo("sugar", foodName);
            if ((currentCarb + foodCarbs) <= carbLimit) //if the customer can safely consume
            {
                currentCarb += foodCarbs;
                return base.buyOne(market, foodName, number);
            }
            return false;
        }

        public override void buy(Vendor market, uint number, string[] list)
        {  //passing in a list of names user wants to buy 
            for (int i = 0; i < list.Length; i++)
                buyOne(market, list[i], number); //for every name in the list, we try to buy it
        }
    }
}
