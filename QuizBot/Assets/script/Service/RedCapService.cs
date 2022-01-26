using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class RedCapService : MonoBehaviour
{
    private static RedCapService _instance;

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
    public IEnumerator ImportData(System.Action<UsersDetails> callBack, 
                                  string token, 
                                  string content, 
                                  string action, 
                                  string format, 
                                  string type, 
                                  string returnFormat, 
                                  string fields)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("content", content);
        form.AddField("action", action);
        form.AddField("format", format);
        form.AddField("type", type);
        form.AddField("returnFormat", returnFormat);
        form.AddField("fields", fields);

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
                Debug.Log("Request and deserialization successful");
                callBack(root);
            }
        }
    }
    
    public IEnumerator ExportData()
    {
        string teacherId = DataManager.teacherID;
        string assessorId = DataManager.assessorID;

        UserDetail userDetail = new UserDetail();
        userDetail.record_id = "record_id";
        userDetail.teacher_id = teacherId;
        userDetail.assessor_id = assessorId;
        
        string data = JsonUtility.ToJson(userDetail);
        
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