using UnityEngine;
using UnityEngine.UI;

using DevelopersWork.ChestSystem.Components.Currency;

namespace DevelopersWork.ChestSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CurrencyScriptableObject", menuName = "Scriptable Object/Currency/New")]
    public class CurrencyScriptableObject : ScriptableObject
    {
        [SerializeField]
        string currencyName;
        public string CurrencyName { get { return currencyName; } }

        [SerializeField]
        CurrencyType currencyType;
        public CurrencyType CurrencyType { get { return currencyType; } }

        [SerializeField]
        CurrencyView currencyPrefab;
        public CurrencyView CurrencyPrefab { get { return currencyPrefab; } }

        [SerializeField]
        Sprite sourceImage;
        public Sprite SourceImage { get { return sourceImage; } }
    }
}
