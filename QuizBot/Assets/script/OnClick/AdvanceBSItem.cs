
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class AdvanceBSItem: MonoBehaviour
{
    public TextMeshProUGUI shownText;
    public Toggle response_yes;
    public Toggle response_no;
    public Button clickedButton;
    public List<Sprite> sprites;
    public Image image;
    
    public int iterator; //Used to track our position in the array.
    public bool complete; //Indicates when iteration moves to last element, viewed by other scripts for scene transition logic
    public bool gradeMe; //Seperate bool tracks actual last element, used for data sync purposes with other scripts
    public static double pi = 3.1416;
    public List<int> recordedSolutions;

    public Validation_Games checker; //Used to check for valid answer before proceeding
    public Prompts_BS prompts; //Holds the list of prompts that the evaluator will be cycling through - select relevant child

    public static string sep = "  ";
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        complete = false;
        gradeMe = true;
        iterator = 0; //Selects the starting text to display
        recordedSolutions = new List<int>();
        shownText.text = prompts.promptsToDisplay[iterator].item + sep + prompts.promptsToDisplay[iterator].pronounce; //Display the first text
        clickedButton.onClick.AddListener(TaskOnClick);
        image.sprite = sprites[prompts.promptsToDisplay[iterator].index + 1];
        image.preserveAspect = true;
    }
    
    //Occurs when button is clicked
    protected virtual void TaskOnClick()
    {
        checker.Validator();
        if (checker.GetValidInput())
        {
            BSItem nextSelectedItem = null;
            if (iterator == 0)
            {
                nextSelectedItem = prompts.promptsToDisplay[prompts.promptsToDisplay.Count - 1];
                iterator++;
                shownText.text = nextSelectedItem.item + sep + prompts.promptsToDisplay[iterator].pronounce;; //Display the first text
                image.sprite = sprites[nextSelectedItem.index + 1];
            } 
            else 
            {
                Tuple<double, double> eapResults = getEAPEstimationScore();
                double eap_estimation_value = eapResults.Item1;
                double standard_error = eapResults.Item2;

                nextSelectedItem = pickNextItemRandomly(eap_estimation_value);

                if (prompts.promptsToDisplay.Count == 8 || standard_error <= 0.4)
                {
                    complete = true;
                }

                if (iterator == 8)
                {
                    DataManager.final_BSscores[DataManager.globalTime-1] = eapResults;
                }
                
                if (nextSelectedItem != null)
                {
                    prompts.promptsToDisplay.Add(nextSelectedItem);
                    prompts.universalItems.Remove(nextSelectedItem);
                    iterator++;
                    shownText.text = nextSelectedItem.item + sep + prompts.promptsToDisplay[iterator].pronounce;; //Display the first text
                    image.sprite = sprites[nextSelectedItem.index + 1];
                    image.preserveAspect = true;
                }
            }

        }
    }

    private Tuple<double, double> getEAPEstimationScore()
    {
        if (checker.GetValidInput())
        {
            if (response_no.isOn)
                this.recordedSolutions.Add(0);
            if (response_yes.isOn)
                this.recordedSolutions.Add(1);

            double D = 2.77112763740014;
            double q = 81.0;
            int array_size = (int) q;
            double[] x = new double[array_size];
            double[] g = new double[array_size];
            double[] l = new double[array_size];
            for (int i=1; i <= q; i++)
            {
                x[i-1] = (-4) + (i - 1) * (8 / (q - 1));
                g[i-1] = Math.Exp(-1 * Math.Pow(x[i-1], 2) / 2) / (Math.Sqrt(2 * pi));

                l[i-1] = 1;
                for (int j=1; j <= recordedSolutions.Count; j++)
                {
                    BSItem item = prompts.promptsToDisplay[j - 1];
                    double difficultyItem = item.difficulty;
                    int responseItem = recordedSolutions[j - 1];
                    double p = 1 / (1 + Math.Exp(-D * (x[i - 1] - difficultyItem)));
                    l[i - 1] = l[i - 1] * Math.Pow(p, responseItem) * Math.Pow((1-p), 1-responseItem);
                }
            }

            double denominator = 0D;
            double first_moment = 0D;
            double second_moment = 0D;
            for (int i = 1; i <= q; i++)
            {
                denominator += (l[i - 1] * g[i - 1]);
                first_moment += (x[i - 1] * l[i - 1] * g[i - 1]);
                second_moment += Math.Pow(x[i - 1], 2) * l[i - 1] * g[i - 1];
            }

            double eap_estimation_value = first_moment / denominator;
            double standard_error = Math.Sqrt((second_moment / denominator) - Math.Pow(eap_estimation_value, 2));

            if (eap_estimation_value < -3)
                eap_estimation_value = -3;
            if (eap_estimation_value > 3)
                eap_estimation_value = 3;

            return Tuple.Create(eap_estimation_value, standard_error);
        }

        return Tuple.Create(-999.999, -999.999);
    }

    private BSItem pickNextItemRandomly(double eap_estimation_value)
    {
        int randomnessThreshold = 4;
        Random random = new Random();
        List<Tuple<double, int>> absDifference = new List<Tuple<double, int>>();
        
        // Create a list of tuples where each tuple contains (absDiff, index)
        for (int index = 0; index < prompts.universalItems.Count; index++)
        {
            BSItem itemIndex = prompts.universalItems[index];
            double diff = Math.Abs(itemIndex.difficulty - eap_estimation_value);
            absDifference.Add(new Tuple<double, int>(diff, index));
        }
        
        // Sort the list and get smallest abs difference first
        absDifference.Sort();
        List<Tuple<double, int>> randomList = absDifference.GetRange(0, randomnessThreshold);
        Tuple<double, int> randomTuple = randomList[random.Next(randomList.Count)];

        return prompts.universalItems[randomTuple.Item2];
    }
}