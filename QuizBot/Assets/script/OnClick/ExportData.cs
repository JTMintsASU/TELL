using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

// Responsible for exporting data into RedCap
public class ExportData : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    public static string pdP; // persistentDataPath, this contains the local storage path
    public Button doneBtn; // Load Button in Panel
    public GameObject panel; // Panel
    public TMP_Text popUpText; // Value for childId
    void Start()
    {
        clickedButton.onClick.AddListener(() => ExportActions());
        doneBtn.onClick.AddListener(doneButtonClick);
        pdP = Application.persistentDataPath;
    }

    // Function that executes on button click and is responsible for exporting data.
    // The aim is to develop this function for each scene (if export required)
    void ExportActions()
    {
        // Preparing export request
        RedCapRequest outboundRequest = new RedCapRequest();
        outboundRequest.token = "B345C5E9AFB7556F4627986E305D4F81"; // This is Akshay's creds, to be replaced!
        outboundRequest.content = "record";
        outboundRequest.action = "import";
        outboundRequest.format = "json";
        outboundRequest.type = "flat";
        outboundRequest.overwriteBehavior = "overwrite";
        outboundRequest.returnContent = "ids";
        
        BinaryFormatter bf = new BinaryFormatter();
        DirectoryInfo directory = new DirectoryInfo(pdP);
        FileInfo[] filesInDirectory = directory.GetFiles("*.dat");

        // Iterating over all ".dat" files in directory
        foreach (FileInfo fileInDirectory in filesInDirectory)
        {
            string fileName = fileInDirectory.FullName;
            string[] splits = fileInDirectory.Name.Split('_');

            // Only pick files in directory that belong to same class
            if (splits.Length == 0 || splits[0] != DataManager.classroomId)
                continue;

            // Read data in file
            FileStream file = File.Open(fileName, FileMode.Open);
            SerialData serialData = (SerialData) bf.Deserialize(file);
            List<RedCapRecord> redCapRecords = RedCapRecord.convertToRedCapRecord(serialData);
            file.Close();

            string data = JsonConvert.SerializeObject(redCapRecords);
            //string data = JsonUtility.ToJson(redCapRecords);
            outboundRequest.forceAutoNumber = redCapRecords[0].recordID == int.MaxValue ? "true" : "false";
            outboundRequest.data = data;

            // Execute export request
            StartCoroutine(RedCapService.Instance.ExportCredentials(outboundRequest));
        }
        
        panel.gameObject.SetActive(true);
    }
    void doneButtonClick()
    {
        panel.gameObject.SetActive(false);
    }
}