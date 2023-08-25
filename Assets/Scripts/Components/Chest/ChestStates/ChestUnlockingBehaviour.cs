using UnityEngine;

using DevelopersWork.ChestSystem.Handlers;

namespace DevelopersWork.ChestSystem.Components.Chest.ChestStates
{
    public class ChestUnlockingBehaviour : StateMachineBehaviour
    {
        ChestView chestView;
        ChestController chestController;

        ChestEventHandler chestEventHandler;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            chestView = animator.gameObject.GetComponent<ChestView>();
            if (chestView != null)
                chestController = chestView.GetChestController(animator.gameObject);

            chestEventHandler = ChestEventHandler.Instance;
            if (chestEventHandler == null)
                throw new MissingReferenceException("ChestEventHandler instance not found!");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            chestController.DecrementTimeLeftToUnlock(Time.deltaTime);
            chestView.ResetText();
            if (chestController.ChestUnlockTimeLeft <= 0)
            {
                chestController.SetChestState(ChestState.Unlocked);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            chestController.ReturnUnlockingSlot();
            chestEventHandler.TriggerEvent();
        }
    }
}
