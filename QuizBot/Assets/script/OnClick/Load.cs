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
    public Button loadBtn; // Load Button in Panel
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
    }

    // Use this for initialization
    void Start()
    {
        loadBtn.onClick.AddListener(loadButtonClick);
        yesBtn.onClick.AddListener(yesButtonClick);
        noBtn.onClick.AddListener(noButtonClick);
    }

    // Occurs when next button is clicked
    void loadButtonClick()
    {
        bool showWarning;
        if (validator.displayWarning.HasValue)
            showWarning = validator.displayWarning.Value;
        else
            showWarning = validator.shouldDisplayWarning(childIDField);

        // Check if panel should be displayed (validator for panel)
        if (showWarning)
        {
            loadBtn.interactable = false;
            panel.gameObject.SetActive(true);
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }

    // Occurs when yes button is clicked
    void yesButtonClick()
    {
        loadBtn.interactable = true;
        panel.gameObject.SetActive(false);
        loader.Load(childIDField);

        // Load new scene
        cleanup.SceneCleanup();
        DataManager.currentScene = sceneName; //Updates DataManager scene string
        SceneManager.LoadScene(sceneName);
    }


    // Occurs when no button is clicked
    void noButtonClick()
    {
        loadBtn.interactable = true;
        panel.gameObject.SetActive(false);
    }
}