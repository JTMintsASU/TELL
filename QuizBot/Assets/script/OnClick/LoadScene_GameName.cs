//This class loads a scene when a button is clicked
//if the given Validator is true
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene_GameName : LoadScene_Conditional
{
    void Start()
    {
        sceneName = DataManager.globalGame;

        //Create listener for the button in question
        clickedButton.onClick.AddListener(this.TaskOnClick);
        
    }
}
