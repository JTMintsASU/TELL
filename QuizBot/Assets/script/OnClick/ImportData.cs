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
                                                                "json", 
                                                                "game,time,teacher_id"));
    }

    void GetUserDetails(UsersDetails usersDetails)
    {
        Debug.Log("Print Time");
        foreach (UserDetail userDetail in usersDetails.users)
        {
            Debug.Log("Assessor ID - " + userDetail.assessor_id);
            Debug.Log("Game - " + userDetail.game);
            Debug.Log("Record ID - " + userDetail.record_id);
            Debug.Log("Score - " + userDetail.score);
            Debug.Log("Teacher ID - " + userDetail.teacher_id);
            Debug.Log("Time - " + userDetail.time);
        }
    }
}