using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.Chest
{
    [RequireComponent(typeof(Animator))]
    public class ChestView : MonoBehaviour
    {
        ChestController chestController;

        Animator animator;

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            chestController.Update();
        }

        public void SetChestController(ChestView chestView, ChestController _chestController)
        {
            if (chestView != this)
                return;

            chestController = _chestController;
        }

        public void UpdateChestState()
        {
            animator.SetInteger("currentState", (int)chestController.ChestState);
        }
    }
}