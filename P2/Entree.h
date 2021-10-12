//Julian Harris
//Entree.h

#include <iostream>

using namespace std;


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
    string *contains = nullptr;  //use uniquePtr? 

    //needs refrigeration bools and expiration
    bool expired; 
    bool perishable;
    bool refrigerated;
    //needs a string for date maybe? or a char array
    unsigned int expDate[8]; //YYYYMMDD

public: 

    bool contained(string input);
    bool isExpired();
    bool isSpoiled();
    void printNutrient(string target); //potentially make a bool?
    string getName();

    Entree(string data, unsigned int size);
    ~Entree();
};