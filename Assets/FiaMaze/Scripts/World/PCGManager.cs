using System.Collections;
using RangerRPG.Core;
using RangerRPG.Grids;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace FiaMaze.World {
    public class PCGManager : SingletonBehaviour<PCGManager> {
        public WorldGrid grid;
        public GameObject playerPrefab;
        public GameObject playerRef;
        private HexGridView<WorldCellInfo> _worldView;
        private WorldCellInfo _playerCell, _endCell, _currentPlayerCell;

        private void Start() {
            grid.InitializeWorldInfo();
            InitStartAndEnd();
            
            grid.InitializeWorldView();
            _worldView = grid.worldView;
            _worldView.GenerateView(_currentPlayerCell);

            var cellLocation = _worldView.GetCellView(_playerCell.Position);

            StartCoroutine(SpawnPlayer(cellLocation.transform));
           
        }
        private IEnumerator SpawnPlayer(Transform cellTransform) {
            yield return null;
            // yield return new WaitForSeconds(0.2f);
            var player = playerRef;
            player.transform.localPosition = cellTransform.position + new Vector3(0, 3, 0);
            Log.Info($"Setting Player Position = {player.transform.position}; Cell Location = {cellTransform.transform.position}");
        }

        private void Update() {
            if (Keyboard.current.rKey.wasPressedThisFrame) {
                SceneManager.LoadScene("PCG");
            }
        }

        private IEnumerator ReloadScene() {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("PCG");
        }

        private void InitStartAndEnd() {
            _playerCell = grid.GetRandomCell();
            Log.Info($"Player Start Cell = {_playerCell.Position}");
            _playerCell.IsPlayerStart = true;
            _currentPlayerCell = _playerCell;
            
            _endCell = grid.GetRandomCell();
            _endCell.IsEndCell = true;
        }

        private void MergeRooms() {
            int roomId = 1;
            grid.ForEachCell(cell => {
                
            });
        }
        
        public void OnPlayerEntered(WorldCellInfo newCell) {
            if (_currentPlayerCell == newCell) {
                //Log.Info($"Same Cell Entry {newCell.Position}");
                return;
            }
            Log.Info($"Entered Cell {newCell.Position}");
            if (newCell == _endCell) {
                StartCoroutine(ReloadScene());
                return;
            }
            UIDebugger.Instance.SetInfo(newCell);
            _currentPlayerCell.SetPlayerStatus(false);
            _currentPlayerCell = newCell;
            _currentPlayerCell.SetPlayerStatus(true);
            _worldView.GenerateView(_currentPlayerCell);
        }

        public GameObject GetPlayerRef() {
            return playerRef;
        }
    }
}