//This class is used to exit the app when a button is clicked
using UnityEngine;
using UnityEngine.UI;

public class GradeQuestion : MonoBehaviour
{
    public Button gradeButton; //Button clicked
    public DataManager gradeData;
    public AdvanceText gradedQuestions;
    public AdvanceBSItem gradedQuestionsBS;
    public Validation_Games checker; //Used to check for valid answer before proceeding

    void Start()
    {
        //Create listener for the button in question
        gradeButton.onClick.AddListener(GradeOnClick);
    }

    void GradeOnClick()
    {
        //Prevents incrementing during final question
        //Due to uncertain execution order, we let the data manager
        //grade the final question in its class
        checker.Validator();
        if (checker.GetValidInput())
        {
            if (DataManager.globalGame == "BS_Instructions" && gradedQuestionsBS.gradeMe)
                gradeData.GradeQuestion();
            else if (gradedQuestions.gradeMe)
                gradeData.GradeQuestion();
        }
    }
}
