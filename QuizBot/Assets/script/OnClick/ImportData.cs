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
        // Debug.Log("checking button click");
        StartCoroutine(Import());
    }

    IEnumerator Import()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", "B345C5E9AFB7556F4627986E305D4F81");
        form.AddField("content", "record");
        form.AddField("action", "export");
        form.AddField("format", "json");
        form.AddField("type", "flat");
        form.AddField("returnFormat", "json");
        form.AddField("fields", "game,time,teacher_id");

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
                Debug.Log(www.downloadHandler.text);
                RootObject root = JsonUtility.FromJson<RootObject>("{\"users\":" + response + "}");
                Debug.Log(("Deserialized"));
            }
        }
    }
}