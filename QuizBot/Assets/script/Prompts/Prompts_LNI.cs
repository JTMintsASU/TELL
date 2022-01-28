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
            "(1) Part 2 P",
            "(2) R",
            "(3) E",
            "(4) O",
            "(5) F",
            "(6) S",
            "(7) A"
        };

        prompts2 = new string[7] {
            "(1) D",
            "(2) V",
            "(3) J",
            "(4) L",
            "(5) N",
            "(6) T",
            "(7) B"
        };

        prompts3 = new string[6] {
            "(1) H",
            "(2) Q",
            "(3) G",
            "(4) C",
            "(5) Y",
            "(6) I"
        };

        prompts4 = new string[6] {
            "(1) Astronaut",
            "(2) Rocket",
            "(3) Planet",
            "(4) Fossils",
            "(5) Tracks",
            "(6) Scientist"
        };

        prompts5 = new string[6] {
            "(1) U",
            "(2) X",
            "(3) W",
            "(4) M",
            "(5) K",
            "(6) Z"
        };

        prompts6 = new string[7] {
            "(1) N",
            "(2) F",
            "(3) H",
            "(4) W",
            "(5) P",
            "(6) I",
            "(7) Y"
        };
    }
}
