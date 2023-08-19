using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.ScriptableObjects;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int totalChestSlots = 4;
    public int TotalChestSlots { get { return totalChestSlots; } }

    [SerializeField]
    List<ChestScriptableObject> chestScriptableObjectList;

    public List<Slot> ChestSlots { get; private set; }

    protected override void Initialize()
    {
        if (chestScriptableObjectList == null || chestScriptableObjectList.Count == 0)
            throw new MissingReferenceException("chestScriptableObjectList not provided");

        ChestSlots = new List<Slot>(totalChestSlots);
    }

    public ChestScriptableObject GetChestScriptableObjectByType(ChestType chestType)
    {
        return chestScriptableObjectList.Find(e => e.ChestType == chestType);
    }

    public Slot GetEmptySlot()
    {
        return ChestSlots.Find(e => e.IsOccupied != true);
    }
}
