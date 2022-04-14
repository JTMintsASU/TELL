using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

// This class is responsible for creating and executing all requests (API calls) for RedCap Database. 
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


    // Responsible for executing API call for importing data from RedCap to local file system. 
    // Files created with the file name -> <classroom_id>_<child_id>.dat
    public IEnumerator ImportAllData(System.Action<UsersDetails> callBack, RedCapRequest redCapRequest){
        
        WWWForm form = new WWWForm();
        
        if (!String.IsNullOrEmpty(redCapRequest.token))
            form.AddField("token", redCapRequest.token);
        
        if (!String.IsNullOrEmpty(redCapRequest.content))
            form.AddField("content", redCapRequest.content);
        
        if (!String.IsNullOrEmpty(redCapRequest.action))
            form.AddField("action", redCapRequest.action);
        
        if (!String.IsNullOrEmpty(redCapRequest.format))
            form.AddField("format", redCapRequest.format);
        
        if (!String.IsNullOrEmpty(redCapRequest.type))
            form.AddField("type", redCapRequest.type);
        
        if (!String.IsNullOrEmpty(redCapRequest.returnContent))
            form.AddField("returnFormat", redCapRequest.returnContent);  
        
        if (!String.IsNullOrEmpty(redCapRequest.fields_0))
            form.AddField("fields[0]", "record_id");
        
        if (!String.IsNullOrEmpty(redCapRequest.form_0))
            form.AddField("forms[0]", redCapRequest.form_0);
        
        if (redCapRequest.records_0 != null && redCapRequest.records_0 != 0)
            form.AddField("records[0]", redCapRequest.records_0);

        if (!String.IsNullOrEmpty(redCapRequest.filterLogic))
            form.AddField("filterLogic", redCapRequest.filterLogic);

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
                UsersDetails root = JsonConvert.DeserializeObject<UsersDetails>("{\"users\":" + response + "}");
                // UsersDetails root = JsonUtility.FromJson<UsersDetails>("{\"users\":" + response + "}");
                callBack(root);
            }
        }
    }
    
    // Responsible for executing API call for importing data from local file system to RedCap. 
    public IEnumerator ExportCredentials(RedCapRequest redCapRequest)
    {
        WWWForm form = new WWWForm();
        
        if (!String.IsNullOrEmpty(redCapRequest.token))
            form.AddField("token", redCapRequest.token);
        
        if (!String.IsNullOrEmpty(redCapRequest.content))
            form.AddField("content", redCapRequest.content);
        
        if (!String.IsNullOrEmpty(redCapRequest.action))
            form.AddField("action", redCapRequest.action);
        
        if (!String.IsNullOrEmpty(redCapRequest.format))
            form.AddField("format", redCapRequest.format);
        
        if (!String.IsNullOrEmpty(redCapRequest.type))
            form.AddField("type", redCapRequest.type);
        
        if (!String.IsNullOrEmpty(redCapRequest.overwriteBehavior))
            form.AddField("overwriteBehavior", redCapRequest.overwriteBehavior);
        
        if (!String.IsNullOrEmpty(redCapRequest.forceAutoNumber))
            form.AddField("forceAutoNumber", redCapRequest.forceAutoNumber);
        
        if (!String.IsNullOrEmpty(redCapRequest.data))
            form.AddField("data", redCapRequest.data);
        
        if (!String.IsNullOrEmpty(redCapRequest.returnContent))
            form.AddField("returnContent", redCapRequest.returnContent);

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