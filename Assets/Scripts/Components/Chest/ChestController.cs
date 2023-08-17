using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    ChestModel chestModel;
    ChestView chestView;

    ChestScriptableObject chestScriptableObject;

    public ChestController(ChestScriptableObject _chestScriptableObject)
    {
        chestScriptableObject = _chestScriptableObject;

        chestModel = new ChestModel(chestScriptableObject);
        chestView = GameObject.Instantiate<ChestView>(chestScriptableObject.ChestViewPrefab, Vector3.zero, Quaternion.identity);
    }
}
