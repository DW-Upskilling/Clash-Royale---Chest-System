using UnityEngine;

using DevelopersWork.ChestSystem.Managers;
using DevelopersWork.ChestSystem.Components.Chest;

namespace DevelopersWork.ChestSystem.Components.Slot
{
    public class SlotController
    {
        GameManager gameManager;

        SlotModel slotModel;
        SlotView slotView;

        public bool IsSlotAvailable { get { return !slotModel.IsOccupied; } }
        public int SequenceNumber { get { return slotModel.SequenceNumber; } }

        public SlotController(SlotView slotPrefab, int index)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
                throw new MissingReferenceException("GameManager instance not found!");

            slotModel = new SlotModel(index);

            slotView = GameObject.Instantiate<SlotView>(slotPrefab, Vector3.zero, Quaternion.identity);
            slotView.gameObject.transform.SetParent(gameManager.ChestSlotContainer.transform);
            slotView.SetSlotController(this);
        }

        public bool UseSlot(ChestController chestController)
        {
            if (slotModel.IsOccupied)
                return false;

            slotModel.IsOccupied = true;
            slotModel.ChestController = chestController;

            slotView.gameObject.SetActive(false);

            return true;
        }

        public bool ReleaseSlot(ChestController chestController)
        {
            if(slotModel.ChestController == chestController && !slotModel.IsDestroyed)
            {
                slotModel.IsOccupied = false;

                slotView.gameObject.SetActive(true);

                return true;
            }

            return false;
        }
    
        public void OnDestory() {
            slotModel.IsDestroyed = true;
        }
    }
}

