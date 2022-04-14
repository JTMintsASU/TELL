//This class is a variant of advancetext
//Designed for use with the 25 prompts of the alphabet
//Used by LNI and LSi
using TMPro;
using UnityEngine;

public class AdvanceTextAlphabet : AdvanceText
{
    public TextMeshProUGUI bigShownText; //holds the big text

    public override void Start()
    {
        base.Start();
        bigShownText.text = textArray[iterator]; //Display the first text
    }

    //Alphabet prompts do not depend on time, so we store all in prompts 1
    public override string[] PromptSelect(int selection)
    {
        return prompts.prompts1;
    }

    //Need to add the check for 4/6 right or abort
    protected override void TaskOnClick()
    {
        Debug.Log("Advance Text Base");
        base.TaskOnClick(); //increment to iterator is here
        Debug.Log("Advance Text Alphabet");
        int part1 = 6 - 1;
        if (iterator == part1) //ok, we've hit 6
        {
            int wrongos = 0;
            //if we can find 3+ incorrect answers, it's time to stop
            for (int index = 0; index < DataManager.individual_LNI.GetLength(0); index++)
            {
                Debug.Log("Wrongos Loop");
                if(DataManager.individual_LNI[index, DataManager.globalTime-1] == AdaptiveResponse.Incorrect)
                    wrongos++;
            }
            if (wrongos >= 3)
                complete = true;
        }
        Debug.Log("Alphabet Complete");
        bigShownText.text = textArray[iterator];
    }
}
