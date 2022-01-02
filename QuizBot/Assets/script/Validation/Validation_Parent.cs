//This parent class allows scripts to reference any kind of validator
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Validation_Parent : MonoBehaviour
{
    //Return a bool result determining if input is valid or not
    public abstract bool Validator();
}
