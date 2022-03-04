//This class stores the static prompts for LNI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompts_LNI : Array_Prompts
{
    Prompts_LNI()
    {
        //See array_prompts for definition
        prompts1 = new string[7] {
            "P",
            "R",
            "E",
            "O",
            "F",
            "S",
            "A"
        };

        prompts2 = new string[7] {
            "D",
            "V",
            "J",
            "L",
            "N",
            "T",
            "B"
        };

        prompts3 = new string[6] {
            "H",
            "Q",
            "G",
            "C",
            "Y",
            "I"
        };

        prompts4 = new string[6] {
            "Astronaut",
            "Rocket",
            "Planet",
            "Fossils",
            "Tracks",
            "Scientist"
        };

        prompts5 = new string[6] {
            "U",
            "X",
            "W",
            "M",
            "K",
            "Z"
        };

        prompts6 = new string[7] {
            "N",
            "F",
            "H",
            "W",
            "P",
            "I",
            "Y"
        };
    }
}
