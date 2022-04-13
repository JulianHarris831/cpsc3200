using System;

namespace P5
{
    public interface ICustomer
    {
        void buy(Vendor market, uint number, string[] list);
        bool buyOne(Vendor market, string foodName, uint number);
        bool addFunds(double deposit, uint number);
    }
}
