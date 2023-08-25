using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DevelopersWork.ChestSystem.Components.DialogBox
{
    public class DialogBoxView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI titleText;

        [SerializeField]
        TextMeshProUGUI messageText;

        [SerializeField]
        GameObject actionButtonsContainer;

        DialogBoxController dialogBoxController;

        public void SetChestController(DialogBoxController dialogBoxController)
        {
            this.dialogBoxController = dialogBoxController;

            titleText.text = dialogBoxController.Title;
            messageText.text = dialogBoxController.Message;
        }

        public void AddButtonToUI(Button action) { 
            action.transform.SetParent(actionButtonsContainer.transform);
        }

        public DialogBoxController GetDialogBoxController(GameObject gameObject)
        {
            DialogBoxView dialogBoxView = gameObject.GetComponent<DialogBoxView>();

            // Return dialogBoxController only if component of same gameObject requests
            if (dialogBoxView == this)
            {
                return dialogBoxController;
            }

            return null;
        }

        private void OnDestroy()
        {
            
        }
    }
}
