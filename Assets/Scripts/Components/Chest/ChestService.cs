using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.GenericClasses;
using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.Handlers;

using DevelopersWork.ChestSystem.Components.Slot;
using DevelopersWork.ChestSystem.Components.DialogBox;

namespace DevelopersWork.ChestSystem.Components.Chest
{
    public class ChestService : Singleton<ChestService>
    {
        [SerializeField]
        List<ChestScriptableObject> chestScriptableObjectList;

        SessionManager sessionManager;
        ChestEventHandler chestEventHandler;
        DialogBoxService dialogBoxService;

        protected override void Initialize()
        {
            if (chestScriptableObjectList == null || chestScriptableObjectList.Count == 0)
                throw new MissingReferenceException("ChestScriptableObjectList not provided");

            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");
        }

        void Start()
        {
            dialogBoxService = DialogBoxService.Instance;
            if (dialogBoxService == null)
                throw new MissingReferenceException("DialogBoxService instance not found!");
            chestEventHandler = ChestEventHandler.Instance;
            if (chestEventHandler == null)
                throw new MissingReferenceException("ChestEventHandler instance not found!");

            chestEventHandler.AddListener(HandleQueue);
        }

        void OnDestroy()
        {
            chestEventHandler.RemoveListener(HandleQueue);
        }

        void HandleQueue()
        {
            ChestController chestController = sessionManager.GetChestFromUnlockingQueue();
            if(chestController != null)
            {
                if (sessionManager.UseUnlockingSlot())
                {
                    chestController.SetChestState(ChestState.Unlocking);
                }
            }
        }

        public ChestController CreateChest()
        {
            SlotController slotController = sessionManager.GetAvailableSlot();
            if (slotController == null)
            {
                dialogBoxService.Information("Chest Slot Alert", "Hold on a moment! It seems all the chest slots are currently occupied.").Show();
                return null;
            }

            ChestScriptableObject chestScriptableObject = GetChestScriptableObjectRandom();
            if (chestScriptableObject == null)
                return null;
            
            return new ChestController(chestScriptableObject, slotController);
        }

        ChestScriptableObject GetChestScriptableObjectRandom()
        {
            float totalProbability = chestScriptableObjectList.Aggregate(0f, (accumulator, next) => accumulator + next.ChestProbability);

            System.Random random = new System.Random();
            float probability = (float)random.NextDouble() * totalProbability;

            float differnce = int.MaxValue;
            int index = -1;

            for (int i = 0; i < chestScriptableObjectList.Count; i++)
            {
                ChestScriptableObject chestScriptableObject = chestScriptableObjectList[i];
                float currentDiffernce = Math.Abs(chestScriptableObject.ChestProbability - probability);
                if (differnce > currentDiffernce)
                {
                    differnce = currentDiffernce;
                    index = i;
                }
            }
            return chestScriptableObjectList[index];
        }

    }
}
