using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DevelopersWork.ChestSystem.GenericClasses;
using DevelopersWork.ChestSystem.ScriptableObjects;

using DevelopersWork.ChestSystem.Components.Chest;
using DevelopersWork.ChestSystem.Components.DialogBox;
using DevelopersWork.ChestSystem.Components.Slot;

namespace DevelopersWork.ChestSystem.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        int totalSlots = 4;
        public int TotalSlots { get { return totalSlots; } }

        [SerializeField]
        int unlockingSlots = 1;
        public int UnlockingSlots { get { return unlockingSlots; } }

        [SerializeField]
        int queueSlots = 2;
        public int QueueSlots { get { return queueSlots; } }

        [SerializeField]
        SlotView slotPrefab;
        public SlotView SlotPrefab { get { return slotPrefab; } }

        [SerializeField]
        GridLayoutGroup chestSlotContainer;
        public GameObject ChestSlotContainer { get { return chestSlotContainer.gameObject; } }

        [SerializeField]
        TextMeshProUGUI TimeText;

        protected override void Initialize()
        {
            if (chestSlotContainer == null)
                throw new MissingReferenceException("chestSlotContainer not provided");

            if (slotPrefab == null)
                throw new MissingReferenceException("SlotPrefab not provided");
        }

        private void Start()
        {
            ResetTimeTexts();
        }

        void ResetTimeTexts()
        {
            TimeText.text = Time.timeScale + "x";
        }

        // Below methods used during development to simulate faster results
        public void AccelerateTime()
        {
            // 100x times is maximum speed of fast forwarding
            Time.timeScale = Mathf.Min(100, Time.timeScale + 1);
            ResetTimeTexts();
        }
        public void DecelerateTime()
        {
            // 1x times is minimum speed of fast forwarding
            Time.timeScale = Mathf.Max(1, Time.timeScale - 1);
            ResetTimeTexts();
        }
    }
}
