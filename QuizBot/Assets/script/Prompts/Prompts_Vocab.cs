//This class stores the prompts for vocab
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompts_Vocab : Array_Prompts
{
    Prompts_Vocab()
    {
        //See array_prompts for definition
        prompts1 = new string[6] {
            "(1) Alphabet",
            "(2) Senses",
            "(3) Schedule",
            "(4) Students",
            "(5) Scent",
            "(6) Taste"
        };

        prompts2 = new string[6] {
            "(1) Transportation",
            "(2) Engine",
            "(3) Vehicles",
            "(4) Pretending",
            "(5) Dangerous",
            "(6) Delicious"
        };

        prompts3 = new string[6] {
            "(1) Stretch",
            "(2) Creatures",
            "(3) Lantern",
            "(4) Harvest",
            "(5) Enormous",
            "(6) Autumn"
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
            "(1) Wild",
            "(2) Jungle",
            "(3) Tame",
            "(4) Branches",
            "(5) Seeds",
            "(6) Stem"
        };

        prompts6 = new string[6] {
            "(1) Veterinarian",
            "(2) Collar",
            "(3) Leash",
            "(4) Farmer",
            "(5) Soil",
            "(6) Wheat"
        };
    }
}
