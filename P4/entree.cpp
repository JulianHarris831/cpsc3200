//Julian Harris
//Entree.cpp

#include "entree.h"

using namespace std;

string Entree::getName() { return name; }

void Entree::operator=(Entree& original)
{
    name = original.getName();
    servings = original.getServings();
    calories = original.getCalories();
    totalFat = original.getTotalFat();
    satFat = original.getSatFat();
    transFat = original.getTransFat();
    cholest = original.getCholest();
    sodium = original.getSodium();
    carbs = original.getCarbs();
    fiber = original.getFiber();
    sugars = original.getSugar();
    contains = original.getContains();
    ingredients = original.getIngredients();
    expDate = original.getExpDate();
    perishable = original.getPerishable();
}

bool Entree::isExpired(string currentDate) //uses a basic sstream to parse the date. 
{ 
    int year, month, day, cYear, cMonth, cDay; //representing expiration date and current date
    stringstream parser(expDate);
    parser >> year >> month >> day;  //loading expiration date
    stringstream parserCurrent(currentDate);
    parserCurrent >> cYear >> cMonth >> cDay; //loading current date

    if(year > cYear)  //If today's date is larger than the expiration date, it's expired.
        return false; 
    if(year < cYear)
        return true;   //only continue if they are equal!
    if(month > cMonth) //we have to check both because we NEED to stop
        return false;  //if it is clearly expired or not expired, but continue if equal
    if(month < cMonth)
        return true; 
    if(day > cDay)
        return false;
    if(day < cDay)
        return true;

    return true; //if we make it to this point, that means the expiration date and 
                 //current date match, so it is expired.
}  
 
bool Entree::isSpoiled(string currentDate, bool refrigerated) { return ((perishable && !refrigerated) || isExpired(currentDate)); }

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

Entree::Entree(string data) //read constructor, needs sstream
{
    stringstream parser(data);
    string entry;

    for(int i = 0; i < 16; i++){
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
                        perishable = (entry == "yes");
                        break;

                    default:
                        //nothing should happen if wrong index.
                        break;
                }
        }
    }
}