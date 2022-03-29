//This class is used to hold data when it is transferred from app cache memory to local data files
using System;
using System.Collections.Generic;

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
    public List<List<bool>> sIndividualExpressiveList;
    public List<List<bool>> sIndividualExpressiveFlagList;
    public List<List<bool>> sIndividualReceptiveList;
    public List<List<bool>> sIndividualReceptiveFlagList;
    public List<List<string>> sIndividualResponses;
    
    public static SerialData convertToSerialData(RedCapRecord redCapRecord)
    {
        SerialData serialData = new SerialData();
        serialData.sRecordId = redCapRecord.recordID.ToString();
        serialData.sAssessorID = redCapRecord.assessorID;
        serialData.sChildID = redCapRecord.childID;
        serialData.sClassroomID = redCapRecord.classroomID;
        serialData.sTeacherID = redCapRecord.teacherID;
        return serialData;
    }
}
