using System;
using RangerRPG.Core;
using RangerRPG.Grids;
using UnityEngine;

namespace FiaMaze.World {
    public class WorldManager : SingletonBehaviour<WorldManager> {
        [SerializeField] private WorldGrid grid;
        [SerializeField] private MiniWorldTemplateFactory worldTemplates;
        [SerializeField] private GameObject worldParent;
        [SerializeField] private GameObject playerPrefab;

        // State Variables
        private WorldCellInfo _currentCellInfo;
        private MiniWorld _currentWorld;
        
        private void Start() {
            GenerateWorld();
            LoadMiniWorld();
            SpawnPlayer();
        }

        /// <summary>
        /// GenerateWorld traverses through the world grid and updates the core information of each grid
        /// </summary>
        private void GenerateWorld() {
            // Update World Cell Info
            _currentCellInfo = grid.GetCell(new AxialPosition(0, 0));
        }
        
        /// <summary>
        /// LoadMini
        /// </summary>
        private void LoadMiniWorld() {
            var mwTemplate = worldTemplates.GetTemplateForWorldCell(_currentCellInfo);
            HexDirection availableDirs = grid.GetAvailableDirections(_currentCellInfo.Position);
            _currentWorld = Instantiate(mwTemplate, worldParent.transform).Init(_currentCellInfo, availableDirs);
        }
        
        /// <summary>
        /// Spawn the Player and start the game
        /// </summary>
        private void SpawnPlayer() {
            //Instantiate(playerPrefab);
        }

        public void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}