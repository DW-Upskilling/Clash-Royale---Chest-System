using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Components.Chest
{
    [RequireComponent(typeof(Animator), typeof(Button))]
    public class ChestView : MonoBehaviour
    {
        ChestController chestController;

        Animator animator;
        Button button;
        TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            button = gameObject.GetComponent<Button>();
            textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if(chestController != null)
                chestController.Update();
        }

        private void OnDestroy()
        {
            if (chestController != null)
                chestController.OnDestroy();
        }

        public void SetChestController(ChestController _chestController)
        {
            chestController = _chestController;
            button.onClick.AddListener(chestController.triggerClick);
        }

        public ChestController GetChestController(GameObject gameObject)
        {
            // Return ChestController only if component of same gameObject requests
            if(gameObject.GetComponent<ChestView>() != null)
            {
                return chestController;
            }

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

                stringBuilder.Append($"{hours:D2}:{minutes:D2}:{seconds:D2}");

                stringBuilder.Append("\n");
            }

            textMeshProUGUI.text = stringBuilder.ToString();
        }
    }
}