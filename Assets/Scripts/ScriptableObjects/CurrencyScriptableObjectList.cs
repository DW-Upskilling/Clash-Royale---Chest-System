using UnityEngine;

namespace DevelopersWork.ChestSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CurrencyScriptableObjectList", menuName = "Scriptable Object/Currency/List")]
    public class CurrencyScriptableObjectList : ScriptableObject
    {
        [SerializeField]
        CurrencyScriptableObject[] currencyList;
        public CurrencyScriptableObject[] CurrencyList { get { return currencyList; } }
    }
}