using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DevelopersWork.ChestSystem.GenericClasses;
using DevelopersWork.ChestSystem.Handlers;

using DevelopersWork.ChestSystem.Components.Currency;
using DevelopersWork.ChestSystem.Components.Chest;
using DevelopersWork.ChestSystem.Components.DialogBox;
using DevelopersWork.ChestSystem.Components.Slot;

namespace DevelopersWork.ChestSystem.Managers
{
    public class SessionManager : Singleton<SessionManager>
    {
        GameManager gameManager;

        List<SlotController> slotControllers;
        Queue<ChestController> chestUnlockingQueue;

        public List<SlotModel> ChestSlots { get; private set; }
        public List<bool> UnlockingChestSlots { get; private set; }

        int currentUnlockingChestSlots;

        protected override void Initialize()
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");

            currentUnlockingChestSlots = gameManager.UnlockingSlots;

            slotControllers = new List<SlotController>();
            for (int i = 0; i < gameManager.TotalSlots; i++)
            {
                slotControllers.Add(new SlotController(gameManager.SlotPrefab, i));
            }

            chestUnlockingQueue = new Queue<ChestController>();
        }

        public SlotController GetAvailableSlot()
        {
            if (slotControllers.FindAll(e => e.IsSlotAvailable == false).Count >= gameManager.TotalSlots)
                return null;
            
            return slotControllers.Find(e => e.IsSlotAvailable == true);
        }

        public bool IsQueueingSlotsAvailable() {
            return chestUnlockingQueue.Count < gameManager.QueueSlots;
        }

        public bool AddChestToUnlockingQueue(ChestController chestController)
        {
            if (!IsQueueingSlotsAvailable())
                return false;
            chestUnlockingQueue.Enqueue(chestController);
            return true;
        }

        public ChestController GetChestFromUnlockingQueue()
        {
            if (chestUnlockingQueue.Count == 0)
                return null;
            return chestUnlockingQueue.Dequeue();
        }

        public bool IsChestUnlockingSlotAvailable()
        {
            return currentUnlockingChestSlots > 1;
        }

        public bool UseUnlockingSlot()
        {
            if (IsChestUnlockingSlotAvailable())
            {
                currentUnlockingChestSlots--;
                return true;
            }
            return false;
        }

        public void ReturnUnlockingSlot()
        {
            currentUnlockingChestSlots++;
        }
    }
}
