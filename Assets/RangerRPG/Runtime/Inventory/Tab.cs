using System.Collections;
using System.Collections.Generic;
using RangerRPG.Inventory;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Tab")]
public class Tab : ScriptableObject
{
    public string displayName;
    public ItemType holdingTypes;
    public int LimitWeight;
    public int LimitSlot;
    public bool baseOnWeight;
    public int totalWeight { get; private set; }
    public int totalSlot { get; private set; }
    
    public void AddToTab(ItemData i_itemData)
    {
        totalWeight += i_itemData.weight;
    }

    public void RemoveFromTab(ItemData i_itemData)
    {
        totalWeight -= i_itemData.weight;
    }

    public void AddToSlot()
    {
        totalSlot++;
    }

    public void RemoveFromSlot()
    {
        totalSlot--;
    }
}
