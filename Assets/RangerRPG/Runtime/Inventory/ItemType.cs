using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.Inventory {
    /// <summary>
    /// This is an enum used
    /// </summary>
    [CreateAssetMenu(fileName = "ItemType", menuName = "Inventory/ItemType", order = 0)]
    public class ItemType : ScriptableObject {
        public ItemType parentType = null;
    }
}