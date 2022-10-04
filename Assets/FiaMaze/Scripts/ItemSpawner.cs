using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiaMaze {
    public class ItemSpawner : MonoBehaviour {
        public List<ItemData> allItems = new();
        public List<Transform> spawnPoints = new();
        public Transform itemParent;
        
        private IEnumerator Start() {
            yield return new WaitForSeconds(2f);
            SpawnItems();
        }
        
        void SpawnItems() {
            int index = 0;
            foreach (var spawnPoint in spawnPoints) {
                Instantiate(allItems[index].prefab, spawnPoint.position, spawnPoint.rotation, itemParent);
                index++;
            }    
        }
        
    }
}