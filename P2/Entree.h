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
    string contains; 

    string expDate; 
    bool perishable;
    bool refrigerated;

public: 

    bool contained(string input);
    bool isExpired(string currentDate);
    bool isSpoiled(string currentDate);
    void printNutrient(string target); 
    string getName();

    Entree(string data, unsigned int size);
    //need to ensure move semantics are called properly with an overloaded assignment operator. 
    ~Entree();
};