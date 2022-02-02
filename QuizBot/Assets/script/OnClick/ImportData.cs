using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ImportData  : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    void Start()
    {
        clickedButton.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        StartCoroutine(RedCapService.Instance.ImportData(GetUserDetails,
            "B345C5E9AFB7556F4627986E305D4F81",
            "record",
            "export",
            "json",
            "flat",
            "json", null));
                                                                //"game,time,teacher_id"));
    }

    void GetUserDetails(UsersDetails usersDetails)
    {
        Debug.Log("Print Time");
        foreach (UserDetail userDetail in usersDetails.users)
        {
            Debug.Log("Assessor ID - " + userDetail.assessor_id + "\n" + 
                      "Game - " + userDetail.game + "\n" + 
                      "Record ID - " + userDetail.record_id + "\n" + 
                      "Score - " + userDetail.score + "\n" + 
                      "Teacher ID - " + userDetail.teacher_id + "\n" + 
                      "Time - " + userDetail.time);
        }
    }
}