//This class is used to store and load data.
//Useful for scene transitions, data exports, and local saves.
//By using static variables we keep a persistent location in memory.
//Note: Use doubles to store all numbers to avoid expensive casting
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    //User info
    //Note ID can be name or an ID #
    public static string teacherID;
    public static string assessorID;
    public static string childID;

    public static string currentScene; //used to determine what logic to use

    public static string globalGame; //Var used to store what game we are playing
    public static int globalTime; //Var used to store which 'Time'/unit/week we are on

    public static string childNameLNI; //used to store child name for use in LNI
    //PII THAT SHOULD NOT BE SAVED LONG-TERM

    //Per-game scored answers
    //These lists hold each answer's result
    public static List<bool> individual_expressive;
    public static List<bool> individual_receptive;
    public static List<int> individual_total;
    public static AdaptiveResponse[,] individual_LNI = new AdaptiveResponse[26,6]; //26 letters, 6 times

    //These hold the total score for the game
    public static double score_expressive;
    public static double score_receptive;
    public static double score_total;
    public static List<string> responses; //List of answers given
    

    //UserInfo Fields
    public TMP_InputField teacherNameField;
    public TMP_InputField assessorNameField;
    public TMP_InputField childNameField;
    public TMP_InputField teacherIDField;
    public TMP_InputField assessorIDField;
    public TMP_InputField childIDField;

    //Instructions Fields
    public TMP_InputField lniNameField;

    //Evaluator Fields
    public TMP_InputField responseField;
    public Toggle primaryToggle;
    public Toggle receptiveToggle; //Vocab

    //Grader Fields
    public AdvanceText promptCycler;
    public TextMeshProUGUI childText; //Displays child ID
    public TextMeshProUGUI[] promptText;
    public TextMeshProUGUI[] responsesText; //Displays child answers
    public TextMeshProUGUI[] expressiveText;
    public TextMeshProUGUI[] receptiveText;
    public TextMeshProUGUI expressiveTotalText;
    public TextMeshProUGUI receptiveTotalText;

    //RVocab Fields
    public TextMeshProUGUI[] expressivePercent;
    public TextMeshProUGUI[] receptivePercent;

    //RLI Fields
    public TextMeshProUGUI[] RLNI_letterText;

    //Long-Term Grades
    public static double vocabularyTotalQuestions; //How many vocab questions are asked per unit?
    public static double[] grade_vocabularyExpressive;
    public static double[] grade_vocabularyReceptive;
    public static double[] grade_vocabularyTotal;
    public static bool[] learnedLetterNames; //Tracks letters that we have 'tested out of'
    

    // Start is called before the first frame update
    void Start()
    {
        vocabularyTotalQuestions = 6;

        //Instantializes arrays if brand new
        if (grade_vocabularyExpressive == null)
        {
            grade_vocabularyExpressive = new double[6] { -1, -1, -1, -1, -1, -1 };
            grade_vocabularyReceptive = new double[6] { -1, -1, -1, -1, -1, -1 };
            grade_vocabularyTotal = new double[6] { -1, -1, -1, -1, -1, -1 };
            learnedLetterNames = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};
        }

        //Initializes currentScene
        if(currentScene == null)
            currentScene = "UserInfo";

        //Fill saved UserInfo
        if(currentScene == "UserInfo")
        {
            teacherIDField.text = teacherID;
            assessorIDField.text = assessorID;
            childIDField.text = childID;
        }

        //Reset scores and wipe responses
        if(currentScene == "Evaluator" || currentScene == "LNI_Evaluator")
        {
            individual_expressive = new List<bool>();
            individual_receptive = new List<bool>();
            individual_total = new List<int>();
            score_expressive = 0;
            score_receptive = 0;
            score_total = 0;
            responses = new List<string>();
        }

        //Display all our data!
        if (currentScene == "Grader")
        {
            childText.text = childID;
            string[] promptStorage = promptCycler.PromptSelect(globalTime);
            //Loop populates table textboxes, hardcoded at 6 due to issues reading unfully instantiated sizes
            for (int wheel = 0; wheel < 6; wheel++)
            {
                promptText[wheel].text = promptStorage[wheel];
                responsesText[wheel].text = responses[wheel];
                expressiveText[wheel].text = individual_expressive[wheel] ? "1" : "0";
                receptiveText[wheel].text = individual_receptive[wheel] ? "1" : "0";
            }
            //Access calculated total grades for this time
            //See https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings for formatting
            expressiveTotalText.text = grade_vocabularyExpressive[globalTime - 1].ToString("F0") + '%';
            receptiveTotalText.text = grade_vocabularyReceptive[globalTime - 1].ToString("F0") + '%';
        }

        if (currentScene == "LNI_Grader")
        {
            //Check for "Tested Out" Letters
            for (int letter = 0; letter < individual_LNI.GetLength(0); letter++)
            {
                int adaptiveCounter = 0; //Var used to track 'consecutive' correct answers

                for (int time = 0; time < individual_LNI.GetLength(1); time++)
                {
                    Debug.Log("grading loop");
                    //If score was correct or CSkipped, increase adaptive counter
                    if (individual_LNI[letter, time] == AdaptiveResponse.Correct ||
                    individual_LNI[letter, time] == AdaptiveResponse.CSKIP)
                    {
                        adaptiveCounter++;                        
                    }

                    //If incorrect, reset adaptive counter. Note that we don't count ISKIP
                    else if  (individual_LNI[letter, time] == AdaptiveResponse.Incorrect)
                    {
                        adaptiveCounter = 0;
                    }

                    if(adaptiveCounter>=2)
                    {
                        learnedLetterNames[letter] = true; //THIS IS OUR PROBLEM LINE--WHAT CHANGES WHEN TRUE
                        //there's no mechanic to take this back to false--test out once and you're good
                    }
                }
            }

            /*Populate answers and scores entered for this time
            string[] promptStorage = promptCycler.PromptSelect(globalTime);
            for (int wheel = 0; wheel < 6; wheel++)
            {
                promptText[wheel].text = promptStorage[wheel];
                responsesText[wheel].text = responses[wheel];
            }*/
        }

        //Report card - show all times
        if (currentScene == "RVocab")
        {
            childText.text = childID;
            //Loop populates grades textboxes, hardcoded at 6 due to issues reading unfully instantiated sizes
            for(int loop = 0; loop < 6; loop++) 
            {
                expressivePercent[loop].text = grade_vocabularyExpressive[loop].ToString("F0"); //Parameter ensures two decimal points
                receptivePercent[loop].text = grade_vocabularyReceptive[loop].ToString("F0");
            }
        }

        //RLI - show scores for each letter
        if (currentScene == "RLI")
        {
            //look for last good score
            for(int loop = 0; loop < learnedLetterNames.Length; loop++)
            {
                string result;
                if (learnedLetterNames[loop] == true)
                {
                    result = "<color=green>+</color>";
                }
                else result = "<color=red>-</color>";
                RLNI_letterText[loop].text = result;
            }
            //if none, zero
            //else track back and add, exit once tested out
            
             
        }
    }

    //This function can be called to grade a current question
    //and adjust scores accordingly
    public void GradeQuestion()
    {
        //Calculate scores based on toggles
        if (currentScene == "Evaluator")
        {
            if (primaryToggle.isOn)
            {
                individual_expressive.Add(true);
                individual_receptive.Add(true);
                individual_total.Add(2);
                score_expressive++;
                score_receptive++;
            }
            else if (receptiveToggle.isOn) //Only visible if expressive is off
            {
                individual_expressive.Add(false);
                individual_receptive.Add(true);
                individual_total.Add(1);
                score_receptive++;
            }
            else
            {
                individual_expressive.Add(false);
                individual_receptive.Add(false);
                individual_total.Add(0);
            }

            responses.Add(responseField.text);
        }

        if (currentScene == "LNI_Evaluator")
        {
            //Get prompt array for current time, get exact prompt we're on, convert from str to char to int to number of alphabet
            int charNum = (int)(char.Parse(promptCycler.PromptSelect(globalTime)[promptCycler.iterator])) - 65; //'A' ASCII int is 65   
            Debug.Log("Preparing to score letter at charNum " + charNum + "and globalTime " + globalTime);

            if (primaryToggle.isOn)
            {
                individual_LNI[charNum, globalTime-1] = AdaptiveResponse.Correct;
                responses.Add("<color=green>Correct</color>");
            }
            else
            {
                individual_LNI[charNum, globalTime-1] = AdaptiveResponse.Incorrect;
                responses.Add("<color=red>Incorrect</color>");
            }
        }
    }

    //General-purpose function
    //that can be called to store data before scene transitions
    public void SceneCleanup()
    {
        //Save UserInfo
        if(currentScene == "UserInfo")
        {
            //Save child name or ID, with ID taking precedence
            teacherID = teacherNameField.text;
            if (teacherIDField.text != "")
                teacherID = teacherIDField.text;
            assessorID = assessorNameField.text;
            if (assessorIDField.text != "")
                assessorID = assessorIDField.text;
            childID = childNameField.text;
            if(childIDField.text != "")
                childID = childIDField.text;
        }

        //Store child name for letter randomization
        if(currentScene == "LNI_Instructions")
        {
            childNameLNI = lniNameField.text;
        }

        //Grade final question and calculate results before moving on
        if (currentScene == "Evaluator")
        {
            GradeQuestion();
            int timeIndex = globalTime - 1; //Global Time starts at 1 instead of 0
            //*100 for percentile
            grade_vocabularyExpressive[timeIndex] = (score_expressive / vocabularyTotalQuestions) * 100;
            grade_vocabularyReceptive[timeIndex] = (score_receptive / vocabularyTotalQuestions) * 100;
            score_total = score_expressive + score_receptive;
            grade_vocabularyTotal[timeIndex] = (score_total / (vocabularyTotalQuestions * 2)) * 100;
        }
    }
}
