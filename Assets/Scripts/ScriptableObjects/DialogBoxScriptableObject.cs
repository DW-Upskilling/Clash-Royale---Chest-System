using UnityEngine;
using UnityEngine.UI;

using DevelopersWork.ChestSystem.Components.DialogBox;

namespace DevelopersWork.ChestSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DialogBoxScriptableObject", menuName = "Scriptable Object/DialogBox")]
    public class DialogBoxScriptableObject : ScriptableObject
    {
        [SerializeField]
        string dialogBoxName;
        public string DialogBoxName { get { return dialogBoxName; } }

        [SerializeField]
        DialogBoxType dialogBoxType;
        public DialogBoxType DialogBoxType { get { return dialogBoxType; } }

        [SerializeField]
        DialogBoxView dialogBoxPrefab;
        public DialogBoxView DialogBoxPrefab { get { return dialogBoxPrefab; } }

        [SerializeField]
        Button actionButtonPrefab;
        public Button ActionButtonPrefab { get { return actionButtonPrefab; } }
    }
}