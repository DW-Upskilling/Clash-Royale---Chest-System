using System;
using UnityEngine;

public class Slot
{
    public int Index { get; private set; }

    bool isOccupied = false;
    public bool IsOccupied { get { return isOccupied; } set { isOccupied = value; } }

    public ChestController ChestController { get; set; }

    public Slot(int id)
    {
        Index = id;
    }
}
