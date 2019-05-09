using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVar", menuName = "ScriptableObjects/Int Variable", order = 51)]
public class IntVar : ScriptableObject, ISerializationCallbackReceiver
{
    public int InitialValue = 0;

    [NonSerialized]
    public int Value;

    public void OnAfterDeserialize()
    {
        Value = InitialValue;
    }

    public void OnBeforeSerialize() { }
}
