using UnityEngine;

[System.Serializable]
public class UserDetail
{
    public string record_id;
    public string teacher_id;
    public string assessor_id;
    public string game;
    public string time;
    public string score;
}


[System.Serializable]
public class UsersDetails
{
    public UserDetail[] users;
}