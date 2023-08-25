using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevelopersWork.ChestSystem.GenericClasses;
using DevelopersWork.ChestSystem.ScriptableObjects;

namespace DevelopersWork.ChestSystem.Components.DialogBox
{
    public class DialogBoxService: Singleton<DialogBoxService>
    {
        [SerializeField]
        List<DialogBoxScriptableObject> dialogBoxScriptableObjectList;

        protected override void Initialize()
        {
            if (dialogBoxScriptableObjectList == null || dialogBoxScriptableObjectList.Count == 0)
                throw new MissingReferenceException("DialogBoxScriptableObject not provided");
        }

        public DialogBoxController Information(string title, string message)
        {
            DialogBoxController dialogBoxController = new DialogBoxController(GetDialogBoxScriptableObjectByType(DialogBoxType.Information), title, message);
            dialogBoxController.AddButton("Close", dialogBoxController.CloseButtonAction);

            return dialogBoxController;
        }

        public DialogBoxController Action(string title, string message)
        {
            return new DialogBoxController(GetDialogBoxScriptableObjectByType(DialogBoxType.Action), title, message);
        }

        DialogBoxScriptableObject GetDialogBoxScriptableObjectByType(DialogBoxType dialogBoxType)
        {
            return dialogBoxScriptableObjectList.Find(e => e.DialogBoxType == dialogBoxType);
        }

    }
}
