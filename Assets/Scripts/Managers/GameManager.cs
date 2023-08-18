using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int totalChestSlots = 4;
    public int TotalChestSlots { get { return totalChestSlots; } }

    protected override void Initialize()
    {

    }
}
