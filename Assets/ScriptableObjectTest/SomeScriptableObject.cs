using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSomeScriptableObject", menuName = "Custom/SomeScriptableObject")]
public class SomeScriptableObject : ScriptableObject
{
    public int dataToStore;
    public float relevantValue;
}
