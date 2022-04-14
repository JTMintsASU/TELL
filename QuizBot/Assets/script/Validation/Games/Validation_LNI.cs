using UnityEngine;
using UnityEngine.UI;

//Validation_LNI checks to see if Evaluator input is valid
public class Validation_LNI : Validation_Games
{
    //Takes the required toggle inputs from the evaluator
    public Toggle response_yes;
    public Toggle response_no;
    public AdvanceText prompts;
    public bool validScene; //Used for checks to advance scene
    public bool validInput; //Used for checks to advance prompts

    //Check the input
    public override bool Validator()
    {
        validScene = true;
        validInput = true;

        //Do not advance scene until prompts are done
        if (!prompts.complete)
            validScene = false;

        //Invalid if neither response is ticked
        if (!response_yes.isOn && !response_no.isOn)
        {
            validScene = false;
            validInput = false;
        }

        if (validScene)
            Debug.Log("Scene valid for leaving.");
        return validScene;
    }

    public override bool GetValidInput()
    {
        return validInput;
    }
}
