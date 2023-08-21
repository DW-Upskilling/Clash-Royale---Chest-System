using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Handlers
{
    [RequireComponent(typeof(Button))]
    public class ChestGenerateHandler : MonoBehaviour
    {

        GameManager gameManager;
        SessionManager sessionManager;
        
        Button generateChestButton;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");

            sessionManager = SessionManager.Instance;
            if (sessionManager == null)
                throw new MissingReferenceException("SessionManager instance not found!");

            generateChestButton = gameObject.GetComponent<Button>();
        }

        private void OnEnable()
        {
            generateChestButton.onClick.AddListener(generateChestButtonAction);
        }

        private void OnDisable()
        {
            generateChestButton.onClick.RemoveListener(generateChestButtonAction);
        }

        void generateChestButtonAction() {
            Slot chestSlot = sessionManager.GetEmptySlot();
            if (chestSlot == null)
                return;
                
            ChestScriptableObject chestScriptableObject = gameManager.GetChestScriptableObjectRandom();
            if (chestScriptableObject == null)
                return;

            chestSlot.ChestController = new ChestController(chestScriptableObject, chestSlot);
            chestSlot.IsOccupied = true;
        }
    }
}