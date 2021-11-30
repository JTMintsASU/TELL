using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad
{
	public SerialData staging;

    //When called, load data from file if no active childID
    public SaveLoad()
    {
		staging = new SerialData();
		if (DataManager.childID==null)
			Load();
	}

	//Creates save file out of datamanager class called LocalSave.dat
	public void Save()
	{
		//Prepare data
		staging.sAssessorID = DataManager.assessorID;
		staging.sChildID = DataManager.childID;
		staging.sTeacherID = DataManager.teacherID;
		staging.sGradeVocabExp = DataManager.grade_vocabularyExpressive;
		staging.sGradeVocabRec = DataManager.grade_vocabularyReceptive;
		staging.sGradeVocabTotal = DataManager.grade_vocabularyTotal;

		//Create and save file
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath
					 + "/LocalSave.dat");
		bf.Serialize(file, staging);
		file.Close();
	}

	//Attempts to load LocalSave.dat into current DataManager
	//Static members for datamanager allows data to persist through entire app
	public void Load()
	{
		//Load file into serializable
		if (File.Exists(Application.persistentDataPath
					   + "/LocalSave.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/LocalSave.dat", FileMode.Open);
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
