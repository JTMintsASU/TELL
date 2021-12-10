//This class is used to 'skip' Validation
//on scenes without any input to check.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Validation_Skip : Validation_Parent
{
    public override bool Validate_Input()
    {
        throw new System.NotImplementedException();
    }

    //Since there's nothing to check,
    //we'll just say the input is valid
    public override bool Validator()
    {
        return true;
    }
}
