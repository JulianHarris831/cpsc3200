//Julian Harris
//Vendor.cpp

#include "vendor.h"

using namespace std;

//vendor needs a name and whether or not it is refrigerated
Vendor::Vendor(string newName, bool isFridge) 
{
    vendorName = newName;
    refrigerated = isFridge;
}  

Vendor Vendor::copy() //copy function, returns a new object. 
{ 
    Vendor newCopy(this->vendorName, this->refrigerated);
    if(head == nullptr) //if class is empty, just return an empty object
        return newCopy;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        newCopy.load(nodePtr);  //loading all valid nodes into the copy
        nodePtr = nodePtr->next;
    }
    return newCopy;
} 

void Vendor::load(string data, unsigned int size, float newPrice, unsigned int newStock)
{
    //do bounds checking outside of function? just for clarity...
    Node* newNode = new Node(); //creating newNode first...
    newNode->price = newPrice;
    newNode->stock = newStock;
    newNode->food = new Entree(data, size);

    //could potentially add at the front, and push head down. nahhhh

    if(head == nullptr)
        head = newNode;
    else{
        Node* nodePtr = head; 
        while(nodePtr->next != nullptr)
            nodePtr = nodePtr->next;
        nodePtr->next = newNode;
    }
}

void Vendor::load(Node* original) //for Entree deep copies.
{
    Node* newNode = new Node(); //creating newNode first...
    newNode->food = new Entree(); //blank constructor for copy
    *(newNode->food) = *(original->food);
    newNode->price = original->price;
    newNode->stock = original->stock;

    if(head == nullptr)
        head = newNode;
    else{
        Node* nodePtr = head; 
        while(nodePtr->next != nullptr)
            nodePtr = nodePtr->next;
        nodePtr->next = newNode;
    }
}

string Vendor::printMenu()
{
    if(head == nullptr)
        return "There is nothing on the menu, sorry!\n\n";
    stringstream ss;
    ss << "\nPrinting menu of " << vendorName << "\n\n";
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        //gathering a string to return and print in main
        ss << "Item: " << nodePtr->food->getName() << '\n'
            << "Price: $" << nodePtr->price << '\n'
            << "Stock: " << nodePtr->stock << "\n\n";
        nodePtr = nodePtr->next;
    }
    return ss.str();
}

string Vendor::printAllInfo()
{
    if(head == nullptr)
        return "There is nothing on the menu, sorry!\n\n";
    stringstream ss;
    ss << "\nPrinting nutrient info of " << vendorName << "\n\n";
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        //gathering a string to return and print in main
        ss << nodePtr->food->printAll();
        nodePtr = nodePtr->next;
    }
    return ss.str();
}

void Vendor::sell(Customer buyer, unsigned int accountNumber, string foodName, string currentDate)
{
    if(head != nullptr && isStocked(foodName, currentDate)){ //if list is not empty and food is in stock, go ahead.
        Node* nodePtr = head;
        bool sold = false;
        while(nodePtr != nullptr){ //iterating through list till we find target food.
            if(!sold && nodePtr->food->getName() == foodName && buyer.purchaseItem(nodePtr->price, accountNumber)){ //Ensures target is found and customer can purchase.
                //go through with sale, customer has already purchased.
                nodePtr->stock -= 1; //one item has been sold
                cout << "Sale complete! Enjoy your " << nodePtr->food->getName() << '\n';
                sold = true;
            }
            nodePtr = nodePtr->next;
        }
    }
    else
        cout << "That food is not in stock or is expired.\n";
}

void Vendor::powerOutage()
{
    if(head != nullptr && refrigerated == true){
        Node* nodePtr = head;
        while(nodePtr != nullptr){
            nodePtr->food->outage();
            nodePtr = nodePtr->next;
        }
    }
    refrigerated = false;

}

bool Vendor::isStocked(string foodName, string currentDate)
{
    if(head == nullptr)
        return false;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName && !(nodePtr->food->isSpoiled(currentDate))) 
            return (nodePtr->stock > 0); //If there is stock left, says it is stocked.
        nodePtr = nodePtr->next;
    }
    return false; //if item was not found in menu
}

void Vendor::cleanStock(string currentDate)
{
    if(head != nullptr){
        Node* nodePtr = head;
        Node* prevPtr = nullptr;
        while(nodePtr != nullptr){
            if(nodePtr->stock < 1 || nodePtr->food->isSpoiled(currentDate)){
                cout << nodePtr->food->getName() << ' ' << nodePtr->stock 
                << " count have been removed.\n"; //printing what has been removed and how many
                
                //deleting the current item here. 
                if(head == nodePtr){  //first element in list needs to be removed
                    head = nodePtr->next;
                    delete nodePtr;
                    nodePtr = head;
                }
                else{  //non first element removal
                    prevPtr->next = nodePtr->next;
                    delete nodePtr;
                    nodePtr = prevPtr->next;
                }
            }
            else if(nodePtr != nullptr){
                prevPtr = nodePtr;
                nodePtr = nodePtr->next;
            }
        }
        cout << vendorName << " has been cleaned.\n";
    }
    else
        cout << "Nothing to clean! " << vendorName << " is empty.\n";
}

////needs to deallocate linked list.
Vendor::~Vendor() 
{
    /*
    Node* nodePtr = head;
    Node* nextPtr;

    while(nodePtr){
        nextPtr = nodePtr->next;
        delete nodePtr->food;
        delete nodePtr;
        nodePtr = nextPtr;
    }
    head = nullptr;
    */
}
