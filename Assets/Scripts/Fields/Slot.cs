using System;
using UnityEngine;

public class Slot
{
    bool isOccupied = false;
    public bool IsOccupied { get { return isOccupied; } set { isOccupied = value; } }

    public ChestController ChestController { get; set; }
}
