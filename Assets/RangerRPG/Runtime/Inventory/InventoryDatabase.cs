using System.Collections.Generic;
using RangerRPG.EventSystem;
using UnityEngine;

namespace RangerRPG.Inventory {
    [CreateAssetMenu(fileName = "InventoryDatabase", menuName = "Inventory/Database", order = 0)]
    public class InventoryDatabase : ScriptableObject {
        public Dictionary<ItemData, ItemStack> AllItems = new();

        public GameEvent newItemAcquireEvent;
        
        public void AddNewItem(ItemStack newItemStack) {
            AllItems.Add(newItemStack.data, newItemStack);
            //newItemAcquireEvent.Raise(new ItemAcquireEventData{ItemData = newItemStack.data});
        }
        public void ResetData() {
            AllItems.Clear();
        }
    }
}