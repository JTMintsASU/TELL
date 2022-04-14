//This script is used to save the game
//selected when a button is clicked based on
//that button's name.
using UnityEngine;
using UnityEngine.UI;

public class SetGame : MonoBehaviour
{
    public Button gameButton;

    // Start is called before the first frame update
    void Start()
    {
        //Create listener for the button in question
        gameButton.onClick.AddListener(SetOnClick);
    }

    // Update is called once per frame
    void SetOnClick()
    {
        DataManager.globalGame = SaveGame(gameButton.name);
    }
    public static string SaveGame(string btnGame) => btnGame switch
    {
        "buttonVocabulary" => "Instructions",
        "buttonLNI" => "LNI_Instructions",
        _ => "Error: No game found for button!" //Default 'error' case
    };
}
