//This parent class allows scripts to reference any kind of validator
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Validation_Games : Validation_Parent
{
    //Return a bool result determining if input is valid or not for the scene
    //Note: this member is passed down unimplemented for children
    //public abstract bool Validator();

    //Implemented by game validators to check a single response
    public abstract bool GetValidInput();
}
