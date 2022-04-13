//Julian Harris
//EmployeeCustomer.cs

//Documentation 
//CLASS INVARIANTS:
//EmployeeCustomer holds a current weekly payment, a static payment level, a balance, an account number, and a name.
//The customer side holds a workPlace name as well that must match where they purchase.
//EmployeeCustomer is in a valid state when called with the supplied constructor.

//INTERFACE INVARIANTS:
//When creating an EmployeeCustomer, the client is responsible for the following
//  1. Inserting their own account number. 
//  2. Inserting their own pay level and name.
//  3. Inserting a valid customer of desired type when constructing.
//  4. Inserting valid Vendor classes for purchases.
//  5. Client should not add negative amounts of funds.

//IMPLEMENTATION INVARIANTS: 
//  Client must call payday manually to have funds added to balance.
//  Client must use updatePay to subtract funds. 

using System;

namespace P5
{
    public class EmployeeCustomer : IEmployee, ICustomer
    {
        private const double plOne = 200; //pay level one, two, three
        private const double plTwo = 400;
        private const double plThree = 600;

        private double weeklyPay = 0;
        private double payLevel = 0;
        private double balance = 0; //cannot be negative
        private string name = "";
        private string workPlace = "";
        private uint accountNumber = 0;

        private Customer account;

        private EmployeeCustomer() { }

        //Primary constructor used
        public EmployeeCustomer(uint pl, uint newNum, string n, string work, Customer newAccount)
        {
            if (pl == 1)
                payLevel = plOne;
            if (pl == 2)
                payLevel = plTwo;
            if (pl == 3)
                payLevel = plThree;  //if none of these, pay level remains at 0.
            weeklyPay = payLevel;

            accountNumber = newNum;
            name = n;
            workPlace = work;
            account = newAccount; //better copying?
        }

        //PRE: Must be money within account or payment through weeklyPay balance will not work.
        //POST: weeklyPay is updated or funds within account are decreased.
        public bool buyOne(Vendor market, string foodName, uint number)
        {
            if(workPlace == market.getName() && account.buyOne(market, foodName, number))
            {
                if(updatePay(market.findPrice(foodName))) //paying through paycheck
                    account.addFunds(market.findPrice(foodName), account.getNumber()); //reimbursing if able to pay through paycheck, otherwise customer account pays
                return true;
            }
            return false;
        }

        //PRE: Must be money within account or payment through weeklyPay balance will not work.
        //POST: weeklyPay is updated or funds within account are decreased.
        public void buy(Vendor market, uint number, string[] list)
        {
            for (int i = 0; i < list.Length; i++)
                buyOne(market, list[i], number); //for every name in the list, we try to buy it
        }

        //PRE: Valid parameters
        //POST: Customer account member var balance may be updated. This is unrelated to the Employee balance.
        public bool addFunds(double deposit, uint number) { return account.addFunds(deposit, number); }

        //PRE: None
        //POST: Balance is updated with the remaining weeklyPay that was not spent. 
        public void payday() { balance += weeklyPay; }

        //PRE: None
        //POST: weeklyPay is reset to the default pay level, removing changes spent on customer goods.
        public void setLevel() { weeklyPay = payLevel; } //resets to normal weekly pay.

        //PRE: None
        //POST: None
        public double getBalance() { return balance; }

        //PRE: Requires a valid double matching the item being purchased to function properly.
        //POST: weeklyPay is subtracted by the price of purchased item if successful.
        public bool updatePay(double p)  //subtracting from paycheck price of item
        {
            if (weeklyPay >= p) //if item can be afforded via paycheck
            {
                weeklyPay -= p;
                return true;
            }
            return false;
        }

        //PRE: None
        //POST: None
        public void printInfo()
        {
            Console.WriteLine($"Name: {name}\nWorkplace: {workPlace}\nWeekly Pay: {weeklyPay}");
        }
    }
}
