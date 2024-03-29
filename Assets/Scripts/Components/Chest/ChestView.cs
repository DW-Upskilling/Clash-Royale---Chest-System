using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DevelopersWork.ChestSystem.Components.Chest
{
    [RequireComponent(typeof(Animator), typeof(Button))]
    public class ChestView : MonoBehaviour
    {
        ChestController chestController;

        Animator animator;
        Button button;
        
        [SerializeField]
        TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            button = gameObject.GetComponent<Button>();
        }

        private void Start()
        {
            if (chestController != null)
                chestController.Start();
        }

        private void OnDestroy()
        {
            if (chestController != null)
                chestController.OnDestroy();
        }

        public void SetChestController(ChestController _chestController)
        {
            chestController = _chestController;
            button.onClick.AddListener(chestController.Trigger);
        }

        public ChestController GetChestController(GameObject gameObject)
        {
            if(this.gameObject == gameObject)
                return chestController;

            return null;
        }

        public void UpdateChestState()
        {
            if (chestController != null)
            {
                ResetText();

                animator.SetInteger("currentState", (int)chestController.ChestState);
            }
        }

        public void ResetText() {
            if (textMeshProUGUI == null)
                return;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(chestController.ChestName);

            stringBuilder.Append("\n");

            stringBuilder.Append(Enum.GetName(typeof(ChestState), chestController.ChestState));

            stringBuilder.Append("\n");

            if (chestController.ChestState == ChestState.Unlocking)
            {
                int totalSeconds = (int)chestController.ChestUnlockTimeLeft;

                int hours = totalSeconds / 3600; 
                int minutes = (totalSeconds % 3600) / 60; 
                int seconds = totalSeconds % 60;

                stringBuilder.Append($"{hours:D2}hr {minutes:D2}min {seconds:D2}sec");
                stringBuilder.Append("\n");

                stringBuilder.Append($"Unlock now with {chestController.RequiredGemsToUnlockInstant} Gems!");
                stringBuilder.Append("\n");
            }

            textMeshProUGUI.text = stringBuilder.ToString();
        }
    }
}