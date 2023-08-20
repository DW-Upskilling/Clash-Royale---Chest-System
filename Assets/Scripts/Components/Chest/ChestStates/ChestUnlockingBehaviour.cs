using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Components.Chest;

public class ChestUnlockingBehaviour : StateMachineBehaviour
{
    ChestView chestView;
    ChestController chestController;
    ChestModel chestModel;

    GameManager gameManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager instance not found!");
        
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
        if(chestModel.ChestUnlockTimeLeft <= 0)
        {
            chestController.SetChestState(ChestState.Unlocked);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager.GiveUnlockingSlot();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
