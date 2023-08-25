using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.Managers;

namespace DevelopersWork.ChestSystem.Components.Currency
{
    public class CurrencyModel
    {
        public string CurrencyName { get; private set; }
        public CurrencyType CurrencyType { get; private set; }
        public CurrencyView CurrencyPrefab { get; private set; }
        public Sprite SourceImage { get; private set; }

        public int Value
        {
            get { return PlayerPrefs.GetInt("Currency_" + CurrencyName, 0); }
            set { PlayerPrefs.SetInt("Currency_" + CurrencyName, value); }
        }

        public CurrencyModel(CurrencyScriptableObject currencyScriptableObject)
        {
            CurrencyName = currencyScriptableObject.CurrencyName;
            
            CurrencyType = currencyScriptableObject.CurrencyType;
            
            CurrencyPrefab = currencyScriptableObject.CurrencyPrefab;
            
            SourceImage = currencyScriptableObject.SourceImage;
        }
    }
}

