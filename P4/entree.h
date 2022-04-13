//Julian Harris
//Entree.h

#ifndef ENTREE_H
#define ENTREE_H

#include <iostream>
#include <sstream> //needed to parse the date and read in file

using namespace std;

//Class invariants:
//-Entree holds a name, nutrition facts, a list of ingredients, an expiration date,
//      and refrigeration requirements.
//-Entree object is in valid state when initalized with the parameterized constructor.
//      Blank constructor Entree is for deep copying functionality with other classes.
//-Entree cannot be used to get any nutrition facts until initialized.
//Reasons for avoided overloaded operators
//Addition, subtraction, multiplication, etc do not make sense for an Entree
//as it stores a collection of unrelated numbers that shouldn't be combined.
//Comparison/relation operators do not make sense as there are too many things to compare,
//only if you wanted to specifically compare calories which is not useful.
//Indexing operators not overloaded because no array or any kind of list.
//Stream was not used because a specific print returned for use within printing a list, 
//not very effective to simply stream it. Streaming input is too vague.

//Interface invarients:
//-When creating an Entree, the client is responsible for the following:
//  1. All nutritional facts, expiration data, and ingredients must in proper order.
//  2. Contains list must be delimited by $ between items.
//  3. The expiration date format must be YYYY/MM/DD
//  4. MUST pass in 16 with the constructor

//Implementation invarients:
//-Client cannot get nutritional facts or print anything if they have declared the
//  default constructor outside of use with copies.
//-Client must request isExpired/isSpoiled to get relevant information each time,
//  they are not saved.

class Entree
{
private: 

    string name; 
    float servings;
    unsigned int calories;
    float totalFat;
    float satFat;
    float transFat;
    unsigned int cholest;
    unsigned int sodium;
    unsigned int carbs;
    unsigned int fiber;
    unsigned int sugars; 
    unsigned int protein;

    string ingredients; 
    string contains; //parsed with stringstream to read

    string expDate;  //parsed with stringstream to read
    bool perishable;

public: 

    //primary parameterized constructor
    Entree(string data);
    //constructor for deep copies, used with pointers
    Entree(){} 

    //PRE: Initialized Entree, and a 
    //POST: None
    bool contained(string input); //checks input with private string contains, true returned if inside.
    
    //PRE: Initialized Entree, and the current date passed in to see if expired.
    //POST: None
    bool isExpired(string currentDate); //checks given date with expDate, returns true if date is older.
    
    //PRE: Initialized Entree, and the current date passed in to see if expired.
    //POST: None
    bool isSpoiled(string currentDate, bool refrigerated);
   
    //PRE: Requires initialized Entree or will fail.
    //POST: None
    string printAll(); //outputs all nutrient data

    //Overloaded operators
    void operator=(Entree& original);
    bool operator==(Entree& b){ return name == b.getName(); }

    //MANY getters required for recording data sold, and for assignment operator
    string getName();  
    float getServings() { return servings; }
    unsigned int getCalories() { return calories; } 
    float getTotalFat() { return totalFat; }
    float getSatFat() { return satFat; }
    float getTransFat() { return transFat; }
    unsigned int getCholest() { return cholest; }
    unsigned int getSodium() { return sodium; }
    unsigned int getCarbs() { return carbs; }
    unsigned int getFiber() { return fiber; }
    unsigned int getSugar() { return sugars; }
    unsigned int getProtein() { return protein; }
    string getContains() { return contains; } 
    string getIngredients() { return ingredients; }
    string getExpDate() { return expDate; }  
    bool getPerishable() { return perishable; }
};

#endif