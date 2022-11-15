using System;
using UnityEngine;

namespace FiaMaze.World {
    public class SimplePickup : MonoBehaviour {

        public bool isPicked = false;
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player") && isPicked == false) {
                isPicked = true;
                HandleTrigger(other.gameObject);
                Destroy(gameObject, 0.1f);
            }
        }

        public virtual void HandleTrigger(GameObject other) {
            return;
        }
    }
}