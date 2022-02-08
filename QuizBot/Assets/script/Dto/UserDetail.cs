using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserDetail
{
    public string record_id;
    public string teacher_id;
    public string assessor_id;
    public string child_id;
    public string classroom_id;

    public static UserDetail fromSerialData(SerialData serialData)
    {
        UserDetail userDetail = new UserDetail();
        userDetail.record_id = Random.Range(10, 1000).ToString();
        userDetail.assessor_id = serialData.sAssessorID;
        userDetail.child_id = serialData.sChildID;
        userDetail.classroom_id = serialData.sClassroomID;
        userDetail.teacher_id = serialData.sTeacherID;
        return userDetail;
    }
}


[System.Serializable]
public class UsersDetails
{
    public List<UserDetail> users = new List<UserDetail>();
}