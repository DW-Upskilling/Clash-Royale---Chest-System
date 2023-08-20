using System;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Components.Chest;
using Assets.Scripts.ScriptableObjects;

public class ChestController
{
    GameManager gameManager;

    Slot slot;

    ChestModel chestModel;
    ChestView chestView;

    public ChestState ChestState { get { return chestModel.ChestState; } }
    public string ChestName { get { return chestModel.ChestName; } }

    public float ChestUnlockTimeLeft { get { return chestModel.ChestUnlockTimeLeft; } }

    public ChestController(ChestScriptableObject chestScriptableObject, Slot _slot)
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager instance not found!");

        chestModel = new ChestModel(chestScriptableObject);
        chestView = GameObject.Instantiate<ChestView>(chestScriptableObject.ChestViewPrefab, Vector3.zero, Quaternion.identity);
        chestView.SetChestController(this);

        chestView.gameObject.transform.SetParent(gameManager.ChestSlotContainer.transform);

        slot = _slot;

        SetChestState(ChestState.Locked);
        ConvertTimeLeftToSeconds();
    }

    public void Update()
    {
    }

    public void OnDestroy()
    {

       slot.IsOccupied = false;
        
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

    public void triggerClick()
    {
        if(chestModel.ChestState == ChestState.Locked && gameManager.GetUnlockingSlot())
        {
            SetChestState(ChestState.Unlocking);
        }
    }

    public ChestModel GetChestModel(GameObject gameObject)
    {
        // Return ChestModel only if component of same gameObject requests
        if (gameObject.GetComponent<ChestView>() != null)
        {
            return chestModel;
        }

        return null;
    }
}

