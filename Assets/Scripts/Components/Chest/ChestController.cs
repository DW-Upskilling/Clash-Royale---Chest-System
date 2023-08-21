using System;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Components.Chest;
using Assets.Scripts.ScriptableObjects;

public class ChestController
{
    GameManager gameManager;
    SessionManager sessionManager;

    Slot slot;
    public Slot Slot {get{return slot;}}

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

        sessionManager = SessionManager.Instance;
        if (sessionManager == null)
            throw new MissingReferenceException("SessionManager instance not found!");

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
        if(chestModel.ChestState == ChestState.Locked && sessionManager.UseUnlockingSlot(slot))
        {
            SetChestState(ChestState.Unlocking);
        } 
        else if (chestModel.ChestState == ChestState.Unlocking)
        {
            if(sessionManager.UseCurrency(CurrencyType.Gem, GemsRequiredToCompleteUnlocking))
            {
                chestModel.ChestUnlockTimeLeft = 0;
            }
        }
        else if (chestModel.ChestState == ChestState.Unlocked) {
            SetChestState(ChestState.Collected);
        }
    }

    public int GemsRequiredToCompleteUnlocking { get {
            if(chestModel.ChestState == ChestState.Unlocking)
            return Mathf.CeilToInt(ChestUnlockTimeLeft / 600);
            return int.MaxValue;
    } }

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

