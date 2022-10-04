using System;
using System.Collections.Generic;
using RangerRPG.Core;
using UnityEngine;

namespace RangerRPG.Inventory {
    public class AdaptableTab: ItemAcquireListenerBehaviour {
        public List<ItemType> items = new();

        public bool activated = false;

        private void Start() {
            gameObject.SetActive(activated);
        }
        
        public override void OnAddNewItem(ItemData newItem) {
            if (activated) return;
            //Log.Info("adaptable tab");
            if (items.Contains(newItem.type)) {
                activated = true;
                gameObject.SetActive(true);
            }
        }

        private void OnEnable() {
            //Log.Info($"adaptable tab:: on enable {activated}");
            gameObject.SetActive(activated);
        }
    }

    public interface IItemAcquireListener {
        public void OnAddNewItem(ItemData newItem);
    }
}