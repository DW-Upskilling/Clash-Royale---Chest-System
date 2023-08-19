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
            Slot chestSlot = gameManager.GetEmptySlot();
            if (chestSlot == null)
            {
                Debug.Log("Currently no empty chest slot available");
                return;
            }
                
            ChestType randomChestType = (ChestType)Random.Range(1f, 4f);
            ChestScriptableObject chestScriptableObject = gameManager.GetChestScriptableObjectByType(randomChestType);
            
            chestSlot.IsOccupied = true;
            chestSlot.ChestController = new ChestController(chestScriptableObject);
        }
    }
}