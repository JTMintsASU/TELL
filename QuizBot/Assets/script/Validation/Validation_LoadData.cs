using TMPro;

public class Validation_LoadData : Validation_UserInfo
{
    
    //Time to validate!
    //Requires one of each teacher, assessor, child and classroom field filled
    public override bool Validator()
    {
        bool valid = true; //Return result
        string empty = ""; //Used to more clearly indicate null strings

        if (teacherName.text == empty && teacherID.text == empty)
            valid = false;

        if (assessorName.text == empty && assessorID.text == empty)
            valid = false;

        if (childName.text == empty && childID.text == empty)
            valid = false;

        if (classRoomId.text == empty && classRoomName.text == empty)
            valid = false;

        return valid;
    }
    
}