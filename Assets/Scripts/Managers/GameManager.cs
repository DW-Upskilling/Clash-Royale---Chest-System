using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.ScriptableObjects;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int totalChestSlots = 4;
    public int TotalChestSlots { get { return totalChestSlots; } }

    [SerializeField]
    int concurrentUnlockedChests = 1;
    public int ConcurrentUnlockedChests { get { return concurrentUnlockedChests; } }

    [SerializeField]
    List<ChestScriptableObject> chestScriptableObjectList;

    [SerializeField]
    GridLayoutGroup chestSlotContainer;
    public GameObject ChestSlotContainer { get { return chestSlotContainer.gameObject; } }

    public List<Slot> ChestSlots { get; private set; }

    protected override void Initialize()
    {
        if (chestScriptableObjectList == null || chestScriptableObjectList.Count == 0)
            throw new MissingReferenceException("chestScriptableObjectList not provided");

        if(chestSlotContainer == null)
            throw new MissingReferenceException("chestSlotContainer not provided");

        ChestSlots = new List<Slot>();
        for(int i = 0; i < totalChestSlots; i++)
            ChestSlots.Add(new Slot());
    }

    public ChestScriptableObject GetChestScriptableObjectByType(ChestType chestType)
    {
        return chestScriptableObjectList.Find(e => e.ChestType == chestType);
    }

    public Slot GetEmptySlot()
    {
        return ChestSlots.Find(e => e.IsOccupied != true);
    }

    public bool GetUnlockingSlot()
    {
        if(concurrentUnlockedChests > 0)
        {
            concurrentUnlockedChests -= 1;
            return true;
        }

        return false;
    }

    public void GiveUnlockingSlot()
    {
        concurrentUnlockedChests += 1;    
    }
}
