//Julian Harris
//Customer.cs

//CLASS INVARIANTS:
//-Customer holds an accountNumber and a balance.
//-Customer is in a valid state when called with the parameterized constructor, though will not break
//if called with the protected default somehow.

//INTERFACE INVARIANTS:
//When creating a Customer, the client is responsible for the following:
//	1. Inserting their own account number to be used as a password.
//	2. Inserting valid Vendor classes.
//	3. Client should not add negative amounts of funds, though it will not break the class.

//IMPLEMENTATION INVARIANTS:
//-Client has access to all functionality on instantiation.

using System;

namespace P3
{
	public class Customer 
	{
		protected double balance;
		protected uint accountNumber;

		public Customer(uint newAccountNumber)  //user passes in own account num to create
		{
			accountNumber = newAccountNumber;
			balance = 0;
		}
		protected Customer() //default constructor needed for inheritance
		{
			accountNumber = 0;
			balance = 0;
		} 

		//PRE: Client must pass in their own account number and the deposit amount (positive)
		//POST: Balance is increased by the amount passed in from deposit.
		public bool addFunds(double deposit, uint number) 
		{
			if (number != accountNumber)
			{
				Console.WriteLine("Invalid account number.\n");
				return false;
			}
			else
			{
				balance += deposit;
				return true;
			}
		}

		//PRE: A valid vendor, a string of the desired food, and the account number must be passed in.
		//POST: Balance is decreased by the price of the food if the purchase was successful.
		public virtual bool buyOne(Vendor market, string foodName, uint number) 
		{ //bool to simply test if it failed or not
			int foodIndex = market.findFoodIndex(foodName); //assigning index
			double price = market.findPrice(foodName);
			if (foodIndex < 0 || number != accountNumber || price > balance) //if the food is not found, account num was incorrect or customer can't afford, we exit
				return false;

			if (market.sell(foodName)) //this will Console.WriteLine what is bought!
			{
				balance -= price;
				return true;
			}
			return false; //this should never happen, but just to make sure.
			
		}

		//PRE: A valid vendor, a list of the desired foods, and the account number must be passed in.
		//POST: Balance is decreased by the price of the foods if the purchases were successful.
		public virtual void buy(Vendor market, uint number, string[] list) 
        {  //passing in a list of names user wants to buy 
			for (int i = 0; i < list.Length; i++) 
				buyOne(market, list[i], number); //for every name in the list, we try to buy it
        }

	}
}
