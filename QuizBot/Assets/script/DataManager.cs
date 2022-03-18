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
    public TextMeshProUGUI lniSkippedText;

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
    public TextMeshProUGUI RLNI_A;
    public TextMeshProUGUI RLNI_B;
    public TextMeshProUGUI RLNI_C;
    public TextMeshProUGUI RLNI_D;
    public TextMeshProUGUI RLNI_E;
    public TextMeshProUGUI RLNI_F;
    public TextMeshProUGUI RLNI_G;
    public TextMeshProUGUI RLNI_H;
    public TextMeshProUGUI RLNI_I;
    public TextMeshProUGUI RLNI_J;
    public TextMeshProUGUI RLNI_K;
    public TextMeshProUGUI RLNI_L;
    public TextMeshProUGUI RLNI_M;
    public TextMeshProUGUI RLNI_N;
    public TextMeshProUGUI RLNI_O;
    public TextMeshProUGUI RLNI_P;
    public TextMeshProUGUI RLNI_Q;
    public TextMeshProUGUI RLNI_R;
    public TextMeshProUGUI RLNI_S;
    public TextMeshProUGUI RLNI_T;
    public TextMeshProUGUI RLNI_U;
    public TextMeshProUGUI RLNI_V;
    public TextMeshProUGUI RLNI_W;
    public TextMeshProUGUI RLNI_X;
    public TextMeshProUGUI RLNI_Y;
    public TextMeshProUGUI RLNI_Z;

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

        //Check for 'tested out' letters
        if(currentScene == "LNI_Instructions")
        {
            for (int letter = 0; letter < individual_LNI.GetLength(0); letter++)
            {
                for (int time = 0; time < individual_LNI.GetLength(1); time++)
                {
                    int matchStart = time - 2;
                    if (matchStart >= 0) //must have at least two records to check
                    {
                        //See if last two records were correct
                        if (individual_LNI[letter, matchStart] == AdaptiveResponse.Correct &&
                        individual_LNI[letter, matchStart+1] == AdaptiveResponse.Correct)
                        {
                            learnedLetterNames[letter] = true;
                            lniSkippedText.text += " " + ((char)(letter + 65)).ToString(); //65 is code for 'A'
                        }
                    }
                }
            
            }
        }

        //Reset scores and wipe responses
        if(currentScene == "Evaluator")
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
            string[] promptStorage = promptCycler.promptSelect(globalTime);
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
            childText.text = childID;
            string arrayText = "";
            foreach (AdaptiveResponse response in individual_LNI)
            {
                arrayText += response.ToString();
            }
            responsesText[0].text = arrayText;
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
            int scoreTotal = 0;
            for(int loop = 0; loop < individual_LNI.GetLength(1); loop++)
            {
                AdaptiveResponse x = individual_LNI[0, loop];
                if (x == AdaptiveResponse.Correct || x == AdaptiveResponse.Skipped)
                    scoreTotal++;
            }
            //if none, zero
            //else track back and add, exit once tested out
            
             RLNI_A.text = scoreTotal.ToString();
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
            int charNum = (int)(char.Parse(promptCycler.promptSelect(globalTime)[promptCycler.iterator])) - 65; //'A' ASCII int is 65   

            if (primaryToggle.isOn)
            {
                individual_LNI[charNum, globalTime-1] = AdaptiveResponse.Correct;
            }
            else
            {
                individual_LNI[charNum, globalTime-1] = AdaptiveResponse.Incorrect;
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
