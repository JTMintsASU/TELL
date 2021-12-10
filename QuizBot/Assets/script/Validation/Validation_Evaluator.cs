using UnityEngine;
using UnityEngine.UI;

//Validation_Evaluator checks to see if Evaluator input is valid
public class Validation_Evaluator : Validation_Parent
{
    //Takes the required toggle inputs from the evaluator
    public Toggle expressive_yes;
    public Toggle expressive_no;
    public Toggle receptive_yes;
    public Toggle receptive_no;
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

        //Invalid if neither expressive is ticked
        if (!expressive_yes.isOn && !expressive_no.isOn)
        {
            validScene = false;
            validInput = false;
        }

        //Invalid if expressive no is ticked AND 
        //neither receptive is ticked
        if (expressive_no.isOn && !receptive_yes.isOn && !receptive_no.isOn)
        {
            validScene = false;
            validInput = false;
        }

        return validScene;
    }

    public bool GetValidInput()
    {
        return validInput;
    }

    public override bool Validate_Input()
    {
        throw new System.NotImplementedException();
    }
}
