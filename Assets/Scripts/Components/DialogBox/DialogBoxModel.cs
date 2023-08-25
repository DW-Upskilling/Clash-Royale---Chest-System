using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DevelopersWork.ChestSystem.ScriptableObjects;

namespace DevelopersWork.ChestSystem.Components.DialogBox
{
    public class DialogBoxModel
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public DialogBoxType DialogBoxType { get; private set; }
        public Button ActionButtonPrefab { get; private set; }

        public DialogBoxModel(DialogBoxScriptableObject dialogBoxScriptableObject, string title, string message)
        {
            DialogBoxType = dialogBoxScriptableObject.DialogBoxType;
            ActionButtonPrefab = dialogBoxScriptableObject.ActionButtonPrefab;

            Title = title;
            Message = message;
        }
    }
}
