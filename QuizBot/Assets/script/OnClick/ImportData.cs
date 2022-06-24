using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

// Responsible for importing data into RedCap
public class ImportData  : MonoBehaviour
{
    public SaveLoad saveLoad;
    public Button clickedButton; //Button clicked
    public Button doneBtn; // Load Button in Panel
    public GameObject panel; // Panel
    public TMP_Text popUpText; // Value for childId

    void Start()
    {
        saveLoad = new SaveLoad();
        clickedButton.onClick.AddListener(() => StartCoroutine(ImportActions()));
        doneBtn.onClick.AddListener(doneButtonClick);
    }


    // Function that is responsible for importing data into RedCap, triggered after button click.
    IEnumerator ImportActions()
    {
        // If classroomId was not entered in the first scene, import is not allowed.
        if (DataManager.classroomID == String.Empty)
            Debug.Log("Classroom ID is missing, cannot import");
        else
        {
            List<int> recordIDsToImport = new List<int>();
            
            // Two step process:
            // 1. First, get all record_id corresponding to the classroom_id filter in hand
            // 2. Second, get all records with the corresponding record_id and store locally.
            
            // 1. Getting all record_id corresponding to the classroom_id filter in hand
            RedCapRequest redCapRequestForRecordIDs = new RedCapRequest();
            redCapRequestForRecordIDs.token = "B345C5E9AFB7556F4627986E305D4F81"; // This is Akshay's creds, to be replaced!
            redCapRequestForRecordIDs.content = "record";
            redCapRequestForRecordIDs.action = "export";
            redCapRequestForRecordIDs.format = "json";
            redCapRequestForRecordIDs.type = "flat";
            redCapRequestForRecordIDs.returnFormat = "json";
            redCapRequestForRecordIDs.filterLogic = "[classroom_id]=" + "\"" + DataManager.classroomID + "\"";

            // Execute import request
            yield return StartCoroutine(
                RedCapService.Instance.ImportAllData(usersDetails => GetRecordIDs(usersDetails, recordIDsToImport),
                                                            redCapRequestForRecordIDs));

            // 2. Getting all records with the corresponding record_id and store locally.
            RedCapRequest redCapRequestForRecords = new RedCapRequest();
            redCapRequestForRecords.token = "B345C5E9AFB7556F4627986E305D4F81"; // This is Akshay's creds, to be replaced!
            redCapRequestForRecords.content = "record";
            redCapRequestForRecords.action = "export";
            redCapRequestForRecords.format = "json";
            redCapRequestForRecords.type = "flat";
            redCapRequestForRecords.returnFormat = "json";
            foreach (var recordId in recordIDsToImport)
            {
                redCapRequestForRecords.records_0 = recordId;

                // Execute import request
                StartCoroutine(
                    RedCapService.Instance.ImportAllData(usersDetails => GetAndSaveRecords(usersDetails), 
                            redCapRequestForRecords));
                
                
            }
            
            panel.gameObject.SetActive(true);
        }
    }
    
    // Function responsible for returning all valid record_ids to save
    void GetRecordIDs(UsersDetails usersDetails, List<int> recordIDsToImport)
    {
        for (int index = usersDetails.users.Count - 1; index >= 0; index--)
        {
            RedCapRecord redCapRecord = usersDetails.users[index];
            if (redCapRecord.recordID == null || redCapRecord.recordID == 0)
            {
                usersDetails.users.RemoveAt(index);
                Debug.Log("Record obtained from RedCap is corrupted, missing record_id");
            }
            else
            {
                recordIDsToImport.Add(redCapRecord.recordID.Value);
            }
        }
    }

    // Function is responsible for preprocessing data obtained from RedCap and storing it locally.
    // 1. Records obtained from RedCap should contain classroomId and childId. If any of these are missing, record
    // is not stored locally.
    // 2. Once eligible records are obtained, they are stored locally.
    void GetAndSaveRecords(UsersDetails usersDetails)
    {
        if (isRecordValid(usersDetails) == false)
            return;
        
        if (usersDetails.users.Count > 0)
        {
            saveLoad.Save(usersDetails);

            //Save teach and assessor since they'll be overwritten
            string tempAssess = DataManager.assessorID;
            string tempTeach = DataManager.teacherID;

            //Reload after importing so we have updated data if file already exists
            saveLoad.Load(DataManager.childID, DataManager.classroomID);

            //reset assessor and teacher
            DataManager.assessorID = tempAssess;
            DataManager.teacherID = tempTeach;
        }
    }


    // Function is responsible for returning if RedCap record is valid to save locally
    bool isRecordValid(UsersDetails usersDetails)
    {
        bool isValid = false;
        for (int index = usersDetails.users.Count - 1; index >= 0; index--)
        {
            RedCapRecord redCapRecord = usersDetails.users[index];
            if (redCapRecord != null &&
                string.IsNullOrEmpty(redCapRecord.classroomID) == false &&
                string.IsNullOrEmpty(redCapRecord.childID) == false)
                isValid = true;
        }

        return isValid;
    }
    
    
    void doneButtonClick()
    {
        panel.gameObject.SetActive(false);
    }
}