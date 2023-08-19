using UnityEngine;

public class SessionManager : Singleton<SessionManager>
{
    int availableChestSlots;
    public int AvailableChestSlots { get { return availableChestSlots; } }

    GameManager gameManager;

    protected override void Initialize()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager instance not found!");

        availableChestSlots = gameManager.TotalChestSlots;
    }

    public bool isSlotAvailable()
    {
        return availableChestSlots > 0;
    }
}
