using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

// Responsible for exporting data into RedCap
public class ImportData  : MonoBehaviour
{
    public SaveLoad saveLoad;
    public Button clickedButton; //Button clicked

    private void Awake()
    {
        saveLoad = new SaveLoad();
    }

    void Start()
    {
        clickedButton.onClick.AddListener(() => ImportActions());
    }


    // Function that executes on button click and is responsible for importing data.
    // The aim is to develop this function for each scene (if import required)
    void ImportActions()
    {
        // If classroomId was not entered in the first scene, import is not allowed.
        if (DataManager.classroomId == String.Empty)
            Debug.Log("Classroom ID is missing, cannot import");
        else
        {
            // Prepare request for import.
            RedCapRequest redCapRequest = new RedCapRequest();
            redCapRequest.Token = "B345C5E9AFB7556F4627986E305D4F81";
            redCapRequest.Content = "record";
            redCapRequest.Action = "export";
            redCapRequest.Format = "json";
            redCapRequest.Type = "flat";
            redCapRequest.ReturnFormat = "json";
            redCapRequest.Fields0 = "record_id";
            redCapRequest.Form0 = "credentials";
            redCapRequest.FilterLogic = "[classroom_id]=" + "\"" + DataManager.classroomId + "\"";

            // Execute import request
            StartCoroutine(RedCapService.Instance.ImportAllData(usersDetails => GetAndSaveUserDetails(usersDetails),
                                                                        redCapRequest));
        }
    }

    // Function used to remove all records that cannot be saved (due to missing data points)
    void GetAndSaveUserDetails(UsersDetails usersDetails)
    {
        for (int index = usersDetails.users.Count - 1; index >= 0; index--)
        {
            Credential credential = usersDetails.users[index];
            if (credential.classroom_id == String.Empty || credential.child_id == String.Empty)
                usersDetails.users.RemoveAt(index);
        }
        
        if (usersDetails.users.Count > 0)
            saveLoad.SaveAll(usersDetails);
        else
            Debug.Log("No data to import in RedCap Database");
    }
}