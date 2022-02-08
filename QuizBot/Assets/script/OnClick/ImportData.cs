using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

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
        clickedButton.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        StartCoroutine(RedCapService.Instance.ImportAllData(usersDetails => GetAndSaveUserDetails(usersDetails),
            "B345C5E9AFB7556F4627986E305D4F81",
            "record",
            "export",
            "json",
            "flat",
            "json", null));
            //"game,time,teacher_id"));
    }

    // Function used to remove all records that cannot be saved (due to missing data points)
    void GetAndSaveUserDetails(UsersDetails usersDetails)
    {
        for (int index = usersDetails.users.Count - 1; index >= 0; index--)
        {
            UserDetail userDetail = usersDetails.users[index];
            if (userDetail.classroom_id == String.Empty || userDetail.child_id == String.Empty)
                usersDetails.users.RemoveAt(index);
        }
        
        saveLoad.SaveAll(usersDetails);
    }
}