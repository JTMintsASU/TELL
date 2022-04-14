//This script hides a canvas on click
//and shows it when a certain button is clicked 
//Used for prompt displays for each question
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideAndShowCanvas : MonoBehaviour
{
    public Canvas hnsCanvas; //Canvas to hide and show
    public GameObject background; //BG image to hide and show (separate renderer)
    public Button showButton; //Button that shows canvas
    
    // Start is called before the first frame update
    void Start()
    {
        //Call ShowOnClick when showButton is clicked
        showButton.onClick.AddListener(ShowOnClick);
        showButton.enabled = false; //Only enable button when canvas is hidden
    }

    // Update is called once per frame
    void Update()
    {
        //Hide canvas on click if currently displayed
        if (Input.GetMouseButtonDown(0))
        {
            if (hnsCanvas.enabled == true)
            {
                hnsCanvas.enabled = false;
                background.SetActive(false);
                showButton.enabled = true;
            }
        }
    }

    //Show canvas if not shown and not in the middle of a click
    void ShowOnClick()
    {
        Debug.Log("HideAndShowCanvas");
        if (hnsCanvas.enabled == false)
        {
            hnsCanvas.enabled = true;
            background.SetActive(true);
            showButton.enabled = false;
        }
    }
}
