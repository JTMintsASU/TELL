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

    public static Credential convertToCredential(SerialData inputData)
    {
        Credential outputCred = new Credential();
        outputCred.record_id = String.IsNullOrEmpty(inputData.sRecordId) ? int.MaxValue : int.Parse(inputData.sRecordId);
        outputCred.assessor_id = inputData.sAssessorID;
        outputCred.child_id = inputData.sChildID;
        outputCred.classroom_id = inputData.sClassroomID;
        outputCred.teacher_id = inputData.sTeacherID;
        return outputCred;
    }
}


[System.Serializable]
public class UsersDetails
{
    public List<Credential> users = new List<Credential>();
}