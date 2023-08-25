using UnityEngine;
using UnityEngine.UI;

using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.ScriptableObjects;

using DevelopersWork.ChestSystem.Components.Chest;
using DevelopersWork.ChestSystem.Components.Slot;
using DevelopersWork.ChestSystem.Components.DialogBox;

namespace DevelopersWork.ChestSystem.Handlers
{
    [RequireComponent(typeof(Button))]
    public class ChestGenerateHandler : MonoBehaviour
    {      
        ChestService chestService;
        
        Button generateChestButton;

        private void Awake()
        {
            chestService = ChestService.Instance;
            if (chestService == null)
                throw new MissingReferenceException("ChestService instance not found!");

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
            chestService.CreateChest();
        }
    }
}