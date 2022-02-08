//This class is used to hold data when it is transferred from app cache memory to local data files
using System;

[Serializable]
public class SerialData
{
    public string sTeacherID;
    public string sAssessorID;
    public string sChildID;
    public string sClassroomID;

    //Grade storage
    public double[] sGradeVocabExp;
    public double[] sGradeVocabRec;
    public double[] sGradeVocabTotal;
    
    
    public static SerialData fromUserDetail(UserDetail userDetail)
    {
        SerialData serialData = new SerialData();
        serialData.sAssessorID = userDetail.assessor_id;
        serialData.sChildID = userDetail.child_id;
        serialData.sClassroomID = userDetail.classroom_id;
        serialData.sTeacherID = userDetail.teacher_id;
        return serialData;
    }
}
