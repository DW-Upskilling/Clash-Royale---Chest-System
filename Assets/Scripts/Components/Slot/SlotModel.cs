using DevelopersWork.ChestSystem.Components.Chest;

namespace DevelopersWork.ChestSystem.Components.Slot
{
    public class SlotModel
    {
        public int SequenceNumber { get; private set; }

        public bool IsOccupied { get; set; }
        public bool IsDestroyed { get; set; }
        public ChestController ChestController { get; set; }

        public SlotModel(int sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
            
            IsOccupied = false;
            IsDestroyed = false;
        }
    }
}
