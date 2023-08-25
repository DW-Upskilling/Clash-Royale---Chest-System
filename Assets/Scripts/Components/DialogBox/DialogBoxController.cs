using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.ScriptableObjects;

namespace DevelopersWork.ChestSystem.Components.DialogBox
{
    public class DialogBoxController
    {
        GameManager gameManager;

        DialogBoxModel dialogBoxModel;
        DialogBoxView dialogBoxView;

        List<Button> actionButtons;

        public DialogBoxType DialogBoxType { get { return dialogBoxModel.DialogBoxType; } }
        public string Message { get { return dialogBoxModel.Message; } }
        public string Title { get { return dialogBoxModel.Title; } }

        public DialogBoxController(DialogBoxScriptableObject dialogBoxScriptableObject, string title, string message)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");

            dialogBoxModel = new DialogBoxModel(dialogBoxScriptableObject, title, message);

            dialogBoxView = GameObject.Instantiate<DialogBoxView>(dialogBoxScriptableObject.DialogBoxPrefab, Vector3.zero, Quaternion.identity);
            dialogBoxView.SetChestController(this);

            actionButtons = new List<Button>();
        }

        public void AddButton(string buttonName, UnityEngine.Events.UnityAction unityAction)
        {
            switch (dialogBoxModel.DialogBoxType)
            {
                case DialogBoxType.Information:
                    if (actionButtons.Count > 0)
                        break;
                    actionButtons.Add(CreateButton(buttonName, unityAction));
                    break;
                case DialogBoxType.Action:
                    if (actionButtons.Count > 1)
                        break;
                    actionButtons.Add(CreateButton(buttonName, unityAction));
                    break;
            }
        }

        public void Show()
        {
            foreach(Button button in actionButtons)
            {
                RectTransform rectTransform = button.GetComponent<RectTransform>();

                // Reset the RectTransform properties to their default values
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.localScale = Vector3.one;
            }
            dialogBoxView.gameObject.SetActive(true);
        }

        Button CreateButton(string displayText, UnityEngine.Events.UnityAction unityAction)
        {
            Button button = GameObject.Instantiate<Button>(dialogBoxModel.ActionButtonPrefab, Vector3.zero, Quaternion.identity);
            dialogBoxView.AddButtonToUI(button);

            button.onClick.AddListener(unityAction);

            TextMeshProUGUI textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
            if(textMeshProUGUI != null)
            {
                textMeshProUGUI.text = displayText;
            }

            return button;
        }

        public void CloseButtonAction()
        {
            GameObject.Destroy(dialogBoxView.gameObject);
        }
    }
}
