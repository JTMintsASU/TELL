using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad
{
	public SerialData staging;
	public static string persistentDataPath = Application.persistentDataPath;

	//When called, load data from file if no active childID
	public SaveLoad()
    {
		staging = new SerialData();
		//if (DataManager.childID==null)
		//	Load();
	}

	//Creates save file out of datamanager class called <childId>.dat
	public void Save()
	{
		//Prepare data
		staging.sAssessorID = DataManager.assessorID;
		staging.sChildID = DataManager.childID;
		staging.sTeacherID = DataManager.teacherID;
		staging.sGradeVocabExp = DataManager.grade_vocabularyExpressive;
		staging.sGradeVocabRec = DataManager.grade_vocabularyReceptive;
		staging.sGradeVocabTotal = DataManager.grade_vocabularyTotal;

		if (staging.sChildID == null)
			return;

		string fileName = staging.sChildID + ".dat";
		string savePath = persistentDataPath + "/" + fileName;

		//Create and save file
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(savePath);
		bf.Serialize(file, staging);
		file.Close();
	}

	//Attempts to load LocalSave.dat into current DataManager
	//Static members for datamanager allows data to persist through entire app
	public void load(TMP_InputField childIDField)
	{
		if (childIDField == null || childIDField.text == null)
			return;

		string fileName = childIDField.text + ".dat";
		string loadPath = persistentDataPath + "/" + fileName;

		//Load file into serializable
		if (File.Exists(loadPath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(loadPath, FileMode.Open);
			staging = (SerialData)bf.Deserialize(file);
			file.Close();
		}

		//Load serializable into Datamanager
		DataManager.assessorID = staging.sAssessorID;
		DataManager.childID = staging.sChildID;
		DataManager.teacherID = staging.sTeacherID;
		DataManager.grade_vocabularyExpressive = staging.sGradeVocabExp;
		DataManager.grade_vocabularyReceptive = staging.sGradeVocabRec;
		DataManager.grade_vocabularyTotal = staging.sGradeVocabTotal;
	}
}
