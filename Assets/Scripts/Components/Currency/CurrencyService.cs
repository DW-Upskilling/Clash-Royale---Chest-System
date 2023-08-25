using System;
using System.Collections.Generic;
using UnityEngine;

using DevelopersWork.ChestSystem.ScriptableObjects;
using DevelopersWork.ChestSystem.GenericClasses;
using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.Handlers;

using DevelopersWork.ChestSystem.Components.Slot;
using DevelopersWork.ChestSystem.Components.DialogBox;

namespace DevelopersWork.ChestSystem.Components.Currency
{
    public class CurrencyService : Singleton<CurrencyService>
    {
        [SerializeField]
        CurrencyScriptableObjectList currencyScriptableObjectlist;

        [SerializeField]
        GameObject currencyContainer;

        SessionManager sessionManager;

        Dictionary<CurrencyType, CurrencyController> currencies;

        protected override void Initialize()
        {
            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");

            currencies = new Dictionary<CurrencyType, CurrencyController>();
            foreach (CurrencyScriptableObject currencyScriptableObject in currencyScriptableObjectlist.CurrencyList) {
                currencies.Add(currencyScriptableObject.CurrencyType, new CurrencyController(currencyScriptableObject, currencyContainer.transform));
            }
        }

        public CurrencyController GetCurrencyControllerByType(CurrencyType currencyType)
        {
            if (!currencies.ContainsKey(currencyType))
                return null;
            return currencies[currencyType];
        }
    }
}
