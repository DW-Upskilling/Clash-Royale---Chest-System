using System;
using System.Collections.Generic;
using UnityEngine;

using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.ScriptableObjects;

using DevelopersWork.ChestSystem.Components.Currency;
using DevelopersWork.ChestSystem.Components.Slot;
using DevelopersWork.ChestSystem.Components.DialogBox;

namespace DevelopersWork.ChestSystem.Components.Chest
{
    public class ChestController
    {
        GameManager gameManager;
        SessionManager sessionManager;

        DialogBoxService dialogBoxService;

        CurrencyService currencyService;

        ChestModel chestModel;
        ChestView chestView;

        public ChestState ChestState { get { return chestModel.ChestState; } }
        public string ChestName { get { return chestModel.ChestName; } }
        public SlotController SlotController { get { return chestModel.SlotController; } }
        public RewardScriptableObjectList RewardScriptableObjectList { get { return chestModel.RewardScriptableObjectList;  } }

        public float ChestUnlockTimeLeft { get { return chestModel.ChestUnlockTimeLeft; } }

        public ChestController(ChestScriptableObject chestScriptableObject, SlotController slotController)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");

            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");

            chestModel = new ChestModel(chestScriptableObject);
            chestModel.SlotController = slotController;

            chestView = GameObject.Instantiate<ChestView>(chestScriptableObject.ChestViewPrefab, Vector3.zero, Quaternion.identity);
            chestView.transform.SetParent(gameManager.ChestSlotContainer.transform);
            chestView.transform.SetSiblingIndex(slotController.SequenceNumber);

            chestView.SetChestController(this);

            if (!slotController.UseSlot(this))
            {
                GameObject.Destroy(chestView.gameObject);
            }

            SetChestState(ChestState.Locked);
            ConvertTimeLeftToSeconds();
        }
        public void Start()
        {
            dialogBoxService = DialogBoxService.Instance;
            if (dialogBoxService == null)
                throw new MissingReferenceException("DialogBoxService instance not found!");

            currencyService = CurrencyService.Instance;
            if (currencyService == null)
                throw new MissingReferenceException("CurrencyService instance not found!");
        }

        public void OnDestroy()
        {
            chestModel.SlotController.ReleaseSlot(this);
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

        public void Trigger()
        {
            if (chestModel.ChestState == ChestState.Locked)
            {
                if (sessionManager.UseUnlockingSlot())
                {
                    DialogBoxController dialogBoxController = dialogBoxService.Action(
                        "Unlocking Options",
                        "1. Wait for "+chestModel.ChestUnlockTimeLeft+"seconds of time.\n2. Use "+ RequiredGemsToUnlockInstant + "Gems to instantly unlock."
                    );
                    dialogBoxController.AddButton("Regular Unlock", () => { 
                        SetChestState(ChestState.Unlocking);
                        dialogBoxController.CloseButtonAction();
                    });
                    if(RequiredGemsToUnlockInstant <= currencyService.GetCurrencyControllerByType(CurrencyType.Gem).Value)
                        dialogBoxController.AddButton("Instant Unlock", () => {
                            currencyService.GetCurrencyControllerByType(CurrencyType.Gem).Decrement(RequiredGemsToUnlockInstant);
                            SetChestState(ChestState.Unlocked);
                            dialogBoxController.CloseButtonAction();
                        });

                    dialogBoxController.Show();
                }
                else
                {
                    DialogBoxController dialogBoxController = dialogBoxService.Action(
                        "No Available Unlocking Slots",
                        "Currently, all slots are occupied. Please wait for existing chests to finish unlocking before starting new ones."
                    );
                    if(!chestModel.IsQueued && sessionManager.IsQueueingSlotsAvailable())
                    dialogBoxController.AddButton("Add to Queue", () => {
                        if (sessionManager.AddChestToUnlockingQueue(this))
                        {
                            chestModel.IsQueued = true;
                        }
                        dialogBoxController.CloseButtonAction();
                    });
                    dialogBoxController.AddButton("Close", () => {
                        dialogBoxController.CloseButtonAction();
                    });

                    dialogBoxController.Show();
                }
            }
            else if (chestModel.ChestState == ChestState.Unlocking)
            {
                
            }
            else if (chestModel.ChestState == ChestState.Unlocked)
            {
                SetChestState(ChestState.Collected);
            }
        }

        public void ReturnUnlockingSlot()
        {
            sessionManager.ReturnUnlockingSlot();
        }

        public int RequiredGemsToUnlockInstant
        {
            get
            {
                if (chestModel.ChestState == ChestState.Unlocking || chestModel.ChestState == ChestState.Locked)
                    return Mathf.CeilToInt(ChestUnlockTimeLeft / 600);
                return int.MaxValue;
            }
        }

        public void DecrementTimeLeftToUnlock(float value)
        {
            chestModel.ChestUnlockTimeLeft -= value;
        }
    }
}

