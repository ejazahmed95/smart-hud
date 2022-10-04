using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.Inventory {
    public class ItemPopulator : ItemAcquireListenerBehaviour {
        public List<ItemType> itemTypes = new();
        public InventoryDatabase inventoryDb;
        public ItemSlotUI itemSlotPrefab;

        public override void OnAddNewItem(ItemData newItem) {
            if (itemTypes.Contains(newItem.type)) {
                Instantiate(itemSlotPrefab, transform).Init(newItem);
            }
        }
    }
}