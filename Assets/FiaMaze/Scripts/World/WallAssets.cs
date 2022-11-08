using UnityEngine;

namespace FiaMaze.World {
    [CreateAssetMenu(fileName = "WallAssets", menuName = "Game/WallAssets", order = 0)]
    public class WallAssets : ScriptableObject {
        public GameObject BasicWall;
        public GameObject BasicWallWithDoor;
    }
}