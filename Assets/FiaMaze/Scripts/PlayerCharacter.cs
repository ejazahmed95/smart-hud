using RangerRPG.Core;
using RangerRPG.Inventory;
using UnityEngine;

namespace FiaMaze {
    public class PlayerCharacter : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out ItemBehaviour item) == false) {
                return;
            }
            Log.Info("Trigger Entered!");
            InventorySystem.Instance.Add(item.itemData);
            Destroy(item.gameObject);
        }
    }
}