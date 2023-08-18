using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Components.Chest;

namespace Assets.Scripts.ScriptableObjects
{
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
        TimeType chestUnlockTimeType;
        public TimeType ChestUnlockTimeType { get { return chestUnlockTimeType; } }

        [SerializeField]
        float chestUnlockTime;
        public float ChestUnlockTime { get { return chestUnlockTime; } }

        [SerializeField]
        List<Reward> rewardsList;
        public List<Reward> RewardsList { get { return rewardsList; } }

        [SerializeField]
        ChestView chestViewPrefab;
        public ChestView ChestViewPrefab { get { return chestViewPrefab; } }
    }
}