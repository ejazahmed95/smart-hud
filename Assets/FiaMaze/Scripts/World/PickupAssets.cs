using RangerRPG.Grids;
using UnityEngine;

namespace FiaMaze.World {
    [CreateAssetMenu(fileName = "PickupAssets", menuName = "Game/PickupAssets", order = 0)]
    public class PickupAssets : ScriptableObject {
        public GameObject HealthPickup;
        public GameObject DamagePickup;
        public GameObject WinPickup;

        public GenericDictionary<HexDirection, Material> materials = new();
        public Material defaultMaterial;
            
        public Material GetMaterialForDirection(HexDirection direction) {
            foreach (var (key, value) in materials) {
                if (key == direction) {
                    return value;
                }
            }
            return defaultMaterial;
        }
    }
}