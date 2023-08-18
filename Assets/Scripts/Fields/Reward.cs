using System;
using UnityEngine;

using Assets.Scripts.ScriptableObjects;

[Serializable]
public class Reward
{
    [SerializeField]
    string rewardName;
    public string RewardName { get { return rewardName; } }

    [SerializeField]
    RewardType rewardType;
    public RewardType RewardType { get { return rewardType; } }

    [SerializeField, Range(0f, 1f)]
    float rewardProbability;
    public float RewardProbability { get { return rewardProbability; } }

    [SerializeField]
    int minRewardAmount;
    public int MinRewardAmount { get { return Mathf.Min(minRewardAmount, maxRewardAmount); } }

    [SerializeField]
    int maxRewardAmount;
    public int MaxRewardAmount { get { return Mathf.Max(minRewardAmount, maxRewardAmount); } }

    [SerializeField]
    CurrencyScriptableObject currencyScriptableObject;
    public CurrencyScriptableObject CurrencyScriptableObject { get { return currencyScriptableObject; } }

    [SerializeField]
    string cardName;
    public string CardName { get { return cardName; } }
}

