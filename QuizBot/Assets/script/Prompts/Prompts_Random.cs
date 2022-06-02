//This script stores a randomized string into all the prompts.
//Used in letter identification to select random letters from participants name
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prompts_Random : Array_Prompts
{
    public string[] alphabet; //Used to store main chunk of alphabet letters in part 2
    public string[] prefixAlphabets;
    public string[] suffixAlphabets;
    //Used to see what letters we already know and adapt out of them
    bool[] adaptLNIResults = DataManager.learnedLetterNamesLNI;
    bool[] adaptLSIResults = DataManager.learnedLetterNamesLSI;

    // Start is called before the first frame update
    void Awake()
    {
        if (DataManager.globalGame == "LNI_Instructions")
        {
            //Initialize and shuffle alphabet
            alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
                "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //Knuth shuffle algorithm
            for (int t = 0; t < alphabet.Length; t++)
            {
                string tmp = alphabet[t];
                System.Random ran = new();
                int r = ran.Next(t, alphabet.Length);
                alphabet[t] = alphabet[r];
                alphabet[r] = tmp;
            }

            //Input proofing
            string textToScramble = DataManager.childNameLNI;
            textToScramble = new string(textToScramble.Where(c => char.IsLetter(c)).ToArray()); //letters only
            textToScramble = textToScramble.ToUpper(); //All uppercase
            DataManager.childNameLNI = ""; //Remove PII now that it is no longer needed

            //Randomize string
            System.Random randomizer = new();
            string scrambled = new(textToScramble.ToCharArray().OrderBy(s => (randomizer.Next(2) % 2) == 0).ToArray());
            //Convert each letter in scrambled to a string in an array
            string[] promptAlpha = {"NULL", "NULL", "NULL", "NULL", "NULL", "NULL"};
            for (int pos = 0; (pos < scrambled.Length) && (pos < 6); pos++)
            {
                promptAlpha[pos] = scrambled[pos].ToString();
            }

            alphabet = RemoveSecondFromFirst(alphabet, promptAlpha);

            //Combine arrays together, in order
            List<string> combinedList = new();
            combinedList.AddRange(promptAlpha);
            combinedList.AddRange(alphabet);

            string[] adaptingArray = combinedList.ToArray();

            adaptingArray = adaptPromptsForLNI(adaptingArray);

            //Copy this to prompt 1
            prompts1 = adaptingArray;
        }
        
        if (DataManager.globalGame == "LSI_Instructions")
        {
            prefixAlphabets = DataManager.exceptionalTwoStageCharactersLSI.ToArray();
            //Initialize and shuffle alphabet
            suffixAlphabets = new string[] {"C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", 
                "Q", "R", "U", "V", "W", "X", "Y", "Z" };
            //Knuth shuffle algorithm
            for (int t = 0; t < prefixAlphabets.Length; t++)
            {
                string tmp = prefixAlphabets[t];
                System.Random ran = new();
                int r = ran.Next(t, prefixAlphabets.Length);
                prefixAlphabets[t] = prefixAlphabets[r];
                prefixAlphabets[r] = tmp;
            }
            //Knuth shuffle algorithm
            for (int t = 0; t < suffixAlphabets.Length; t++)
            {
                string tmp = suffixAlphabets[t];
                System.Random ran = new();
                int r = ran.Next(t, suffixAlphabets.Length);
                suffixAlphabets[t] = suffixAlphabets[r];
                suffixAlphabets[r] = tmp;
            }

            //Combine arrays together, in order
            List<string> combinedList = new();
            combinedList.AddRange(prefixAlphabets);
            combinedList.AddRange(suffixAlphabets);

            string[] adaptingArray = combinedList.ToArray();

            adaptingArray = adaptPromptsForLSI(adaptingArray);

            //Copy this to prompt 1
            prompts1 = adaptingArray;
        }
        
    }

    //Function removes any matches of second parameter from first parameter
    string[] RemoveSecondFromFirst(string[] bigArray, string[] smallArray)
    {
        foreach(string letter in smallArray)
        {
            bigArray = bigArray.Where(val => val != letter).ToArray(); 
        }

        return bigArray;
    }

    
    //This function alters the letter prompts based on adaptive scoring
    //Optimization: figure out how to adapt only on the current time
    string[] adaptPromptsForLNI(string[] vanilla)
    {
        string[] result = vanilla;
        int count = -1;
        //Look for 'passed' letters
        foreach (bool letter in adaptLNIResults)
        {
            count++;
            if (letter)
            {
                //Find out if 'passed' letter matches any letters in the prompts
                string skipMe = ((char)(count + 65)).ToString(); //65 is code for 'A'
                for (int loop = 0; loop < result.Length; loop++)
                {
                    if (skipMe == result[loop])
                    {
                        //Add CSkip grade
                        DataManager.individual_LNI[count, DataManager.globalTime - 1] = AdaptiveResponse.CSKIP;
                        //Remove element
                        result = result.Where(val => val != skipMe).ToArray();
                    }
                }
            }
        }
        return result;
    }

    
    //This function alters the letter prompts based on adaptive scoring
    //Optimization: figure out how to adapt only on the current time
    string[] adaptPromptsForLSI(string[] vanilla)
    {
        string[] result = vanilla;
        int count = -1;
        //Look for 'passed' letters
        foreach (bool letter in adaptLSIResults)
        {
            count++;
            if (letter)
            {
                //Find out if 'passed' letter matches any letters in the prompts
                string skipMe = ((char)(count + 65)).ToString(); //65 is code for 'A'
                for (int loop = 0; loop < result.Length; loop++)
                {
                    if (skipMe == result[loop])
                    {
                        //Add CSkip grade
                        DataManager.individual_LSI[count, DataManager.globalTime - 1] = AdaptiveResponse.CSKIP;
                        //Remove element
                        result = result.Where(val => val != skipMe).ToArray();
                    }
                }
            }
        }
        return result;
    }
}
