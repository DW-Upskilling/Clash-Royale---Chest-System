using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;

using DevelopersWork.ChestSystem.Components.Currency;

namespace DevelopersWork.ChestSystem.Components.Reward
{
    public class RewardController{

        RewardModel rewardModel;

        CurrencyService currencyService;

        public RewardController(RewardScriptableObject rewardScriptableObject)
        {
            rewardModel = new RewardModel(rewardScriptableObject);

            currencyService = CurrencyService.Instance;
            if (currencyService == null)
                throw new MissingReferenceException("CurrencyService instance not found!");
        }

        public void DropRoll()
        {
            int amount = getRewardAmount();
            switch (rewardModel.RewardType)
            {
                case RewardType.Currency:
                    CurrencyScriptableObject currencyScriptableObject = rewardModel.CurrencyScriptableObject;
                    CurrencyType currencyType = currencyScriptableObject.CurrencyType;
                    currencyService.GetCurrencyControllerByType(currencyType).Increment(amount);
                    break;
                case RewardType.Card:
                    break;
            }
        }

        bool isRewardReceived()
        {
            float magicFind = Random.value;
            return magicFind <= rewardModel.RewardProbability;
        }

        int getRewardAmount() {
            if (isRewardReceived())
            {
                return Random.Range(rewardModel.MinRewardAmount, rewardModel.MaxRewardAmount);
            }
            return 0;
        }

    }
}