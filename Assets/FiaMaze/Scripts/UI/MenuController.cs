using RangerRPG.Core;
using UnityEngine;

namespace FiaMaze.World {
    public class MenuController : MonoBehaviour {
        [SerializeField] private string gameSceneName = "GameScene";

        public void LoadGame() {
            CustomSceneLoader.LoadScene(gameSceneName);
        }
    }
}