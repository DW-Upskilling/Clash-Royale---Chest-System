using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyScriptableObject", menuName = "Scriptable Object/Currency")]
public class CurrencyScriptableObject : ScriptableObject
{
    [SerializeField]
    string currencyName;
    public string CurrencyName { get { return currencyName; } }

    [SerializeField]
    CurrencyType currencyType;
    public CurrencyType CurrencyType { get { return currencyType; } }
}
