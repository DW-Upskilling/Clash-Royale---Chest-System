using UnityEngine;

namespace DevelopersWork.ChestSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RewardScriptableObjectList", menuName = "Scriptable Object/Reward/List")]
    public class RewardScriptableObjectList : ScriptableObject
    {
        [SerializeField]
        RewardScriptableObject[] rewardsList;
        public RewardScriptableObject[] RewardsList { get { return rewardsList; } }
    }
}