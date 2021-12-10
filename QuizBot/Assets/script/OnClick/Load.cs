using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Function responsible for loading user data
public class Load : MonoBehaviour
{
    // UI Elements
    public Button noBtn; // No Button in Panel
    public Button yesBtn; // Yes Button in Panel
    public Button loadButton; // Load Button in Panel
    public GameObject panel; // Panel
    public TextMeshProUGUI displayText; // Text display in Panel
    public TMP_InputField childIDField; // Value for childId

    // Non-UI Elements
    public string sceneName; // Name up upcoming scene after loading data
    public DataManager cleanup; //Saves data before loading
    public Validation_UserInfo validator;
    SaveLoad loader;

    void Awake()
    {
        loader = new SaveLoad();
        validator = new Validation_UserInfo();
    }

    // Use this for initialization
    void Start()
    {
        loadButton.onClick.AddListener(loadButtonClick);
        yesBtn.onClick.AddListener(yesButtonClick);
        noBtn.onClick.AddListener(noButtonClick);
    }

    // Occurs when next button is clicked
    void loadButtonClick()
    {
        bool showWarning;
        if (Validation_UserInfo.displayWarning.HasValue)
            showWarning = Validation_UserInfo.displayWarning.Value;
        else
            showWarning = validator.shouldDisplayWarning(childIDField);

        // Check if panel should be displayed (validator for panel)
        if (showWarning)
        {
            loadButton.interactable = false;
            yesBtn.gameObject.SetActive(true);
            noBtn.gameObject.SetActive(true);
            displayText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
        }
        else
        {
            yesBtn.gameObject.SetActive(false);
            noBtn.gameObject.SetActive(false);
            displayText.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
        }
    }

    // Occurs when yes button is clicked
    void yesButtonClick()
    {
        loadButton.interactable = true;
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        displayText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        loader.load(childIDField);

        // Load new scene
        cleanup.SceneCleanup();
        DataManager.currentScene = sceneName; //Updates DataManager scene string
        SceneManager.LoadScene(sceneName);
    }


    // Occurs when no button is clicked
    void noButtonClick()
    {
        loadButton.interactable = true;
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        displayText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }
}
