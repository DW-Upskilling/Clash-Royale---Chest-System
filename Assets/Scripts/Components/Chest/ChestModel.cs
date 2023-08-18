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

    public ChestModel(ChestScriptableObject chestScriptableObject)
    {
        ChestName = chestScriptableObject.ChestName;

        ChestType = chestScriptableObject.ChestType;

        ChestProbability = chestScriptableObject.ChestProbability;

        ChestUnlockTimeType = chestScriptableObject.ChestUnlockTimeType;
        ChestUnlockTime = chestScriptableObject.ChestUnlockTime;

        RewardsList = chestScriptableObject.RewardsList;
    }
}
