using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "Scriptable Object/Chest")]
public class ChestScriptableObject : ScriptableObject
{
    [SerializeField]
    string chestName;
    public string ChestName { get { return chestName; } }

    [SerializeField]
    ChestType chestType;
    public ChestType ChestType { get { return chestType; } }

    [SerializeField, Range(0f, 1f)]
    float chestProbability;
    public float ChestProbability { get { return chestProbability; } }

    [SerializeField]
    TimeType unlockTimeType;
    public TimeType UnlockTimeType { get { return unlockTimeType; } }

    [SerializeField]
    float unlockTime;
    public float UnlockTime { get { return unlockTime; } }

    [SerializeField]
    List<Reward> rewardsList;
    public List<Reward> RewardsList { get { return rewardsList; } }
}