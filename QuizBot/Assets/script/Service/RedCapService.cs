using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
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
    // Files created with the file name -> <classroom_id>_<child_id>.txt
    public IEnumerator ImportAllData(System.Action<UsersDetails> callBack, 
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
        if (fields != null)
        {
            form.AddField("fields", fields);
        }

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
    
    
    // Responsible for exporting data in DataManager into RedCap Database
    public IEnumerator ExportData()
    {
        string teacherId = DataManager.teacherID;
        string assessorId = DataManager.assessorID;
        string childId = DataManager.childID;
        string classroomId = DataManager.classroomId;

        UserDetail userDetail = new UserDetail();
        userDetail.record_id = Random.Range(10, 1000).ToString();
        userDetail.teacher_id = teacherId;
        userDetail.assessor_id = assessorId;
        userDetail.child_id = childId;
        userDetail.classroom_id = classroomId;
        
        string data = JsonUtility.ToJson(userDetail);
        
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
    public IEnumerator ExportAllData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        DirectoryInfo directory = new DirectoryInfo(pdP);
        FileInfo[] filesInDirectory = directory.GetFiles("*.txt");
        UsersDetails userDetails = new UsersDetails();
        
        foreach(FileInfo fileInDirectory in filesInDirectory)
        {
            string fileName = fileInDirectory.FullName;
            FileStream file =  File.Open(fileName, FileMode.Open);
            SerialData serialData = (SerialData) bf.Deserialize(file);
            UserDetail userDetail = UserDetail.fromSerialData(serialData);
            userDetails.users.Add(userDetail);
            file.Close();
        }

        string data = JsonConvert.SerializeObject(userDetails.users);
        //string data = JsonUtility.ToJson(userDetails.users);
        
        WWWForm form = new WWWForm();
        form.AddField("token", "B345C5E9AFB7556F4627986E305D4F81");
        form.AddField("content", "record");
        form.AddField("action", "import");
        form.AddField("format", "json");
        form.AddField("type", "flat");
        form.AddField("overwriteBehavior", "overwrite");
        form.AddField("forceAutoNumber", "true");
        form.AddField("data", data);
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