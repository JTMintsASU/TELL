
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Prompts_BS : MonoBehaviour
{
    public List<BSItem> universalItems;
    public List<BSItem> promptsToDisplay;
    public static string firstItem = "Fan";
    public static string configurationFilePath = "script/data/bs_items.json";
    
    // Start is called before the first frame update
    void Awake()
    {
        string readPath = Path.Combine(Application.dataPath, configurationFilePath);
        using (StreamReader r = new StreamReader(readPath))
        {
            string json = r.ReadToEnd();
            universalItems = JsonConvert.DeserializeObject<List<BSItem>>(json);
        }

        if (universalItems == null || universalItems.Count == 0)
        {
            Debug.Log("No items in configuration file to display");
            promptsToDisplay = new List<BSItem>();
        }
        else
        {
            universalItems = universalItems.FindAll(item => item.item != String.Empty &&
                                                            item.index != null &&
                                                            item.difficulty != null);
            BSItem itemToAdd = null;
            foreach (var item in universalItems)
            {
                if (item.item.Equals(firstItem))
                {
                    List<BSItem> items = new List<BSItem>(){};
                    itemToAdd = item;
                    items.Add(itemToAdd);
                    promptsToDisplay = items;
                }
            }
            if (itemToAdd != null)
                universalItems.Remove(itemToAdd);
        }
    }
}