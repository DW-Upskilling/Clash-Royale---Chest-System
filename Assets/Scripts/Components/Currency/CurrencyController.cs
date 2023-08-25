using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.Managers;

namespace DevelopersWork.ChestSystem.Components.Currency
{
    public class CurrencyController
    {
        SessionManager sessionManager;

        CurrencyModel currencyModel;
        CurrencyView currencyView;

        public int Value { get { return currencyModel.Value; } }

        public CurrencyController(CurrencyScriptableObject currencyScriptableObject, Transform currencyContainerTransform) {
            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");

            currencyModel = new CurrencyModel(currencyScriptableObject);
            currencyView = GameObject.Instantiate<CurrencyView>(currencyScriptableObject.CurrencyPrefab, Vector3.zero, Quaternion.identity);
            currencyView.transform.SetParent(currencyContainerTransform);

            currencyView.SetImage(currencyModel.SourceImage);
            currencyView.SetText(currencyModel.Value.ToString());
        }

        public bool Increment(int value)
        {
            if (currencyModel.Value + value < int.MaxValue)
            {
                currencyModel.Value += value;
                currencyView.SetText(currencyModel.Value.ToString());
                return true;
            }

            return false;
        }

        public bool Decrement(int value)
        {
            if (currencyModel.Value - value >= 0)
            {
                currencyModel.Value -= value;
                currencyView.SetText(currencyModel.Value.ToString());
                return true;
            }

            return false;
        }
    }
}

