using UnityEditor;

using DevelopersWork.ChestSystem.ScriptableObjects;

namespace DevelopersWork.ChestSystem.Editor.ScriptableObjects
{
    [CustomEditor(typeof(CurrencyScriptableObject))]
    public class ChestScriptableObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}

