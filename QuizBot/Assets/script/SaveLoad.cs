using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad
{
	public SerialData staging;
	public static string pdP = Application.persistentDataPath;

	//When called, load data from file if no active childID
	public SaveLoad()
    {
		staging = new SerialData();
	}

	// Creates a file and saves data currently in DataManager (current user)
	// 1. Data is stored in a file -> <classroom_id>_<child_id>.dat
	// 2. Only data of current user is stored
	public void Save()
	{
		//Prepare data
		staging.sAssessorID = DataManager.assessorID;
		staging.sChildID = DataManager.childID;
		staging.sTeacherID = DataManager.teacherID;
		staging.sClassroomID = DataManager.classroomId;
		
		// Storing vocab game data
		staging.sGradeVocabExp = DataManager.grade_vocabularyExpressive;
		staging.sGradeVocabRec = DataManager.grade_vocabularyReceptive;
		staging.sGradeVocabTotal = DataManager.grade_vocabularyTotal;
		if (DataManager.globalTime > 0)
		{
			staging.sIndividualExpressiveList = DataManager.individual_vocabularyExpressive;
			staging.sIndividualExpressiveFlagList = DataManager.individual_vocabularyExpressiveFlag;
			staging.sIndividualReceptiveList = DataManager.individual_vocabularyReceptive;
			staging.sIndividualReceptiveFlagList = DataManager.individual_vocabularyReceptiveFlag;
			staging.sIndividualResponses = DataManager.individual_vocabularyResponses;
		}

		if (staging.sClassroomID == null || staging.sChildID == null)
		{
			Debug.LogError("Missing childID or classroomId, unable to save data");
			return;
		}
		
		string fileName = staging.sClassroomID + "_" + staging.sChildID + ".dat"; // File for saving, filename will be <childID>.dat
		string savePath = Path.Combine(pdP, fileName); // File path for storage with the file name

		//Create and save file
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);
		bf.Serialize(file, staging);
		file.Close();
	}

	// Loads data from a file into DataManager
	// 1. Data is stored in a file -> <classroom_id>_<child_id>.dat
	// 2. Data of user is loaded in DataManager
	public void Load(TMP_InputField childIDField, TMP_InputField classroomIDField)
	{
		if (childIDField == null || childIDField.text == null || 
		    classroomIDField == null || classroomIDField.text == null)
		{
			Debug.LogError("Missing childID/classroomId, unable to load data");
			return;
		}
		
		// Obtain the filePath for loading
		string fileName = classroomIDField.text + "_" + childIDField.text + ".dat";
		string loadPath = Path.Combine(pdP, fileName);

		// Open file and load data
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(loadPath, FileMode.Open);
		staging = (SerialData)bf.Deserialize(file);
		file.Close();

		//Load serializable into Datamanager
		DataManager.assessorID = staging.sAssessorID;
		DataManager.childID = staging.sChildID;
		DataManager.teacherID = staging.sTeacherID;
		DataManager.classroomId = staging.sClassroomID;
		DataManager.grade_vocabularyExpressive = staging.sGradeVocabExp;
		DataManager.grade_vocabularyReceptive = staging.sGradeVocabRec;
		DataManager.grade_vocabularyTotal = staging.sGradeVocabTotal;
		DataManager.individual_vocabularyExpressive = staging.sIndividualExpressiveList;
		DataManager.individual_vocabularyExpressiveFlag = staging.sIndividualExpressiveFlagList;
		DataManager.individual_vocabularyReceptive = staging.sIndividualReceptiveList;
		DataManager.individual_vocabularyReceptiveFlag = staging.sIndividualReceptiveFlagList;
		DataManager.individual_vocabularyResponses = staging.sIndividualResponses;
	}
	
	// Creates files and saves data passed as parameter
	// 1. Data is stored in a file -> <classroom_id>_<child_id>.dat
	// 2. Only data of current user is stored
	public void SaveAll(UsersDetails usersDetails)
	{
		if (usersDetails == null || usersDetails.users == null)
			return;
		
		foreach (var user in usersDetails.users)
		{
			SerialData staging = SerialData.convertToSerialData(user);
			
			if (staging.sClassroomID == null || staging.sChildID == null)
			{
				Debug.LogError("Missing childID or classroomId, unable to save data");
				return;
			}
			
		
			string fileName = staging.sClassroomID + "_" + staging.sChildID + ".dat"; // File for saving, filename will be <childID>.dat
			string savePath = Path.Combine(pdP, fileName); // File path for storage with the file name

			//Create and save file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(savePath);
			bf.Serialize(file, staging);
			file.Close();
		}
	}
}
