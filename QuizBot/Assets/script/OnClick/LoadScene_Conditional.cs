//This class loads a scene when a button is clicked
//if the given Validator is true
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene_Conditional : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    public string sceneName; //Name of the scene to load
    public Validation_Parent checker; //Used to check input
    public DataManager cleanup; //Saves data before loading

    void Start()
    {
        //Create listener for the button in question
        clickedButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Saves data and loads next string
        if(checker.Validator()) //if input is invalid
        {
            cleanup.SceneCleanup();
            DataManager.currentScene = sceneName; //Updates DataManager scene string
            SceneManager.LoadScene(sceneName);
        }
    }
}
