//This class is used to validate input when entering user info
//before saving and moving to the next scene.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class Validation_UserInfo : Validation_Parent
{
    //Expected fields to check in the scene
    public TMP_InputField teacherName;
    public TMP_InputField teacherID;
    public TMP_InputField assessorName;
    public TMP_InputField assessorID;
    public TMP_InputField childName;
    public TMP_InputField childID;
    public TMP_InputField classRoomName;
    public TMP_InputField classRoomId;
    public static string persistentDataPath;
    public Nullable<bool> displayWarning;

    void Awake()
    {
        persistentDataPath = Application.persistentDataPath + "/";
    }

    //Time to validate!
    //Requires one of each teacher, assessor, and child field filled
    public override bool Validator()
    {
        string empty = ""; //Used to more clearly indicate null strings
        bool valid = true;

        if (teacherName.text == empty && teacherID.text == empty)
            valid = false;
        if (assessorName.text == empty && assessorID.text == empty)
            valid = false;
        if(childName.text == empty && childID.text == empty)
            valid = false;
        if(classRoomName.text == empty && classRoomId.text == empty)
            valid = false;

        displayWarning = shouldDisplayWarning(childID, classRoomId);

        return valid && !displayWarning.Value;
    }

    public bool shouldDisplayWarning(TMP_InputField childIDField, TMP_InputField classroomIDField)
    {
        if (childIDField == null || childIDField.text == null)
            return false;

        string fileName = classroomIDField.text + "_" + childIDField.text + ".txt";
        string loadPath = persistentDataPath + "/" + fileName;
        if (File.Exists(loadPath))
        {
            return true;
        }
        return false;
    }
}