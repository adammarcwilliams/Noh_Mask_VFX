using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mask", menuName = "ScriptableObjects/Mask", order = 51)]
public class Mask : ScriptableObject
{
    public string MaskName;
    public AudioClip AudioDescription;
}
