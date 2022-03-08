//This class is used to hold data when it is transferred from app cache memory to local data files
using System;

[Serializable]
public class SerialData
{
    public string sRecordId;
    public string sTeacherID;
    public string sAssessorID;
    public string sChildID;
    public string sClassroomID;

    //Grade storage
    public double[] sGradeVocabExp;
    public double[] sGradeVocabRec;
    public double[] sGradeVocabTotal;
    
    
    public static SerialData convertToSerialData(Credential credential)
    {
        SerialData serialData = new SerialData();
        serialData.sRecordId = credential.record_id.ToString();
        serialData.sAssessorID = credential.assessor_id;
        serialData.sChildID = credential.child_id;
        serialData.sClassroomID = credential.classroom_id;
        serialData.sTeacherID = credential.teacher_id;
        return serialData;
    }
}
