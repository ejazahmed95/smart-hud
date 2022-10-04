using System;
using System.Collections.Generic;
using RangerRPG.Core;
using RangerRPG.Inventory;

public class InventorySystem : SingletonBehaviour<InventorySystem>
{
    public List<ItemStack> AllItemStack;
    public List<Tab> AllTabs;
    public List<ItemAcquireListenerBehaviour> itemListeners = new();

    public InventoryDatabase inventoryDB;

    public override void Awake()
    {
        base.Awake();
        AllItemStack = new List<ItemStack>();
        inventoryDB.ResetData();
    }

    public ItemStack GetStack(ItemData i_itemData)
    {
        if(inventoryDB.AllItems.TryGetValue(i_itemData, out ItemStack itemStack))
        {
            return itemStack;
        }
        return null;
    }

    public bool Add(ItemData i_itemData)
    {
        // If Item Stack exist
        if(inventoryDB.AllItems.TryGetValue(i_itemData, out ItemStack itemStack))
        {
            // Check if stack reach max number
            if (itemStack.stackSize + 1 > i_itemData.limitNumber) return false;
            // Check if tab reach max weight
            // if( i_itemData.tab.baseOnWeight && i_itemData.weight + i_itemData.tab.totalWeight > i_itemData.tab.LimitWeight) return false;
            itemStack.AddToStack(i_itemData);
        }
        // If Item Stack does not exist
        else
        {
            // check tab weight
            // if (i_itemData.tab.baseOnWeight) {
            //     if (i_itemData.weight + i_itemData.tab.totalWeight > i_itemData.tab.LimitWeight) return false;
            // }
            // // check tab slot 
            // else if (i_itemData.tab.totalSlot + 1 > i_itemData.tab.LimitSlot) return false;
        
            ItemStack newItemStack = new ItemStack(i_itemData);
            AllItemStack.Add(newItemStack);
            inventoryDB.AddNewItem(newItemStack);
            Log.Debug($"Added a new item:: {i_itemData.displayName}");
            UpdateListeners(newItemStack);
        }

        return true;
    }
    
    private void UpdateListeners(ItemStack newItemStack) {
        foreach (var itemListener in itemListeners) {
            itemListener.OnAddNewItem(newItemStack.data);
        }
    }

    public void Remove(ItemData i_itemData)
    {
        if(inventoryDB.AllItems.TryGetValue(i_itemData, out ItemStack itemStack))
        {
            itemStack.RemoveFromStack();

            if(itemStack.stackSize == 0)
            {
                AllItemStack.Remove(itemStack);
                inventoryDB.AllItems.Remove(i_itemData);
            }
        }
    }
}
