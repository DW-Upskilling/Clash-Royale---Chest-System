using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DevelopersWork.ChestSystem.Components.Currency
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI currencyValueText;

        [SerializeField]
        Image currencyImage;

        public void SetImage(Sprite sourceImage) {
            currencyImage.sprite = sourceImage;
        }

        public void SetText(string text)
        {
            currencyValueText.text = text;
        }
    }
}