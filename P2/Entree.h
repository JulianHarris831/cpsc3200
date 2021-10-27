//Julian Harris
//Entree.h

#ifndef ENTREE_H
#define ENTREE_H

#include <iostream>
#include <sstream>

using namespace std;

//Class invariants:
//-Entree holds a name, nutrition facts, a list of ingredients, an expiration date,
//      and refrigeration requirements.
//-Entree object is in valid state when initalized with the parameterized constructor.
//      Blank constructor Entree is for deep copying functionality with other classes.
//-Entree cannot be used to get any nutrition facts until initialized.

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
    bool refrigerated;

public: 

    //primary parameterized constructor
    Entree(string data, unsigned int size);
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
    bool isSpoiled(string currentDate);
    
    //PRE: None
    //POST: refrigerated is set to false
    void outage(); 
   
    //PRE: Requires initialized Entree or will fail.
    //POST: None
    string printAll(); //outputs all nutrient data
   
    //PRE: Requires initialized Entree, and a nutrient type from the user.
    //POST: None
    void printNutrient(string target); //checks with private vars to see if target matches one, then prints it if yes.
    string getName(); 

};

#endif