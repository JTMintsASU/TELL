using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.UI;

// This class is responsible for creating and executing all requests (API calls) for RedCap Database. 
public class RedCapService : MonoBehaviour
{
    private static RedCapService _instance;
    public static string pdP;
    public static bool dataLoaded = false;
    public static bool error = false;
     public static IEnumerator import_data;

    private void Awake()
    {
        pdP = Application.persistentDataPath;
    }

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
            // Files created with the file name -> <child_id>.dat
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
        
        if (redCapRequest.records_0 != 0)
            form.AddField("records[0]", redCapRequest.records_0);

        if (!String.IsNullOrEmpty(redCapRequest.filterLogic))
            form.AddField("filterLogic", redCapRequest.filterLogic);

        if (!String.IsNullOrEmpty(redCapRequest.redCapEventName))
        {
            form.AddField("events", redCapRequest.redCapEventName);
        }
        /*if (!String.IsNullOrEmpty(redCapRequest.arm))
        {
            form.AddField("arm", redCapRequest.arm);
        }*/

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.asu.edu/api/", form))
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
                callBack(root);
            }
        }
    }

    public void CheckIfDataExists(RedCapRequest redCapRequest,String childID,String childName){
        import_data = CheckIfDataExists_req(redCapRequest, childID, childName);
     }

    public bool WaitOnData(RedCapRequest redCapRequest,String childID,String childName)
    {
        if (!dataLoaded) CheckIfDataExists(redCapRequest, childID, childName); 
        while (import_data.MoveNext())
        {
            Debug.Log("Waiting for data to be validated!");
        }
        dataLoaded = false;
        if(error){
            error = false;
            return true;
        }
        return false;
    }
      

    // Responsible for executing API call for importing data from RedCap to local file system. 
    // Files created with the file name -> <child_id>.dat
    public IEnumerator CheckIfDataExists_req(RedCapRequest redCapRequest,String childID,String childName){
        
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
        
        if (redCapRequest.records_0 != 0)
            form.AddField("records[0]", redCapRequest.records_0);

        if (!String.IsNullOrEmpty(redCapRequest.filterLogic))
            form.AddField("filterLogic", redCapRequest.filterLogic);

        if (!String.IsNullOrEmpty(redCapRequest.redCapEventName))
        {
            form.AddField("events", redCapRequest.redCapEventName);
        }
        /*if (!String.IsNullOrEmpty(redCapRequest.arm))
        {
            form.AddField("arm", redCapRequest.arm);
        }*/


        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.asu.edu/api/", form))
        {
            yield return www.SendWebRequest();

            while (!www.isDone)
            {
                yield return true;
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                if(www.error != null){
                    error = false;
                    dataLoaded = true;
                }
            }
            else
            {
                string response = www.downloadHandler.text;
                
                UsersDetails usersDetails = JsonConvert.DeserializeObject<UsersDetails>("{\"users\":" + response + "}");
                for (int index = usersDetails.users.Count - 1; index >= 0; index--){
                    RedCapRecord redCapRecord = usersDetails.users[index];

                    if (redCapRecord.recordID == null || redCapRecord.recordID == 0)
                    {
                        error = false;
                        dataLoaded = true;
                        usersDetails.users.RemoveAt(index);
                        Debug.Log("Record obtained from RedCap is corrupted, missing record_id");
                    }
                    else if((childID != "" && redCapRecord.childID == childID) || (childName != "" && redCapRecord.childName == childName))
                    {
                        error = true;
                        dataLoaded = true;
                    }
                }
            }           
        }
    }

    public WWWForm prepareForm(RedCapRequest redCapRequest)
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

        if (!String.IsNullOrEmpty(redCapRequest.returnContent))
            form.AddField("returnFormat", redCapRequest.returnContent);

        if (!String.IsNullOrEmpty(redCapRequest.fields_0))
            form.AddField("fields[0]", "record_id");

        if (!String.IsNullOrEmpty(redCapRequest.form_0))
            form.AddField("forms[0]", redCapRequest.form_0);

        if (redCapRequest.records_0 != 0)
            form.AddField("records[0]", redCapRequest.records_0);

        if (!String.IsNullOrEmpty(redCapRequest.filterLogic))
            form.AddField("filterLogic", redCapRequest.filterLogic);

        return form;
    }

    public MasterUsersDetails getAllMasterRecords(RedCapRequest redCapRequest)
    {
        WWWForm form = prepareForm(redCapRequest);

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.asu.edu/api/", form))
        {
            //yield return www.SendWebRequest();
            www.SendWebRequest();

            /*while (!www.isDone)
            {
                yield return true;
            }*/

            while (!www.isDone)
            {
                Debug.Log("waiting");
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                if (www.error != null)
                {
                    error = false;
                    dataLoaded = true;
                }
            }
            else
            {
                string response = www.downloadHandler.text;

                MasterUsersDetails masterUsersDetails = JsonConvert.DeserializeObject<MasterUsersDetails>("{\"masterUsers\":" + response + "}");
                return masterUsersDetails;
            }
        }
        return null;
    }
    public void CheckIfTeacherDataExists_req(RedCapRequest redCapRequest, List<String> studentNamesList, List<String> studentIDsList, RedCapMasterRecord redCapMasterRecord)
    {

        WWWForm form = prepareForm(redCapRequest);

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.asu.edu/api/", form))
        {
            //yield return www.SendWebRequest();
            www.SendWebRequest();

            /*while (!www.isDone)
            {
                yield return true;
            }*/

            while (!www.isDone)
            {
                Debug.Log("waiting");
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                if (www.error != null)
                {
                    error = false;
                    dataLoaded = true;
                }
            }
            else
            {
                string response = www.downloadHandler.text;

                MasterUsersDetails masterUsersDetails = JsonConvert.DeserializeObject<MasterUsersDetails>("{\"masterUsers\":" + response + "}");
                if(masterUsersDetails != null && masterUsersDetails.masterUsers.Count>0)
                {
                    redCapMasterRecord.classroomEventName = masterUsersDetails.masterUsers[0].classroomEventName;
                    redCapMasterRecord.passcode = masterUsersDetails.masterUsers[0].passcode;
                    redCapMasterRecord.schoolName = masterUsersDetails.masterUsers[0].schoolName;
                    redCapMasterRecord.classroomName = masterUsersDetails.masterUsers[0].classroomName;
                    redCapMasterRecord.districtName = masterUsersDetails.masterUsers[0].districtName;
                    redCapMasterRecord.teacherName = masterUsersDetails.masterUsers[0].teacherName;
                    redCapMasterRecord.classroomID = masterUsersDetails.masterUsers[0].classroomID;
                    redCapMasterRecord.schoolID = masterUsersDetails.masterUsers[0].schoolID;
                    redCapMasterRecord.recordID = masterUsersDetails.masterUsers[0].recordID;
                    redCapMasterRecord.districtID = masterUsersDetails.masterUsers[0].districtID;
                    redCapMasterRecord.teacherID = masterUsersDetails.masterUsers[0].teacherID;
                    redCapMasterRecord.token = masterUsersDetails.masterUsers[0].token;

                    for (int ind = 0; ind< masterUsersDetails.masterUsers.Count; ind++)
                    {
                        studentIDsList.Add(masterUsersDetails.masterUsers[ind].studentID);
                        studentNamesList.Add(masterUsersDetails.masterUsers[ind].studentName);
                    }
                }
            }
        }
    }

    public void ExportFileFromRedCap(RedCapMasterRecord redCapMasterRecord, string redcapRecordId, int instanceIndex, List<string> imageDataList)
    {
        string redcapUrl = "https://redcap.asu.edu/api/";
        string redcapRepeatInstrument = "writing";
        string redcapRepeatInstance = (instanceIndex + 1).ToString();

        using (var client = new HttpClient())
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(redCapMasterRecord.token), "token");
            content.Add(new StringContent("file"), "content");
            content.Add(new StringContent("export"), "action");
            content.Add(new StringContent(redcapRecordId), "record");
            content.Add(new StringContent("image_writing"), "field");
            content.Add(new StringContent(redcapRepeatInstrument), "repeat_instrument");
            content.Add(new StringContent(redcapRepeatInstance), "repeat_instance");
            content.Add(new StringContent(redCapMasterRecord.classroomEventName), "event");
            //content.Add(new StringContent(redCapMasterRecord.teacherID), "teacher_id_arc");

            // Perform POST request
            //HttpResponseMessage response = await client.PostAsync(redcapUrl, content);
            HttpResponseMessage response = client.PostAsync(redcapUrl, content).Result;


            // Check response status and content
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("File exported successfully.");
                byte[] fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                DataManager.individual_image_data[instanceIndex] = Convert.ToBase64String(fileBytes);
                imageDataList[instanceIndex] = Convert.ToBase64String(fileBytes);
            }
            else
            {
                Debug.Log("File export failed.");
                string responseContent = response.Content.ReadAsStringAsync().Result;
                Debug.Log(responseContent);
            }
        }
    }

    public async Task clientFileUploladTaskExecution(RedCapMasterRecord redCapMasterRecord, String redcapRecordId, int i, string filePath)
    {

        string redcapUrl = "https://redcap.asu.edu/api/";
        string fieldName = "image_writing";
        string redcapRepeatInstrument = "writing";
        string redcapRepeatInstance = (i + 1).ToString();

        using (var client = new HttpClient())
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(redCapMasterRecord.token), "token");
            content.Add(new StringContent("file"), "content");
            content.Add(new StringContent("import"), "action");
            content.Add(new StringContent(redcapRecordId), "record");
            /*content.Add(new StringContent("[writing_session_no]=" + "\"" + redcapRepeatInstance + "\""),
                "filterLogic");*/
            content.Add(new StringContent(fieldName), "field");
            content.Add(new StringContent(redcapRepeatInstrument), "repeat_instrument");
            content.Add(new StringContent(redcapRepeatInstance), "repeat_instance");

            content.Add(new StringContent(redCapMasterRecord.classroomEventName), "event");
            //content.Add(new StringContent(redCapMasterRecord.teacherID), "teacher_id_arc");

            byte[] fileBytes = Convert.FromBase64String(filePath);
            var fileContent = new ByteArrayContent(fileBytes);
            content.Add(fileContent, "file", "WritingImage_"+DataManager.childID+"_TimePoint"+(i+1));
            //byte[] fileBytes = File.ReadAllBytes(filePath);
            //var fileContent = new ByteArrayContent(fileBytes);
            //content.Add(fileContent, "file", Path.GetFileName(filePath));

            // Perform POST request
            HttpResponseMessage response = await client.PostAsync(redcapUrl, content);

            // Check response status and content
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("File uploaded successfully.");
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.Log(responseContent);
            }
            else
            {
                Debug.Log("File upload failed.");
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.Log(responseContent);
            }
        }
    }
    public void exportFileUpload(RedCapMasterRecord redCapMasterRecord, String redcapRecordId)
    {
        for (int i = 0; i < DataManager.individual_image_data.Count; i++)
        {
            string filePath = DataManager.individual_image_data[i];
            if (filePath != null && filePath.Trim() != "")
            {
                Debug.Log("File is about to be uploaded");
                //StartCoroutine(RedCapService.Instance.clientFileUploladTaskExecution(token, redcapRecordId, i, base64Image));
                Task task = clientFileUploladTaskExecution(redCapMasterRecord, redcapRecordId, i, filePath);
            }
        }
    }


    // Responsible for executing API call for importing data from local file system to RedCap. 
    public void ExportCredentials(RedCapRequest redCapRequest, String fileName, int? recordId, bool childExport)
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

        Debug.Log("redCapRequest.data "+redCapRequest.data);
        if (!String.IsNullOrEmpty(redCapRequest.data))
            form.AddField("data", redCapRequest.data);

        /*if (!String.IsNullOrEmpty(redCapRequest.arm))
        {
            form.AddField("arm", redCapRequest.arm);
        }
        if (!String.IsNullOrEmpty(redCapRequest.redCapEventName))
        {
            form.AddField("event", redCapRequest.redCapEventName);
        }*/

        if (!String.IsNullOrEmpty(redCapRequest.returnContent))
            form.AddField("returnContent", redCapRequest.returnContent);

        using (UnityWebRequest www = UnityWebRequest.Post("https://redcap.asu.edu/api/", form))
        {
            Debug.Log("About to be yielded ");

            //yield return www.SendWebRequest();

            www.SendWebRequest();

            Debug.Log("Request is yielded ");

            while (!www.isDone)
            {
                Debug.Log("waiting");
            }
            //yield return new WaitUntil(() => www.isDone);

            Debug.Log("We have got the result");

            if (www.result != UnityWebRequest.Result.Success)
            {

                Debug.Log(www.error);
            }
            else
            {
                //if(File.Exists(fileName)){
                //   File.Delete(fileName);
                //}
                if (childExport)
                {
                    if (String.IsNullOrEmpty(DataManager.recordID))
                    {
                        string response = www.downloadHandler.text;
                        List<string> ids = JsonConvert.DeserializeObject<List<string>>(response);
                        //UsersDetails usersDetails = JsonConvert.DeserializeObject<UsersDetails>("{\"users\":" + response + "}");
                        DataManager.recordID = ids[0];
                    }
                    exportFileUpload(DataManager.redCapMasterRecord, DataManager.recordID);
                }
                Debug.Log("Data Exported");
            }
        }
    }
}