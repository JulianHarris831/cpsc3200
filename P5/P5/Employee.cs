//Julian Harris
//Employee.cs

//Documentation 
//CLASS INVARIANTS:
//Employee holds a current weekly payment, a static payment level, a balance, an account number, and a name.
//Employee is in a valid state when called with the supplied constructor.

//INTERFACE INVARIANTS:
//When creating an Employee, the client is responsible for the following
//  1. Inserting their own account number. 
//  2. Inserting their own pay level and name.

//IMPLEMENTATION INVARIANTS: 
//  Client must call payday manually to have funds added to balance.

using System;

namespace P5
{
    public class Employee : IEmployee
    {
        private const double plOne = 200; //pay level one, two, three
        private const double plTwo = 400;
        private const double plThree = 600;

        private double weeklyPay = 0;
        private double payLevel = 0;
        private double balance = 0; //cannot be negative
        private string name = "";
        private uint accountNumber = 0;

        private Employee() { } //restricting default access
        public Employee(uint pl, uint newNum, string n)
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
        }

        //PRE: None
        //POST: Balance is updated with the remaining weeklyPay that was not spent. 
        public void payday() { balance += weeklyPay; }
        //PRE: None
        //POST: weeklyPay is reset to the default pay level, removing changes spent on customer goods.
        public void setLevel() { weeklyPay = payLevel; } //resets to normal weekly pay.
        //PRE: None
        //POST: None
        public double getBalance() { return balance; }

        //PRE: None
        //POST: None
        public void printInfo()
        {
            Console.WriteLine($"Name: {name}\nWeekly Pay: {weeklyPay}");
        }
    }
}
