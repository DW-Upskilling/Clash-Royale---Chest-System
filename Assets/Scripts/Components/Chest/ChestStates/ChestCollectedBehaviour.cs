using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Components.Chest.ChestStates
{
    public class ChestCollectedBehaviour : StateMachineBehaviour
    {
        ChestView chestView;
        ChestController chestController;
        ChestModel chestModel;

        GameManager gameManager;
        SessionManager sessionManager;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");
            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");

            chestView = animator.gameObject.GetComponent<ChestView>();
            if (chestView != null)
                chestController = chestView.GetChestController(animator.gameObject);
            if (chestController != null)
                chestModel = chestController.GetChestModel(animator.gameObject);

            rollChestForRewards();
        }

        void rollChestForRewards()
        {
            foreach(Reward reward in chestModel.RewardsList) {
                if (rollReward(reward))
                {
                    switch (reward.RewardType)
                    {
                        case RewardType.Currency:
                            awardCurrency(reward);
                            break;
                        case RewardType.Card:
                            break;
                    }
                }
            }

            chestController.SetChestState(ChestState.Destroyed);
        }

        bool rollReward(Reward reward)
        {
            float magicFind = Random.value;
            return magicFind <= reward.RewardProbability;
        }

        void awardCurrency(Reward reward)
        {
            int randomAmount = Random.Range(reward.MinRewardAmount, reward.MaxRewardAmount);

            CurrencyScriptableObject currencyScriptableObject = reward.CurrencyScriptableObject;
            sessionManager.IncrementCurrency(currencyScriptableObject.CurrencyType, randomAmount);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            sessionManager.ReturnEmptySlot(chestController.Slot);
            Destroy(animator.gameObject);
        }
    }
}
