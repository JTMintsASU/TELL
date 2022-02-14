using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[System.Serializable]

// DTO - Credentials
// Represents a sample record in "Credentials" Instrument in RedCap
public class Credential
{
    public int record_id;
    public string teacher_id;
    public string assessor_id;
    public string child_id;
    public string classroom_id;

    public static Credential fromSerialData(SerialData serialData)
    {
        Credential credential = new Credential();
        credential.record_id = String.IsNullOrEmpty(serialData.sRecordId) ? int.MaxValue : int.Parse(serialData.sRecordId);
        credential.assessor_id = serialData.sAssessorID;
        credential.child_id = serialData.sChildID;
        credential.classroom_id = serialData.sClassroomID;
        credential.teacher_id = serialData.sTeacherID;
        return credential;
    }
}


[System.Serializable]
public class UsersDetails
{
    public List<Credential> users = new List<Credential>();
}