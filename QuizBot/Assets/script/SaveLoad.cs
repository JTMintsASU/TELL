using System.IO;
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

	//Creates save file out of datamanager class called <childId>.dat
	public void Save()
	{
		//Prepare data
		staging.sAssessorID = DataManager.assessorID;
		staging.sChildID = DataManager.childID;
		staging.sTeacherID = DataManager.teacherID;
		staging.sClassroomID = DataManager.classroomId;
		staging.sGradeVocabExp = DataManager.grade_vocabularyExpressive;
		staging.sGradeVocabRec = DataManager.grade_vocabularyReceptive;
		staging.sGradeVocabTotal = DataManager.grade_vocabularyTotal;

		if (staging.sChildID == null)
		{
			Debug.LogError("Missing childID, unable to save data");
			return;
		}
		
		string fileName = staging.sClassroomID + "_" + staging.sChildID + ".txt"; // File for saving, filename will be <childID>.dat
		string savePath = pdP + "/" + fileName; // File path for storage with the file name

		//Create and save file
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);
		bf.Serialize(file, staging);
		file.Close();
	}

	//Attempts to load LocalSave.dat into current DataManager
	//Static members for datamanager allows data to persist through entire app
	public void Load(TMP_InputField childIDField, TMP_InputField classroomIDField)
	{
		if (childIDField == null || childIDField.text == null)
		{
			Debug.LogError("Missing childID, unable to load data");
			return;
		}
		
		// Obtain the filePath for loading
		string fileName = classroomIDField.text + "_" + childIDField.text + ".txt";
		string loadPath = pdP + "/" + fileName;

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
	}
}
