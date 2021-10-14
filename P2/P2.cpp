//Julian Harris
//P2.cpp

#include "Entree.h"
#include <iostream>

using namespace std;

int main()
{
    //sampleData input is assumed to be correct, though var setup should ensure most of these work fine and do not risk class security, only weird output. 
    string sampleData = "Fresh Brand - Sliced Apples\t2.5\t70\t0\t0\t0\t0\t0\t19\t3\t15\t0\tapples\teat$my$shorts\t2021 11 25\tyes";
    Entree test(sampleData, 16);

    return 0;
}