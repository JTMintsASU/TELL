using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ExportData : MonoBehaviour
{
    public Button clickedButton; //Button clicked
    void Start()
    {
        clickedButton.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        // Debug.Log("checking button click");
        StartCoroutine(Export());
    }
    
    
    IEnumerator Export()
    {
        string teacherId = DataManager.teacherID;
        string assessorId = DataManager.assessorID;

        UserDetails userDetails = new UserDetails();
        userDetails.record_id = "record_id";
        userDetails.teacher_id = teacherId;
        userDetails.assessor_id = assessorId;
        
        string data = JsonUtility.ToJson(userDetails);
        
        
        WWWForm form = new WWWForm();
        form.AddField("token", "B345C5E9AFB7556F4627986E305D4F81");
        form.AddField("content", "record");
        form.AddField("action", "import");
        form.AddField("format", "json");
        form.AddField("type", "flat");
        form.AddField("overwriteBehavior", "normal");
        form.AddField("forceAutoNumber", "false");
        form.AddField("data","[" + data + "]");
        form.AddField("returnContent", "ids");

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.rc.asu.edu/api/", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data Exported");
            }
        }
    }
}