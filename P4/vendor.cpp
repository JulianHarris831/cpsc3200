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

Vendor::Vendor()
{
    vendorName = "un-named";
    refrigerated = false;
}

void Vendor::operator=(Vendor& original)
{
    vendorName = original.getName();
    refrigerated = original.getRefrigerated();

    if(head == original.head)
        return;
    Node* nodePtr = original.head;
    while(nodePtr != nullptr){
        load(nodePtr); //loading all information into new vendor.
        nodePtr = nodePtr->next;
    }
}

void Vendor::operator+(const Vendor& b)
{
    //Adding all nodes in b to a, does not matter if it's the same food you can have two.
    Node* nodePtr = b.head;
    while(nodePtr != nullptr){
        load(nodePtr);
        nodePtr = nodePtr->next;
    }
}

//We need to roll through the arr and count each, see which is higher, then done.
bool Vendor::operator<(const Vendor& b) //returns if a is less than b
{
    Node* aPtr = head;
    unsigned int aCount = 0;
    Node* bPtr = b.head;
    unsigned int bCount = 0;
    while(aPtr != nullptr){
        aPtr = aPtr->next;
        aCount++;
    }
    while(bPtr != nullptr){
        bPtr = bPtr->next;
        bCount++;
    }
    return aCount < bCount;
} 

bool Vendor::operator>(const Vendor& b) //returns if a is greater than b
{
    Node* aPtr = head;
    unsigned int aCount = 0;
    Node* bPtr = b.head;
    unsigned int bCount = 0;
    while(aPtr != nullptr){
        aPtr = aPtr->next;
        aCount++;
    }
    while(bPtr != nullptr){
        bPtr = bPtr->next;
        bCount++;
    }
    return aCount > bCount;
} 

bool Vendor::operator==(const Vendor& b) //returns if a is equal to b
{
    Node* aPtr = head;
    unsigned int aCount = 0;
    Node* bPtr = b.head;
    unsigned int bCount = 0;
    while(aPtr != nullptr){
        aPtr = aPtr->next;
        aCount++;
    }
    while(bPtr != nullptr){
        bPtr = bPtr->next;
        bCount++;
    }
    return aCount == bCount;
} 

bool Vendor::operator!=(const Vendor& b)
{
    Node* aPtr = head;
    unsigned int aCount = 0;
    Node* bPtr = b.head;
    unsigned int bCount = 0;
    while(aPtr != nullptr){
        aPtr = aPtr->next;
        aCount++;
    }
    while(bPtr != nullptr){
        bPtr = bPtr->next;
        bCount++;
    }
    return aCount != bCount;
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

void Vendor::load(string data, float newPrice, unsigned int newStock)
{
    //do bounds checking outside of function? just for clarity...
    Node* newNode = new Node(); //creating newNode first...
    newNode->price = newPrice;
    newNode->stock = newStock;
    newNode->food = new Entree(data);

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

string Vendor::printDataCount()
{
    stringstream ss;
    ss << "\nPrinting total sold nutrients of " << vendorName << "\n\n";
    ss << "Servings: " << DataCount.servings << "\nCalories: " 
    << DataCount.calories << "\nTotal fat: " << DataCount.totalFat << "\nSaturated fat: " << DataCount.satFat
    << "\nTrans fat: " << DataCount.transFat << "\nCholesterol: " << DataCount.cholest << "\nSodium: "
    << DataCount.sodium << "\nCarbohydrates: " << DataCount.carbs << "\nFiber: " << DataCount.fiber << "\nSugars: "
    << DataCount.sugars << "\nProtein: " << DataCount.protein << "\n\n";
    return ss.str();
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

bool Vendor::sell(string foodName, string currentDate)
{
    if(head != nullptr && isStocked(foodName, currentDate)){ //if list is not empty and food is in stock, go ahead.
        Node* nodePtr = head;
        bool sold = false;
        while(nodePtr != nullptr){ //iterating through list till we find target food.
            if(!sold && nodePtr->food->getName() == foodName){ //Ensures target is found and customer can purchase.
                //go through with sale, customer has already purchased.
                nodePtr->stock -= 1; //one item has been sold
                cout << "Sale complete! Enjoy your " << nodePtr->food->getName() << '\n';
                sold = true;
                loadData(nodePtr->food);
            }
            nodePtr = nodePtr->next;
        }
        return sold;
    }
    else
        cout << "That food is not in stock or is expired.\n";
        return false;
}

void Vendor::loadData(Entree* soldItem)
{
    DataCount.servings += soldItem->getServings();
    DataCount.calories += soldItem->getCalories();
    DataCount.totalFat += soldItem->getTotalFat();
    DataCount.satFat += soldItem->getSatFat();
    DataCount.transFat += soldItem->getTransFat();
    DataCount.cholest += soldItem->getCholest();
    DataCount.sodium += soldItem->getSodium();
    DataCount.carbs += soldItem->getCarbs();
    DataCount.fiber += soldItem->getFiber();
    DataCount.sugars += soldItem->getSugar();
    DataCount.protein += soldItem->getProtein();
}

//may need a couple getter functions or something to work here with new buy
double Vendor::getPrice(string foodName)
{
    if(head == nullptr)
        return -1;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName)
            return nodePtr->price;
        nodePtr = nodePtr->next;
    }
    return -1;
}

int Vendor::getSugar(string foodName)
{
    if(head == nullptr)
        return -1;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName)
            return nodePtr->food->getSugar(); //returns the sugars
        nodePtr = nodePtr->next;
    }
    return -1; //we will know it failed if we get a negative
} 

int Vendor::getCarbs(string foodName)
{
    if(head == nullptr)
        return -1;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName)
            return nodePtr->food->getCarbs(); //returns the carbs
        nodePtr = nodePtr->next;
    }
    return -1;
}

bool Vendor::checkContains(string foodName, string allergy)
{
    if(head == nullptr)
        return false;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName) //ensures item is in the list
            return nodePtr->food->contained(allergy); //returns whether or not allergy was inside
        nodePtr = nodePtr->next;
    }
    return false;
}

void Vendor::powerOutage(){ refrigerated = false; }

bool Vendor::isStocked(string foodName, string currentDate)
{
    if(head == nullptr)
        return false;
    Node* nodePtr = head;
    while(nodePtr != nullptr){
        if(nodePtr->food->getName() == foodName && !(nodePtr->food->isSpoiled(currentDate, refrigerated))) 
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
            if(nodePtr->stock < 1 || nodePtr->food->isSpoiled(currentDate, refrigerated)){
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
