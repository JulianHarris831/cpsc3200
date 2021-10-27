//Julian Harris
//Entree.cpp

#include "entree.h"

using namespace std;

string Entree::getName() { return name; }

bool Entree::isExpired(string currentDate) //uses a basic sstream to parse the date. 
{ 
    int year, month, day, cYear, cMonth, cDay; //representing expiration date and current date
    stringstream parser(expDate);
    parser >> year >> month >> day;  //loading expiration date
    stringstream parserCurrent(currentDate);
    parserCurrent >> cYear >> cMonth >> cDay; //loading current date

    if(year > cYear)  //If today's date is larger than the expiration date, it's expired.
        return false; 
    else if(month > cMonth)
        return false;
    else if(day > cDay)
        return false;
    else
        return true;
}  
 
bool Entree::isSpoiled(string currentDate) { return ((perishable && !refrigerated) || isExpired(currentDate)); }

void Entree::outage() { refrigerated = false; }

bool Entree::contained(string input)  
{
    stringstream parser(contains); //stored contains data fed into a ss object to read
    string item;
    bool found = false;
    while(!found && getline(parser, item, '$')){
        if(item == input)
            found = true;
    }
    return found;
}

string Entree::printAll() 
{
    stringstream ss("");
    ss << "Name: " << name << "\nServings: " << servings << "\nCalories: " 
    << calories << "\nTotal fat: " << totalFat << "\nSaturated fat: " << satFat
    << "\nTrans fat: " << transFat << "\nCholesterol: " << cholest << "\nSodium: "
    << sodium << "\nCarbohydrates: " << carbs << "\nFiber: " << fiber << "\nSugars: "
    << sugars << "\nProtein: " << protein << "\nIngredients: " << ingredients 
    << "\n\n";
    return ss.str();
}

Entree::Entree(string data, unsigned int size) //read constructor, needs sstream
{
    stringstream parser(data);
    string entry;
    int validSize = size; //cs1 compiler doesn't like this being unsigned

    for(int i = 0; i < validSize; i++){
        if(getline(parser, entry, '\t')){
                //Translate entry into var in respective switch
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
}