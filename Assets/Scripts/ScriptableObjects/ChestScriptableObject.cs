using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScriptableObject : ScriptableObject
{
    [SerializeField]
    string chestName;
    public string ChestName { get { return chestName; } }
}
