//Julian Harris
//AllergyCustomer.cs

//CLASS INVARIANTS:
//-AllergyCustomer class requires a list of items they are allergic to to be passed in along with the account number.
//-AllergyCustomer class is in a valid state immediately after instantiation.

//INTERFACE INVARIANTS:
//-When creating a AllergyCustomer, the client is responsible for the following:
//  1. Keeping track of their own account number to use to access the object.
//  2. Setting up their own allergens to be checked with their purchased food.

//IMPLEMENTATION INVARIANTS:
//Client has access to all functionality upon implementation.

using System;


namespace P5
{
    public class AllergyCustomer : Customer
    {
        private string[] allergies;
        public AllergyCustomer(uint newNumber, string[] newAllergies)
        {
            accountNumber = newNumber;
            balance = 0;
            allergies = newAllergies;
        }

        //PRE: Valid vendor, a string of desired food, and the customers account number must be passed in.
        //POST: Account balance may be decreased if the purchase was successful.
        public override bool buyOne(Vendor market, string foodName, uint number)
        {
            bool allergic = false; //assumes false to start

            if (allergies == null || allergies.Length == 0)
                allergic = false; //if we have no allergies for some reason, not allergic!
            else
            {
                for (int i = 0; i < allergies.Length; i++)
                    if (market.getContains(foodName, allergies[i]) == true)
                        allergic = true; //if an allergic triggers EVEN once, permanently mark allergic
            }
            if (!allergic)
                return base.buyOne(market, foodName, number);
            return false;
        }

        public override void buy(Vendor market, uint number, string[] list)
        {  //passing in a list of names user wants to buy 
            for (int i = 0; i < list.Length; i++)
                buyOne(market, list[i], number); //for every name in the list, we try to buy it
        }

    }
}
