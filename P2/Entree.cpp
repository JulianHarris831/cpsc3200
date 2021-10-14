//Julian Harris
//Entree.cpp

#include "Entree.h"
#include <iostream>
#include <sstream>

using namespace std;

string Entree::getName() { return name; }

bool Entree::isExpired(string currentDate) //will need a basic sstream to parse the date. 
{ 
    int year, month, day, cYear, cMonth, cDay; //representing expiration date and current date
    stringstream parser(expDate);
    parser >> year >> month >> day;
    parser.str(currentDate);
    parser >> cYear >> cMonth >> cDay;

    if(year < cYear)
        return true; 
    else if(month < cMonth)
            return true;
        else if(day < cDay)
                return true;
            else
                return false;
}  
 
bool Entree::isSpoiled(string currentDate) { return (isExpired(currentDate) || (perishable && !refrigerated)); }

bool Entree::contained(string input)  //we will need another stringstream check, parse the contains string and compare it with input. 
{
    stringstream parser(contains);
    string item;
    while(getline(parser, item, '$')){
        if(item == input);
            return true;
    }
    return false;
}

void Entree::printNutrient(string target)
{
    //goddamn c++ can't take a string as an argument for a switch, so this needs to become an if chain with return. REEEEEEE
    switch (target)
    {
        case "calories":
            cout << name << ' ' << target << " info: " << calories << '.';
            break;
        case "servings":
            cout << name << ' ' << target << " info: " << servings << '.';
            break;
        case "total fat":
            cout << name << ' ' << target << " info: " << totalFat << '.';
            break;
        case "saturated fat":
            cout << name << ' ' << target << " info: " << satFat << '.';
            break;
        case "trans fat":
            cout << name << ' ' << target << " info: " << transFat << '.';
            break;
        case "cholesterol":
            cout << name << ' ' << target << " info: " << cholest << '.';
            break;
        case "sodium":
            cout << name << ' ' << target << " info: " << sodium << '.';
            break;
        case "carbohydrates":
            cout << name << ' ' << target << " info: " << carbs << '.';
            break;
        case "fiber":
            cout << name << ' ' << target << " info: " << fiber << '.';
            break;
        case "sugars":
            cout << name << ' ' << target << " info: " << sugars << '.';
            break;
        case "protein":
            cout << name << ' ' << target << " info: " << protein << '.';
            break;
        default:
            cout << name << " does not have " << target << " info, sorry.";
            break;
    } 
}

Entree::Entree(string data, unsigned int size) //read constructor, needs sstream
{
    stringstream parser(data);
    string entry;

    //make a while loop? potentially

    for(int i = 0; i < size; i++){
        if(getline(parser, entry, '\t')){
                //Translate entry into var in respective switch
                cout << "Entering " << entry << " i is " << i << '\n';
                switch(i)
                {
                    case 0: //name
                        name = entry;
                        break;
                    case 1:  //servings 
                        servings = stof(entry);
                        break;
                    case 2: //calories
                        calories = stoi(entry);
                        break;
                    case 3: //totalFat
                        totalFat = stof(entry);
                        break; 
                    case 4: //satFat
                        satFat = stof(entry);
                        break;
                    case 5: //transFat
                        transFat = stof(entry);
                        break;
                    case 6: //cholesterol
                        cholest = stoi(entry);
                        break;
                    case 7: //sodium
                        sodium = stoi(entry);
                        break;
                    case 8: //carbohydrates
                        carbs = stoi(entry);
                        break;
                    case 9: //fiber
                        fiber = stoi(entry);
                        break;
                    case 10: //sugars
                        sugars = stoi(entry);
                        break;
                    case 11: //protein
                        protein = stoi(entry);
                        break; 
                    case 12: //ingredients list
                        ingredients = entry;
                        break;
                    case 13: //contains arr, delimited by $. might move this into the print function/helper, just to keep things light in the fileread.
                        contains = entry;
                        break;
                    case 14: //date data passed in as a string, used only when needed.
                        expDate = entry;
                        break;
                    case 15: //whether or not item needs refrigeration
                        if(entry == "yes"){ 
                            perishable = true;
                            refrigerated = true;
                        }
                        else {
                            perishable = false;
                            refrigerated = false;
                        }
                        break;

                    default:
                        //nothing should happen if wrong index.
                        break;
                }
        }
    }
     cout << "Finished test output\n";
}

//move semantics for copying resize in Vendor. 

//deconstructor, we need to make sure we deallocate anything? nothing is dynamic really. 
Entree::~Entree(){}

