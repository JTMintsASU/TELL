/*
//This class stores the static prompts for LNI
//Should be ok to delete with new random combo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompts_LNI : Array_Prompts
{
    //Used to see what letters we already know and adapt out of them
    //bool[] adaptThis = DataManager.learnedLetterNames;

    void Start()
    {
        //See array_prompts for definition
        prompts1 = new string[7] {
            "A",
            "R",
            "E",
            "O",
            "F",
            "S",
            "P"
        };

        prompts2 = new string[7] {
            "A",
            "V",
            "J",
            "L",
            "N",
            "T",
            "B"
        };

        prompts3 = new string[6] {
            "A",
            "Q",
            "G",
            "C",
            "Y",
            "I"
        };
        prompts3 = adaptPrompts(prompts3);

        prompts4 = new string[6] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"
        };
        prompts4 = adaptPrompts(prompts4);

        prompts5 = new string[6] {
            "A",
            "X",
            "W",
            "M",
            "K",
            "Z"
        };
        prompts5 = adaptPrompts(prompts5);

        prompts6 = new string[7] {
            "A",
            "F",
            "H",
            "W",
            "P",
            "I",
            "Y"
        };
        prompts6 = adaptPrompts(prompts6);
    }

    //This function alters the letter prompts based on adaptive scoring
    //Optimization: figure out how to adapt only on the current time
    /*string[] adaptPrompts(string[] vanilla)
    {
        string[] result = vanilla;
        int count = -1;
        //Look for 'passed' letters
        foreach (bool letter in adaptThis)
        {
            Debug.Log("adaptPrompts loop letter");
            count++;
            if (letter)
            {
                //Find out if 'passed' letter matches any letters in the prompts
                string skipMe = ((char)(count + 65)).ToString(); //65 is code for 'A'
                for (int loop = 0; loop < result.Length; loop++)
                {
                    Debug.Log("adaptPrompts internal loop");
                    if (skipMe == result[loop])
                    {
                        //Continue to look for new letter to test
                        //until we find one that isn't 'passed'
                        int promptNumber;
                        do 
                        {
                            Debug.Log("LNI DoWhile");
                            promptNumber = (int)(char.Parse(result[loop]) - 65);
                            promptNumber += 1; //logic that controls what new letter we will provide
                        } while (adaptThis[promptNumber]);
                        result[loop] = ((char)(promptNumber + 65)).ToString();
                    }
                }
            }
        }
        return result;
    }
}*/
