using System.Collections.Generic;

using UnityEngine;
using TMPro;

using Assets.Scripts.Handlers;

public class SessionManager : Singleton<SessionManager>
{
    [SerializeField]
    TextMeshProUGUI coinsText;
    [SerializeField]
    TextMeshProUGUI gemsText;

    [SerializeField]
    DialogBoxHandler dialogBoxPrefab;

    GameManager gameManager;

    public List<Slot> ChestSlots { get; private set; }
    public List<bool> UnlockingChestSlots { get; private set; }
    
    int CurrentAvailableChestSlots;
    int CurrentUnlockingChestSlots;

    int coins;
    int gems;

    protected override void Initialize()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager instance not found!");

        CurrentAvailableChestSlots = gameManager.TotalChestSlots;
        CurrentUnlockingChestSlots = gameManager.ConcurrentUnlockedChests;

        ChestSlots = new List<Slot>();
        UnlockingChestSlots = new List<bool>();
        for (int i = 0; i < CurrentAvailableChestSlots; i++)
        {
            UnlockingChestSlots.Add(false);
            ChestSlots.Add(new Slot(i));
        }

        LoadPlayerPrefsData();
    }

    void LoadPlayerPrefsData()
    {
        coins = PlayerPrefs.GetInt("Coins", 10);
        gems = PlayerPrefs.GetInt("Gems", 10);

        updateCurrencyText();
    }

    public bool IsChestSlotAvailable()
    {
        return CurrentAvailableChestSlots > 0;
    }

    public Slot GetEmptySlot()
    {
        if (IsChestSlotAvailable())
        {
            CurrentAvailableChestSlots--;
            return ChestSlots.Find(e => e.IsOccupied != true);
        }
        ShowMessage("Slots Full", "There are no available slots to add more chests. You must remove some chests to create space.");
        return null;
    }

    public void ReturnEmptySlot(Slot slot)
    {
        slot.IsOccupied = false;
        CurrentAvailableChestSlots++;
    }

    public bool IsChestUnlockingSlotAvailable()
    {
        return CurrentUnlockingChestSlots > 0;
    }

    public bool UseUnlockingSlot(Slot slot)
    {
        if (IsChestUnlockingSlotAvailable())
        {
            CurrentUnlockingChestSlots--;
            UnlockingChestSlots[slot.Index] = true;
            return true;
        }
        ShowMessage("Unlocking Slots Full", "No available unlocking slots. Wait for existing chests to unlock or use gems to speed up the process.");
        return false;
    }

    public void ReturnUnlockingSlot(Slot slot)
    {
        UnlockingChestSlots[slot.Index] = false;
        CurrentUnlockingChestSlots++;
    }

    public bool UseCurrency(CurrencyType currencyType, int value)
    {
        switch (currencyType)
        {
            case CurrencyType.Coin:
                if(coins >= value)
                {
                    coins -= value;
                    PlayerPrefs.SetInt("Coins", coins);
                    updateCurrencyText();
                    return true;
                }
                break;
            case CurrencyType.Gem:
                if (gems >= value)
                {
                    gems -= value;
                    PlayerPrefs.SetInt("Gems", gems);
                    updateCurrencyText();
                    return true;
                }
                break;
        }
        return false;
    }

    public void IncrementCurrency(CurrencyType currencyType, int value)
    {
        switch (currencyType)
        {
            case CurrencyType.Coin:
                coins += value;
                PlayerPrefs.SetInt("Coins", coins);
                break;
            case CurrencyType.Gem:
                gems += value;
                PlayerPrefs.SetInt("Gems", gems);
                break;
        }
        updateCurrencyText();
    }

    void updateCurrencyText()
    {
        if(coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
        if(gemsText != null)
        {
            gemsText.text = gems.ToString();
        }
    }

    public void ShowMessage(string title, string message)
    {
        DialogBoxHandler dialogBox = GameObject.Instantiate<DialogBoxHandler>(dialogBoxPrefab, Vector3.zero, Quaternion.identity);

        dialogBox.TitleText = title;
        dialogBox.MessageText = message;
    }
}
