using System;

using DevelopersWork.ChestSystem.ScriptableObjects;

namespace DevelopersWork.ChestSystem.Components.Reward
{
    [Serializable]
    public class RewardModel
    {
        public string RewardName { get; private set; }
        public RewardType RewardType { get; private set; }
        public float RewardProbability { get; private set; }
        public int MinRewardAmount { get; private set; }
        public int MaxRewardAmount { get; private set; }
        public CurrencyScriptableObject CurrencyScriptableObject { get; private set; }
        public string CardName { get; private set; }

        public RewardModel(RewardScriptableObject rewardScriptableObject)
        {
            RewardName = rewardScriptableObject.RewardName;
            RewardType = rewardScriptableObject.RewardType;
            RewardProbability = rewardScriptableObject.RewardProbability;
            
            MinRewardAmount = rewardScriptableObject.MinRewardAmount;
            MaxRewardAmount = rewardScriptableObject.MaxRewardAmount;
            
            CurrencyScriptableObject = rewardScriptableObject.CurrencyScriptableObject;
            
            CardName = rewardScriptableObject.CardName;
        }
    }
}

