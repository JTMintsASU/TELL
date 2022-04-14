//This script displays a parameter element when the object is clicked based on the game state.
//Used to display input validation text.
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvaluatorInputError : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    public TextMeshProUGUI displayText; //Text that might be displayed
    public Validation_Games checker; //Used to check game state

    void Start()
    {
        //Create listener for the button in question
        clickedButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Evaluator Input Error");
        //Make text opaque based on condition
        checker.Validator();
        if (!checker.GetValidInput()) //if input is invalid
            displayText.alpha = 255;
        //Hide if condition is no longer met
        else
            displayText.alpha = 0;
    }
}