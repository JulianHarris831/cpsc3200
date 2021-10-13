//Julian Harris
//Entree.cpp

#include "Entree.h"
#include <iostream>
#include <sstream>
//#include <fstream> don't read files, just passing in a string to read

using namespace std;

string Entree::getName() { return name; }

bool Entree::isExpired() { return expired; }

bool Entree::isSpoiled() { return (expired || (perishable && !refrigerated)); }

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
                        if(entry != ""){
                            //declaration of counter or containParser here?
                            stringstream containParser(entry);
                            string item;
                            string *temp = new string[20]; //DELETE ALL THIS, STORE IN NORMAL STRING, PARSE DURING PRINT.
                            int counter = 0;
                            while((getline(containParser, item, '$'))){
                                temp[counter] = item;
                                cout << item << '\n';
                                counter++;
                            }
                            contains = new string[counter];
                            contains = temp;
                        }
                        break;
                    default:
                        cout << "Entered default\n";
                        break;
                }
        }
    }
     cout << "Finished test output\n";
}

Entree::~Entree(){}

