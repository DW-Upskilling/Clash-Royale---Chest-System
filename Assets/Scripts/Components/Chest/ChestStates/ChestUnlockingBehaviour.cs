using UnityEngine;

namespace Assets.Scripts.Components.Chest.ChestStates
{
    public class ChestUnlockingBehaviour : StateMachineBehaviour
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
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            chestModel.ChestUnlockTimeLeft -= Time.deltaTime;
            chestView.ResetText();
            if (chestModel.ChestUnlockTimeLeft <= 0)
            {
                chestController.SetChestState(ChestState.Unlocked);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            sessionManager.ReturnUnlockingSlot(chestController.Slot);
        }
    }
}
