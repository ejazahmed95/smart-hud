using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.Inventory {
    /// <summary>
    /// This is an enum used
    /// </summary>
    [CreateAssetMenu(fileName = "ItemType", menuName = "Inventory/ItemType", order = 0)]
    public class ItemType : ScriptableObject {
        public ItemType parentType = null;
        public bool stackable;
        public bool weightBased;

        public bool IsType(ItemType other) {
            ItemType checkType = other;
            while (checkType != null) {
                if (checkType == this) {
                    return true;
                }
                checkType = checkType.parentType;
            }
            return false;
        }
    }
}