//This class is used to advance the displayed text
//from a given text array. Used to cycle prompts.
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvanceText : MonoBehaviour
{
    public TextMeshProUGUI shownText;
    int localTime; //Tracks which "Time"/Week/Unit to use for prompt
    public string[] textArray; //Holds answer prompts at top
    public int iterator; //Used to track our position in the array.
    public Button clickedButton;
    public bool complete; //Indicates when iteration moves to last element, viewed by other scripts for scene transition logic
    public bool gradeMe; //Seperate bool tracks actual last element, used for data sync purposes with other scripts
    public Validation_Games checker; //Used to check for valid answer before proceeding
    public Array_Prompts prompts; //Holds the list of prompts that the evaluator will be cycling through - select relevant child

    // Start is called before the first frame update
    void Start()
    {
        localTime = DataManager.globalTime;
        textArray = promptSelect(localTime);
        complete = false;
        gradeMe = true;
        iterator = 0; //Selects the starting text to display
        shownText.text = textArray[iterator]; //Display the first text
        clickedButton.onClick.AddListener(TaskOnClick);
    }

    //This function uses an int to select a prompt from Array_Prompts child.
    //The default case "prompts" contains an error message array.
    public string[] promptSelect(int selection) => selection switch
    {
        1 => prompts.prompts1,
        2 => prompts.prompts2,
        3 => prompts.prompts3,
        4 => prompts.prompts4,
        5 => prompts.prompts5,
        6 => prompts.prompts6,
        _ => Array_Prompts.prompts
    };

    //Occurs when button is clicked
    void TaskOnClick()
    {
        checker.Validator();
        if (checker.GetValidInput())
        {
            if (iterator < textArray.Length - 1)
            {
                iterator++;
                shownText.text = textArray[iterator];

                //On last question display, mark completed
                if (iterator == textArray.Length - 1)
                    complete = true;
            }
            //Last element
            else
                gradeMe = false;
        }
    }
}
