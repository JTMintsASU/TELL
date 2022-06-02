//This class is used to store and load data.
//Useful for scene transitions, data exports, and local saves.
//By using static variables we keep a persistent location in memory.
//Note: Use doubles to store all numbers to avoid expensive casting

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    //User info
    //Note ID can be name or an ID #
    public static string recordID;
    public static string teacherID;
    public static string assessorID;
    public static string childID;
    public static string classroomID;

    public static string currentScene; //used to determine what logic to use

    public static string globalGame; //Var used to store what game we are playing
    public static int globalTime; //Var used to store which 'Time'/unit/week we are on

    // Student should answer 3 of these alphabets to skip these alphabets
    public static string[] exceptionalAdaptCharactersLSI = new[] { "A", "B", "E", "O", "S", "U" }; 
    public static string[] exceptionalTwoStageCharactersLSI = new[] { "A", "B", "M", "P", "S", "T" }; 
    
    public static string childNameLNI; //used to store child name for use in LNI
                                       //PII THAT SHOULD NOT BE SAVED LONG-TERM
    
                                       

    //Per-game scored answers
    //LNI Grades
    public static bool[] learnedLetterNamesLNI; //Tracks letters that we have 'tested out of'
    public static AdaptiveResponse[,] individual_LNI; //26 letters, 6 times

    //Per-game scored answers
    //LSI Grades
    public static bool[] learnedLetterNamesLSI; //Tracks letters that we have 'tested out of'
    public static AdaptiveResponse[,] individual_LSI; //26 letters, 6 times
    
    //Per-game scored answers
    //BS Grades
    public static AdaptiveResponse[,] individual_BS;
    public static string[,] individual_BSChildResponse;

    //Vocab Grades
    //These hold the total score for the game
    public static double score_expressive;
    public static double score_receptive;
    public static double score_total;
    public static List<string> responses; //List of answers given

    public static List<bool> individual_expressive;
    public static List<bool> individual_receptive;
    public static List<bool> individual_expressiveFlag;
    public static List<bool> individual_receptiveFlag;
    public static List<int> individual_total;

    public static double vocabularyTotalQuestions; //How many vocab questions are asked per unit?
    public static double[] grade_vocabularyExpressive;
    public static double[] grade_vocabularyReceptive;
    public static double[] grade_vocabularyTotal;

    public static List<List<bool>> individual_vocabularyExpressive;
    public static List<List<bool>> individual_vocabularyReceptive;
    public static List<List<string>> individual_vocabularyResponses;
    public static List<List<bool>> individual_vocabularyExpressiveFlag;
    public static List<List<bool>> individual_vocabularyReceptiveFlag;

    //UserInfo Fields
    public TMP_InputField teacherNameField;
    public TMP_InputField assessorNameField;
    public TMP_InputField childNameField;
    public TMP_InputField classroomField;
    public TMP_InputField teacherIDField;
    public TMP_InputField assessorIDField;
    public TMP_InputField childIDField;
    public TMP_InputField classroomIDField;

    //Instructions Fields
    public TMP_InputField lniNameField;
    
    //BS Child Response Field
    public TMP_InputField bsChildResponseField;

    //Evaluator Fields
    public TMP_InputField responseField;
    public Toggle primaryToggle;
    public Toggle receptiveToggle; //Vocab
    public Toggle expressiveToggle;
    public Toggle expressiveFlag;
    public Toggle receptiveFlag;

    //Grader Fields
    public AdvanceText promptCycler;
    public AdvanceBSItem promptCyclerBS;
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
    public TextMeshProUGUI[] RLSI_letterText;

    //RLI Fields
    public TextMeshProUGUI[] BS_items;
    public TextMeshProUGUI[] BS_letterText;

    

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

            individual_vocabularyExpressive = new List<List<bool>>();
            individual_vocabularyExpressiveFlag = new List<List<bool>>();
            individual_vocabularyReceptive = new List<List<bool>>();
            individual_vocabularyReceptiveFlag = new List<List<bool>>();
            individual_vocabularyResponses = new List<List<string>>();

            learnedLetterNamesLNI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};

            learnedLetterNamesLSI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
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
            classroomIDField.text = classroomID;
            
            // Add logout code here
            grade_vocabularyExpressive = new double[6] { -1, -1, -1, -1, -1, -1 };
            grade_vocabularyReceptive = new double[6] { -1, -1, -1, -1, -1, -1 };
            grade_vocabularyTotal = new double[6] { -1, -1, -1, -1, -1, -1 };
            individual_vocabularyExpressive = new List<List<bool>>();
            individual_vocabularyExpressiveFlag = new List<List<bool>>();
            individual_vocabularyReceptive = new List<List<bool>>();
            individual_vocabularyReceptiveFlag = new List<List<bool>>();
            individual_vocabularyResponses = new List<List<string>>();
            individual_LNI =  new AdaptiveResponse[26, 6];
            individual_LSI =  new AdaptiveResponse[26, 6];
            individual_BS =  new AdaptiveResponse[36, 6];
            individual_BSChildResponse =  new string[36, 6];

            learnedLetterNamesLNI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};

            learnedLetterNamesLSI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};
        }

        //Reset scores and wipe responses
        if(currentScene == "Evaluator" || currentScene == "LNI_Evaluator" || 
           currentScene == "LSI_Evaluator" || currentScene == "BS_Evaluator")
        {
            individual_expressive = new List<bool>();
            individual_expressiveFlag = new List<bool>();
            individual_receptive = new List<bool>();
            individual_receptiveFlag = new List<bool>();
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

        if (currentScene == "LNI_Grader" )
        {
            //Check for "Tested Out" Letters
            for (int letter = 0; letter < individual_LNI.GetLength(0); letter++)
            {
                int adaptiveCounter = 0; //Var used to track 'consecutive' correct answers

                for (int time = 0; time < individual_LNI.GetLength(1); time++)
                {
                    if (time == globalTime-1)
                    {
                        if (individual_LNI[letter, time] == AdaptiveResponse.Correct ||
                            individual_LNI[letter, time] == AdaptiveResponse.CSKIP)
                        {
                            RLNI_letterText[letter].text = "<color=green>+</color>";
                        }
                        else
                        {
                            RLNI_letterText[letter].text = "<color=red>-</color>";
                        }
                    }
                    
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
                        learnedLetterNamesLNI[letter] = true; //THIS IS OUR PROBLEM LINE--WHAT CHANGES WHEN TRUE
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

        if (currentScene == "LSI_Grader" )
        {
            //Check for "Tested Out" Letters
            for (int letterIndex = 0; letterIndex < individual_LSI.GetLength(0); letterIndex++)
            {
                string letter = ((char)(letterIndex + 65)).ToString(); //65 is code for 'A'
                int adaptiveCounter = 0; //Var used to track 'consecutive' correct answers

                for (int time = 0; time < individual_LSI.GetLength(1); time++)
                {
                    if (time == globalTime-1)
                    {
                        if (individual_LSI[letterIndex, time] == AdaptiveResponse.Correct ||
                            individual_LSI[letterIndex, time] == AdaptiveResponse.CSKIP)
                        {
                            RLSI_letterText[letterIndex].text = "<color=green>+</color>";
                        }
                        else
                        {
                            RLSI_letterText[letterIndex].text = "<color=red>-</color>";
                        }
                    }
                    
                    //If score was correct or CSkipped, increase adaptive counter
                    if (individual_LSI[letterIndex, time] == AdaptiveResponse.Correct ||
                        individual_LSI[letterIndex, time] == AdaptiveResponse.CSKIP)
                    {
                        adaptiveCounter++;
                    }

                    //If incorrect, reset adaptive counter. Note that we don't count ISKIP
                    else if  (individual_LSI[letterIndex, time] == AdaptiveResponse.Incorrect)
                    {
                        adaptiveCounter = 0;
                    }
                }

                if (exceptionalAdaptCharactersLSI.Contains(letter))
                {
                    if(adaptiveCounter>=3)
                    {
                        learnedLetterNamesLSI[letterIndex] = true; //THIS IS OUR PROBLEM LINE--WHAT CHANGES WHEN TRUE
                        //there's no mechanic to take this back to false--test out once and you're good
                    }
                }
                else
                {
                    if(adaptiveCounter>=2)
                    {
                        learnedLetterNamesLSI[letterIndex] = true; //THIS IS OUR PROBLEM LINE--WHAT CHANGES WHEN TRUE
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
        
        if (currentScene == "BS_Grader" )
        {
            List<BSItem> universalItems;
            string readPath = Path.Combine(Application.dataPath, Prompts_BS.configurationFilePath);
            using (StreamReader r = new StreamReader(readPath))
            {
                string json = r.ReadToEnd();
                universalItems = JsonConvert.DeserializeObject<List<BSItem>>(json);
            }
            
            //Check for "Tested Out" Letters
            int iterator = 0;
            for (int item = 0; item < individual_BS.GetLength(0); item++)
            {
                for (int time = 0; time < individual_BS.GetLength(1); time++)
                {
                    if (time == globalTime-1)
                    {
                        if (individual_BS[item, time] == AdaptiveResponse.Correct)
                        {
                            BS_items[iterator].text = universalItems.Find(bsItem => bsItem.index.Equals(item)).item;
                            BS_letterText[iterator].text = "<color=green>+</color>";
                            iterator++;
                        }
                        else if (individual_BS[item, time] == AdaptiveResponse.Incorrect)
                        {
                            BS_items[iterator].text = universalItems.Find(bsItem => bsItem.index.Equals(item)).item;
                            BS_letterText[iterator].text = "<color=red>-</color>";
                            iterator++;
                        }
                    }
                }
            }
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
            for(int loop = 0; loop < learnedLetterNamesLNI.Length; loop++)
            {
                string result;
                if (learnedLetterNamesLNI[loop] == true)
                {
                    result = "<color=green>+</color>";
                }
                else result = "<color=red>-</color>";
                RLNI_letterText[loop].text = result;
            }

            for(int loop = 0; loop < learnedLetterNamesLSI.Length; loop++)
            {
                string result;
                if (learnedLetterNamesLSI[loop] == true)
                {
                    result = "<color=green>+</color>";
                }
                else result = "<color=red>-</color>";
                RLSI_letterText[loop].text = result;
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
            
            if (expressiveFlag.isOn)
                individual_expressiveFlag.Add(true);
            else
                individual_expressiveFlag.Add(false);
            
            if (receptiveFlag.isOn)
                individual_receptiveFlag.Add(true);
            else
                individual_receptiveFlag.Add(false);

            responses.Add(responseField.text);
        }

        if (currentScene == "LNI_Evaluator")
        {
            //Get prompt array for current time, get exact prompt we're on, convert from str to char to int to number of alphabet
            int charNum = (int)(char.Parse(promptCycler.PromptSelect(globalTime)[promptCycler.iterator])) - 65; //'A' ASCII int is 65   

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

        if (currentScene == "LSI_Evaluator")
        {
            //Get prompt array for current time, get exact prompt we're on, convert from str to char to int to number of alphabet
            int charNum = (int)(char.Parse(promptCycler.PromptSelect(globalTime)[promptCycler.iterator])) - 65; //'A' ASCII int is 65   

            if (primaryToggle.isOn)
            {
                individual_LSI[charNum, globalTime-1] = AdaptiveResponse.Correct;
                responses.Add("<color=green>Correct</color>");
            }
            else
            {
                individual_LSI[charNum, globalTime-1] = AdaptiveResponse.Incorrect;
                responses.Add("<color=red>Incorrect</color>");
            }
        }

        if (currentScene == "BS_Evaluator")
        {
            BSItem itemToGrade = promptCyclerBS.prompts.promptsToDisplay[promptCyclerBS.iterator];
            if (primaryToggle.isOn)
            {
                individual_BS[itemToGrade.index, globalTime-1] = AdaptiveResponse.Correct;
            }
            else
            {
                individual_BS[itemToGrade.index, globalTime-1] = AdaptiveResponse.Incorrect;
            }
            if (bsChildResponseField.text != String.Empty)
                individual_BSChildResponse[itemToGrade.index, globalTime-1] = bsChildResponseField.text;
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
            classroomID = classroomField.text;
            if(classroomIDField.text != "")
                classroomID = classroomIDField.text;
            
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
            
            // Add individual answers
            individual_vocabularyExpressive.Insert(timeIndex, individual_expressive);
            individual_vocabularyReceptive.Insert(timeIndex, individual_receptive);
            individual_vocabularyExpressiveFlag.Insert(timeIndex, individual_expressiveFlag);
            individual_vocabularyReceptiveFlag.Insert(timeIndex, individual_receptiveFlag);
            individual_vocabularyResponses.Insert(timeIndex, responses);
        }
    }
}
