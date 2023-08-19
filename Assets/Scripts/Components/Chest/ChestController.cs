using System;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Components.Chest;
using Assets.Scripts.ScriptableObjects;

public class ChestController
{
    ChestModel chestModel;
    ChestView chestView;

    public ChestState ChestState { get { return chestModel.ChestState; } }

    public ChestController(ChestScriptableObject chestScriptableObject)
    {
        chestModel = new ChestModel(chestScriptableObject);
        chestView = GameObject.Instantiate<ChestView>(chestScriptableObject.ChestViewPrefab, Vector3.zero, Quaternion.identity);

        chestView.SetChestController(chestView, this);

        SetChestState(ChestState.Locked);
        ConvertTimeLeftToSeconds();
    }

    public void Update() { 
        
    }

    public void SetChestState(ChestState nextChestState)
    {
        chestModel.ChestState = nextChestState;
        chestModel.lastStateModifiedTimestamp = DateTime.Now;
        
        chestView.UpdateChestState();
    }

    void ConvertTimeLeftToSeconds()
    {
        switch (chestModel.ChestUnlockTimeLeftType)
        {
            case TimeType.Days:
                chestModel.ChestUnlockTimeLeftType = TimeType.Hours;
                chestModel.ChestUnlockTimeLeft = chestModel.ChestUnlockTimeLeft * 24;
                ConvertTimeLeftToSeconds();
                break;
            case TimeType.Hours:
                chestModel.ChestUnlockTimeLeftType = TimeType.Minutes;
                chestModel.ChestUnlockTimeLeft = chestModel.ChestUnlockTimeLeft * 60;
                ConvertTimeLeftToSeconds();
                break;
            case TimeType.Minutes:
                chestModel.ChestUnlockTimeLeftType = TimeType.Seconds;
                chestModel.ChestUnlockTimeLeft = chestModel.ChestUnlockTimeLeft * 60;
                ConvertTimeLeftToSeconds();
                break;
            case TimeType.Seconds:
                break;
        }
    }
}

