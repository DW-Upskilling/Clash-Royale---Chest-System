using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.Handlers;

using DevelopersWork.ChestSystem.Components.Reward;
using DevelopersWork.ChestSystem.Components.Currency;

namespace DevelopersWork.ChestSystem.Components.Chest.ChestStates
{
    public class ChestCollectedBehaviour : StateMachineBehaviour
    {
        ChestView chestView;
        ChestController chestController;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            chestView = animator.gameObject.GetComponent<ChestView>();
            if (chestView != null)
                chestController = chestView.GetChestController(animator.gameObject);

            handleChestRewardsCollection();
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(animator.gameObject);
        }

        void handleChestRewardsCollection() {
            RewardScriptableObjectList rewardScriptableObjectList = chestController.RewardScriptableObjectList;

            foreach(RewardScriptableObject rewardScriptableObject in rewardScriptableObjectList.RewardsList)
            {
                RewardController rewardController = new RewardController(rewardScriptableObject);
                rewardController.DropRoll();
            }

            chestController.SetChestState(ChestState.Destroyed);
        }
    }
}
