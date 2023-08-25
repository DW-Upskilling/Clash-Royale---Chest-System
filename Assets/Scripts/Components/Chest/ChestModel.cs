using System;
using System.Collections.Generic;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.Components.Slot;

namespace DevelopersWork.ChestSystem.Components.Chest
{
    public class ChestModel
    {
        public string ChestName { get; private set; }
        public ChestType ChestType { get; private set; }
        public float ChestProbability { get; private set; }
        public TimeType ChestUnlockTimeType { get; private set; }
        public float ChestUnlockTime { get; private set; }
        public RewardScriptableObjectList RewardScriptableObjectList { get; private set; }

        public SlotController SlotController { get; set; }
        public bool IsQueued { get; set; }
        public ChestState ChestState { get; set; }
        public TimeType ChestUnlockTimeLeftType { get; set; }
        public float ChestUnlockTimeLeft { get; set; }
        public DateTime lastStateModifiedTimestamp { get; set; }

        public ChestModel(ChestScriptableObject chestScriptableObject)
        {
            ChestName = chestScriptableObject.ChestName;

            ChestType = chestScriptableObject.ChestType;

            ChestProbability = chestScriptableObject.ChestProbability;

            ChestUnlockTimeType = chestScriptableObject.ChestUnlockTimeType;
            ChestUnlockTime = chestScriptableObject.ChestUnlockTime;

            RewardScriptableObjectList = chestScriptableObject.RewardScriptableObjectList;

            ChestUnlockTimeLeftType = chestScriptableObject.ChestUnlockTimeType;
            ChestUnlockTimeLeft = chestScriptableObject.ChestUnlockTime;

            lastStateModifiedTimestamp = DateTime.Now;

            IsQueued = false;
        }
    }
}
