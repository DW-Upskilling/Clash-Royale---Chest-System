using UnityEditor;

using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Editor.ScriptableObjects
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

