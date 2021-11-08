//Julian Harris
//DbetCustomer.cs

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

using System;

namespace P3
{
    public class DbetCustomer : Customer  //diabetic customer uses all of customer, inheritance
    {
        private uint sugarLimit;
        private uint currentSugar;

        //basic valid setup, requires a sugar limit and an account number.
        public DbetCustomer(uint newNumber, uint newLimit) 
        {
            accountNumber = newNumber;
            sugarLimit = newLimit;
            currentSugar = 0;
            balance = 0;
        }

        //PRE: Valid vendor, a string of desired food, and the customers account number must be passed in.
        //POST: Current sugar amount is increased if purchase is successful, and balance is decreased.
        public override bool buyOne(Vendor market, string foodName, uint number) 
        {
            uint foodSugar = market.getNutrient(foodName, "sugars");  
            if ((currentSugar + foodSugar) <= sugarLimit) //if the customer can safely consume
            {
                currentSugar += foodSugar;
                return base.buyOne(market, foodName, number);
            }
            return false; 
        }
        
    }
}
