//This class can be used to check if a single input field is filled before moving on
//One example case is LNI instructions where we make sure child name is filled
using TMPro;

public class Validation_SingleInput : Validation_Parent
{
    public TMP_InputField singleInput;

    public override bool Validator()
    {
        if (singleInput.text == string.Empty)
            return false;

        else return true;
    }
}
