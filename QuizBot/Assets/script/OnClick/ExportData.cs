using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

// Responsible for exporting data into RedCap
public class ExportData : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    public static string pdP;
    void Start()
    {
        clickedButton.onClick.AddListener(() => ExportActions());
        pdP = Application.persistentDataPath;
    }

    // Function that executes on button click and is responsible for exporting data.
    // The aim is to develop this function for each scene (if export required)
    void ExportActions()
    {
        // Preparing export request
        RedCapRequest redCapRequest = new RedCapRequest();
        redCapRequest.Token = "B345C5E9AFB7556F4627986E305D4F81";
        redCapRequest.Content = "record";
        redCapRequest.Action = "import";
        redCapRequest.Format = "json";
        redCapRequest.Type = "flat";
        redCapRequest.OverwriteBehavior = "overwrite";
        redCapRequest.ReturnContent = "ids";
        
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
            Credential credential = Credential.fromSerialData(serialData);
            file.Close();

            //string data = JsonConvert.SerializeObject(userDetails.users);
            string data = JsonUtility.ToJson(credential);
            redCapRequest.ForceAutoNumber = credential.record_id == int.MaxValue ? "true" : "false";
            redCapRequest.Data = "[" + data + "]";

            // Execute export request
            StartCoroutine(RedCapService.Instance.ExportCredentials(redCapRequest));
        }
    }
}