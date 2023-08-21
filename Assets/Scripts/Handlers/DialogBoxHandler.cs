using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Handlers
{
    public class DialogBoxHandler : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI titleText;
        public string TitleText { get { return titleText.text; } set { titleText.text = value; } }

        [SerializeField]
        TextMeshProUGUI messageText;
        public string MessageText { get { return messageText.text; } set { messageText.text = value; } }

        public void CloseDialogBox()
        {
            Destroy(gameObject);
        }
    }
}