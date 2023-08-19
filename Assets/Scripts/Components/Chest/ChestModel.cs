using System;
using System.Collections.Generic;

using Assets.Scripts.ScriptableObjects;

public class ChestModel
{
    public string ChestName { get; private set; }
    public ChestType ChestType { get; private set; }
    public float ChestProbability { get; private set; }
    public TimeType ChestUnlockTimeType { get; private set; }
    public float ChestUnlockTime { get; private set; }
    public List<Reward> RewardsList { get; private set; }

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

        RewardsList = chestScriptableObject.RewardsList;

        ChestUnlockTimeLeftType = chestScriptableObject.ChestUnlockTimeType;
        ChestUnlockTimeLeft = chestScriptableObject.ChestUnlockTime;

        lastStateModifiedTimestamp = DateTime.Now;
    }
}
