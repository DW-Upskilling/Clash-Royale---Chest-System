using UnityEngine;
using UnityEngine.UI;

namespace DevelopersWork.ChestSystem.Components.Slot
{
    [RequireComponent(typeof(Image))]
    public class SlotView: MonoBehaviour
    {
        SlotController slotController;

        void OnDestroy()
        {
            if(slotController != null)
                slotController.OnDestory();
        }

        public void SetSlotController(SlotController slotController)
        {
            this.slotController = slotController;
        }
    }
}

