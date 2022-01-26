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
        StartCoroutine(RedCapService.Instance.ExportData());
    }
}