//This script is being used to connect the sprite images in advanceBSItem
//with the bs_items metadata table via BSItems.
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Prompts_BS : MonoBehaviour
{
    public List<BSItem> universalItems;
    public List<BSItem> promptsToDisplay;
    public static string testItem = "Horse";
    public static string firstItem = "Fan";
    public static string configurationFile = "bs_items";

    // Start is called before the first frame update
    void Awake()
    {
        string json = Resources.Load<TextAsset>(configurationFile).ToString();
        universalItems = JsonConvert.DeserializeObject<List<BSItem>>(json);

        if (universalItems == null || universalItems.Count == 0)
        {
            Debug.Log("No items in configuration file to display");
            promptsToDisplay = new List<BSItem>();
        }
        else
        {
            universalItems = universalItems.FindAll(item => item.item != String.Empty &&
                                                            item.index != 0 &&
                                                            item.difficulty != 0.0);
            BSItem itemToAdd = null;
            BSItem testItemToAdd = null;
            foreach (var item in universalItems)
            {
                if (item.item.Equals(firstItem))
                {
                    itemToAdd = item;
                }
                if (item.item.Equals(testItem))
                {
                    testItemToAdd = item;
                }
            }
            
            List<BSItem> items = new List<BSItem>(){};
            items.Add(testItemToAdd);
            items.Add(itemToAdd);
            promptsToDisplay = items;
            if (testItemToAdd != null)
                universalItems.Remove(testItemToAdd);
            if (itemToAdd != null)
                universalItems.Remove(itemToAdd);
        }
    }
}