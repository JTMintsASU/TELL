using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class RedCapService : MonoBehaviour
{
    private static RedCapService _instance;
    public static string pdP = Application.persistentDataPath;

    // Create a singleton
    public static RedCapService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RedCapService>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RedCapService).Name;
                    _instance = go.AddComponent<RedCapService>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }


    // Responsible for importing all data in RedCap to local file system. 
    // Files created with the file name -> <classroom_id>_<child_id>.dat
    public IEnumerator ImportAllData(System.Action<UsersDetails> callBack, RedCapRequest redCapRequest){
        
        WWWForm form = new WWWForm();
        
        if (!String.IsNullOrEmpty(redCapRequest.Token))
            form.AddField("token", redCapRequest.Token);
        
        if (!String.IsNullOrEmpty(redCapRequest.Content))
            form.AddField("content", redCapRequest.Content);
        
        if (!String.IsNullOrEmpty(redCapRequest.Action))
            form.AddField("action", redCapRequest.Action);
        
        if (!String.IsNullOrEmpty(redCapRequest.Format))
            form.AddField("format", redCapRequest.Format);
        
        if (!String.IsNullOrEmpty(redCapRequest.Type))
            form.AddField("type", redCapRequest.Type);
        
        if (!String.IsNullOrEmpty(redCapRequest.ReturnContent))
            form.AddField("returnFormat", redCapRequest.ReturnContent);  
        
        if (!String.IsNullOrEmpty(redCapRequest.Fields0))
            form.AddField("fields[0]", "record_id");
        
        if (!String.IsNullOrEmpty(redCapRequest.Form0))
            form.AddField("forms[0]", redCapRequest.Form0);

        if (!String.IsNullOrEmpty(redCapRequest.FilterLogic))
            form.AddField("filterLogic", redCapRequest.FilterLogic);

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.rc.asu.edu/api/", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                UsersDetails root = JsonUtility.FromJson<UsersDetails>("{\"users\":" + response + "}");
                callBack(root);
            }
        }
    }
    
    // Legacy code, please ignore.
    // Responsible for exporting data in DataManager into RedCap Database
    public IEnumerator ExportData()
    {
        string teacherId = DataManager.teacherID;
        string assessorId = DataManager.assessorID;
        string childId = DataManager.childID;
        string classroomId = DataManager.classroomId;

        Credential credential = new Credential();
        credential.record_id = Random.Range(10, 1000);
        credential.teacher_id = teacherId;
        credential.assessor_id = assessorId;
        credential.child_id = childId;
        credential.classroom_id = classroomId;
        
        string data = JsonUtility.ToJson(credential);
        
        WWWForm form = new WWWForm();
        form.AddField("token", "B345C5E9AFB7556F4627986E305D4F81");
        form.AddField("content", "record");
        form.AddField("action", "import");
        form.AddField("format", "json");
        form.AddField("type", "flat");
        form.AddField("overwriteBehavior", "normal");
        form.AddField("forceAutoNumber", "true");
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
    
    // Responsible for exporting ALL data present in local files into RedCap Database
    public IEnumerator ExportCredentials(RedCapRequest redCapRequest)
    {
        WWWForm form = new WWWForm();
        
        if (!String.IsNullOrEmpty(redCapRequest.Token))
            form.AddField("token", redCapRequest.Token);
        
        if (!String.IsNullOrEmpty(redCapRequest.Content))
            form.AddField("content", redCapRequest.Content);
        
        if (!String.IsNullOrEmpty(redCapRequest.Action))
            form.AddField("action", redCapRequest.Action);
        
        if (!String.IsNullOrEmpty(redCapRequest.Format))
            form.AddField("format", redCapRequest.Format);
        
        if (!String.IsNullOrEmpty(redCapRequest.Type))
            form.AddField("type", redCapRequest.Type);
        
        if (!String.IsNullOrEmpty(redCapRequest.OverwriteBehavior))
            form.AddField("overwriteBehavior", redCapRequest.OverwriteBehavior);
        
        if (!String.IsNullOrEmpty(redCapRequest.ForceAutoNumber))
            form.AddField("forceAutoNumber", redCapRequest.ForceAutoNumber);
        
        if (!String.IsNullOrEmpty(redCapRequest.Data))
            form.AddField("data", redCapRequest.Data);
        
        if (!String.IsNullOrEmpty(redCapRequest.ReturnContent))
            form.AddField("returnContent", redCapRequest.ReturnContent);

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