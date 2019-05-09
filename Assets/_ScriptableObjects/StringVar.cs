using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringVar", menuName = "ScriptableObjects/String Variable", order = 51)]
public class StringVar : ScriptableObject, ISerializationCallbackReceiver
{
    public string InitialValue = "";

    [NonSerialized]
    public string Value;

    public void OnAfterDeserialize()
    {
        Value = InitialValue;
    }

    public void OnBeforeSerialize() { }
}