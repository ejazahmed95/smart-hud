using RangerRPG.Core;
using RangerRPG.Inventory;
using UnityEngine;

namespace FiaMaze {
    public class PlayerCharacter : MonoBehaviour {

        [SerializeField] private GameObject shootProjectile;
        [SerializeField] private GameObject spawnPoint;
        
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out ItemBehaviour item) == false) {
                return;
            }
            Log.Info("Trigger Entered!");
            InventorySystem.Instance.Add(item.itemData);
            Destroy(item.gameObject);
        }

        public void OnFire() {
            Log.Info("Spawned a projectile");
            Instantiate(shootProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}