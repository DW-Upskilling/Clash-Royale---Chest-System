using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    TextMeshProUGUI TimeText;

    protected override void Initialize()
    {
        if (chestScriptableObjectList == null || chestScriptableObjectList.Count == 0)
            throw new MissingReferenceException("chestScriptableObjectList not provided");

        if(chestSlotContainer == null)
            throw new MissingReferenceException("chestSlotContainer not provided");
    }

    private void Start()
    {
        ResetTimeTexts();
    }

    public ChestScriptableObject GetChestScriptableObjectRandom()
    {
        float totalProbability = chestScriptableObjectList.Aggregate(0f, (accumulator, next) => accumulator + next.ChestProbability);
        
        System.Random random = new System.Random();
        float probability = (float)random.NextDouble() * totalProbability;
        
        float differnce = int.MaxValue;
        int index = -1;

        for(int i=0; i< chestScriptableObjectList.Count; i++)
        {
            ChestScriptableObject chestScriptableObject = chestScriptableObjectList[i];
            float currentDiffernce = Math.Abs(chestScriptableObject.ChestProbability - probability);
            if(differnce > currentDiffernce)
            {
                differnce = currentDiffernce;
                index = i;
            }
        }
        return chestScriptableObjectList[index];
    }

    public ChestScriptableObject GetChestScriptableObjectByType(ChestType chestType)
    {
        return chestScriptableObjectList.Find(e => e.ChestType == chestType);
    }

    public void AccelerateTime()
    {
        // 100x times is maximum speed of fast forwarding
        Time.timeScale = Mathf.Min(100, Time.timeScale + 1);
        ResetTimeTexts();
    }
    public void DecelerateTime()
    {
        // 1x times is minimum speed of fast forwarding
        Time.timeScale = Mathf.Max(1, Time.timeScale - 1);
        ResetTimeTexts();
    }

    void ResetTimeTexts()
    {
        if (TimeText != null)
        {
            TimeText.text = Time.timeScale + "x";
        }
    }
}
