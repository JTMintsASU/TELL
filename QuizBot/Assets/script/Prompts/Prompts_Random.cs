//This script stores a randomized string into all the prompts.
//Used in letter identification to select random letters from participants name
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Prompts_Random : Array_Prompts
{
    // Start is called before the first frame update
    void Start()
    {
        //Randomize string
        string textToScramble = DataManager.childID;
        System.Random randomizer = new System.Random();
        string scrambled = new string(textToScramble.ToCharArray().OrderBy(s => (randomizer.Next(2) % 2) == 0).ToArray());
        //Convert each letter in scrambled to a string in an array
        string[] promptAlpha = {"NULL", "NULL", "NULL", "NULL", "NULL","NULL"};
        for (int pos = 0; (pos < scrambled.Length) && (pos < 6); pos++)
            promptAlpha[pos] = scrambled[pos].ToString();
        //Copy this to all prompts
        prompts1 = promptAlpha;
        prompts2 = promptAlpha;
        prompts3 = promptAlpha;
        prompts4 = promptAlpha;
        prompts5 = promptAlpha;
        prompts6 = promptAlpha;
    }
}
