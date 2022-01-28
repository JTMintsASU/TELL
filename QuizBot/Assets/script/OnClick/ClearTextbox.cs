using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearTextbox : MonoBehaviour
{
    public Button clearButton;
    public TMP_InputField boxToClear;

    // Start is called before the first frame update
    void Start()
    {
        clearButton.onClick.AddListener(ClearOnClick);
        
    }

    // Update is called once per frame
    void ClearOnClick()
    {
        boxToClear.text = "";
    }
}
