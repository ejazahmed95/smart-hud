using UnityEngine;

namespace RangerRPG.Inventory {
    public abstract class ItemAcquireListenerBehaviour : MonoBehaviour {

        public abstract void OnAddNewItem(ItemData newItem);
    }
}