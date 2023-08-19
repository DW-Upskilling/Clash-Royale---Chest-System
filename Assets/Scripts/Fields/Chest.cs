using System;
using UnityEngine;

public class Chest
{
    public string ChestName { get; set; }
    public ChestType ChestType { get; set; }
    public TimeType ChestUnlockTimeType { get; set; }
    public float ChestUnlockTime { get; set; }
}
